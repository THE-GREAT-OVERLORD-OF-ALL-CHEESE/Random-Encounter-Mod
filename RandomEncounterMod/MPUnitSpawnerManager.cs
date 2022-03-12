using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VTNetworking;
using VTOLVR.Multiplayer;

public static class MPUnitSpawnerManager
{
    public enum UnitType
    {
        Aircraft,
        Land,
        Sea
    }

    public static void SpawnUnit(string unitName, ForceManager manager, UnitType type, Vector3D pos, Quaternion rot, string[] loadout)
    {
        if (VTOLMPUtils.IsMultiplayer() && VTOLMPLobbyManager.isLobbyHost == false)
        {
            return;
        }

        if (UnitCatalogue.GetUnitPrefab(unitName) != null)
        {
            Vector3 spawnPos = VTMapManager.GlobalToWorldPoint(pos);
            Quaternion spawnRot = rot;

            Debug.Log("setting up unit spawner");
            GameObject unitSpawnerObj = new GameObject();
            UnitSpawner unitSpawner = unitSpawnerObj.AddComponent<UnitSpawner>();
            unitSpawner.unitID = unitName;

            unitSpawner.transform.position = spawnPos;
            unitSpawner.transform.rotation = spawnRot;

            if (VTOLMPUtils.IsMultiplayer())
            {
                Debug.Log("Network spawning a " + unitName + " at pos: " + pos.ToString());
                VTOLMPUnitManager.instance.StartCoroutine(MPSpawnRoutine(unitSpawner, manager, type, pos, rot, loadout));
            }
            else
            {
                Debug.Log("Local spawning a " + unitName + " at pos: " + pos.ToString());
                GameObject aircraftObj = GameObject.Instantiate(UnitCatalogue.GetUnitPrefab(unitName), spawnPos, rot, null);

                SetupUnit(unitSpawner, aircraftObj, manager, type, pos, rot, loadout);
            }
        }
        else
        {
            Debug.Log("No aircraft were available, cannot spawn a traffic aircraft.");
        }
    }

    public static IEnumerator MPSpawnRoutine(UnitSpawner spawner, ForceManager manager, UnitType type, Vector3D pos, Quaternion rot, string[] loadout)
    {
        string resourcePath = UnitCatalogue.GetUnit(spawner.unitID).resourcePath;
        VTNetworkManager.NetInstantiateRequest req = VTNetworkManager.NetInstantiate(resourcePath, spawner.transform.position, spawner.transform.rotation, true);
        while (!req.isReady)
        {
            yield return null;
        }
        GameObject obj = req.obj;

        SetupUnit(spawner, obj, manager, type, pos, rot, loadout);
    }

    public static void SetupUnit(UnitSpawner spawn, GameObject aircraftObj, ForceManager manager, UnitType type, Vector3D pos, Quaternion rot, string[] loadout)
    {
        switch (type)
        {
            case UnitType.Aircraft:
                SetupAircraft(spawn, aircraftObj, manager, pos, rot, loadout);
                break;
            case UnitType.Land:
                SetupLand(spawn, aircraftObj, (ForceManager_Ground)manager, pos, rot, loadout);
                break;
            case UnitType.Sea:
                SetupSea(spawn, aircraftObj, (ForceManager_Sea)manager, pos, rot, loadout);
                break;
        }
    }

    public static void SetupAircraft(UnitSpawner spawn, GameObject aircraftObj, ForceManager manager, Vector3D pos, Quaternion rot, string[] hpLoadout)
    {
        Vector3 spawnPos = VTMapManager.GlobalToWorldPoint(pos);
        Quaternion spawnRot = rot;

        aircraftObj.transform.position = spawnPos;
        aircraftObj.transform.rotation = spawnRot;

        Debug.Log("setting up floating origin");
        FloatingOriginTransform floatingTransform = aircraftObj.GetComponent<FloatingOriginTransform>();

        if (floatingTransform == null)
        {
            floatingTransform = aircraftObj.AddComponent<FloatingOriginTransform>();
        }

        aircraftObj.SetActive(false);
        ForceUnit_Aircraft ai = aircraftObj.AddComponent<ForceUnit_Aircraft>();
        ai.SetForce(manager);

        floatingTransform.SetRigidbody(aircraftObj.GetComponent<Rigidbody>());
        aircraftObj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        Debug.Log("setting up unit spawner");
        ai.aircraft.unitSpawner = spawn;

        Traverse unitSpawnerTraverse = Traverse.Create(ai.aircraft.unitSpawner);
        unitSpawnerTraverse.Field("_spawned").SetValue(true);
        Traverse aircraftSpawnerTraverse = Traverse.Create(ai.aircraft);
        aircraftSpawnerTraverse.Field("taxiSpeed").SetValue(ai.aircraft.aiPilot.taxiSpeed);

        Debug.Log("setting up engagers");
        IEngageEnemies[] engagers = ai.aircraft.gameObject.GetComponentsInChildrenImplementing<IEngageEnemies>(true);
        aircraftSpawnerTraverse.Field("engagers").SetValue(engagers);

        ai.aircraft.autoRTB = true;
        ai.aircraft.SetEngageEnemies(true);

        ai.pilot.startLanded = false;
        ai.pilot.actor.discovered = false;

        if (ai.pilot.detectionRadar != null)
        {
            ai.pilot.vt_radarEnabled = true;
            ai.pilot.playerComms_radarEnabled = true;
        }

        Debug.Log("enabling aircraft");
        aircraftObj.SetActive(true);

        Debug.Log("setting up loadout");
        Loadout loadout = new Loadout();
        loadout.hpLoadout = hpLoadout;//set the correct loadout
        loadout.normalizedFuel = 1;
        loadout.cmLoadout = new int[] { 30, 30 };
        if (ai.wm)
        {
            if (VTOLMPUtils.IsMultiplayer())
            {
                if (VTOLMPLobbyManager.isLobbyHost)
                {
                    WeaponManagerSync wmSync = aircraftObj.GetComponentInChildren<WeaponManagerSync>(true);
                    if (wmSync)
                    {
                        wmSync.NetClearWeapons();
                        wmSync.NetEquipWeapons(loadout, false);
                        return;
                    }
                }
            }
            else
            {
                ai.wm.EquipWeapons(loadout);
            }
        }
        aircraftSpawnerTraverse.Field("loadout").SetValue(loadout);
    }

    public static void SetupLand(UnitSpawner spawner, GameObject vehicleObj, ForceManager_Ground manager, Vector3D pos, Quaternion rot, string[] hpLoadout)
    {
        Debug.Log("setting up floating origin");
        FloatingOriginTransform floatingTransform = vehicleObj.GetComponent<FloatingOriginTransform>();
        if (floatingTransform == null)
        {
            Debug.Log("floating origin was null, adding a new one");
            floatingTransform = vehicleObj.AddComponent<FloatingOriginTransform>();
        }
        else
        {
            Debug.Log($"{floatingTransform.gameObject.name} already had a floating transform");
        }


        Rigidbody rb = vehicleObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            floatingTransform.SetRigidbody(rb);
        }


        vehicleObj.SetActive(false);

        Actor actor = vehicleObj.GetComponent<Actor>();
        GroundUnitMover mover = vehicleObj.GetComponent<GroundUnitMover>();
        manager.squad.RegisterUnit(mover);


        GroundUnitSpawn spawn = vehicleObj.GetComponent<GroundUnitSpawn>();

        //spawn.OnPreSpawnUnit();

        manager.spawns.Add(spawn);

        mover.moveSpeed = manager.mission.speed;

        GameObject unitSpawner = new GameObject();
        spawn.unitSpawner = unitSpawner.AddComponent<UnitSpawner>();

        //modify arty to attack
        /*ArtilleryUnit arty = vehicleObj.GetComponent<ArtilleryUnit>();
        if (arty != null && arty.targetFinder == null) {
            arty.targetFinder = vehicleObj.GetComponent<VisualTargetFinder>();
            arty.SetEngageEnemies(true);
        }*/

        vehicleObj.SetActive(true);

        //if (RandomEncounterMod.instance.mpMode)
        //{
        //    Debug.Log("MP is enabled, networking this vehicle!");
        //RandomEncounterMod.instance.MPSetUpAircraft(actor);
        //}
    }

    public static void SetupSea(UnitSpawner spawner, GameObject vehicleObj, ForceManager_Sea manager, Vector3D pos, Quaternion rot, string[] hpLoadout)
    {
        Debug.Log("setting up floating origin");
        FloatingOriginTransform floatingTransform = vehicleObj.GetComponent<FloatingOriginTransform>();
        if (floatingTransform == null)
        {
            Debug.Log("floating origin was null, adding a new one");
            floatingTransform = vehicleObj.AddComponent<FloatingOriginTransform>();
        }
        else
        {
            Debug.Log($"{floatingTransform.gameObject.name} already had a floating transform");
        }


        Rigidbody rb = vehicleObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            floatingTransform.SetRigidbody(rb);
        }


        vehicleObj.SetActive(false);

        Actor actor = vehicleObj.GetComponent<Actor>();
        ShipMover mover = vehicleObj.GetComponent<ShipMover>();
        manager.ship = mover;

        //GameObject unitSpawner = new GameObject();
        //spawn.unitSpawner = unitSpawner.AddComponent<UnitSpawner>();

        vehicleObj.SetActive(true);
    }
}
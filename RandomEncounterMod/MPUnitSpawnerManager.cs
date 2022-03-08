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

    public static void SpawnUnit(string unitName, ForceManager manager, UnitType type, Vector3D pos, Quaternion rot)
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
                VTOLMPUnitManager.instance.StartCoroutine(MPSpawnRoutine(unitSpawner, manager, type, pos, rot));
            }
            else
            {
                Debug.Log("Spawning a " + unitName + " at pos: " + pos.ToString());
                GameObject aircraftObj = GameObject.Instantiate(UnitCatalogue.GetUnitPrefab(unitName), spawnPos, rot, null);

                SetupUnit(unitSpawner, aircraftObj, manager, type, pos, rot);
            }
        }
        else
        {
            Debug.Log("No aircraft were available, cannot spawn a traffic aircraft.");
        }
    }

    public static IEnumerator MPSpawnRoutine(UnitSpawner spawner, ForceManager manager, UnitType type, Vector3D pos, Quaternion rot)
    {
        string resourcePath = UnitCatalogue.GetUnit(spawner.unitID).resourcePath;
        VTNetworkManager.NetInstantiateRequest req = VTNetworkManager.NetInstantiate(resourcePath, spawner.transform.position, spawner.transform.rotation, true);
        while (!req.isReady)
        {
            yield return null;
        }
        GameObject obj = req.obj;

        SetupUnit(spawner, obj, manager, type, pos, rot);
    }

    public static void SetupUnit(UnitSpawner spawn, GameObject aircraftObj, ForceManager manager, UnitType type, Vector3D pos, Quaternion rot)
    {
        switch (type)
        {
            case UnitType.Aircraft:
                SetupAircraft(spawn, aircraftObj, manager, pos, rot);
                break;
        }
    }

    public static void SetupAircraft(UnitSpawner spawn, GameObject aircraftObj, ForceManager manager, Vector3D pos, Quaternion rot)
    {
        Vector3 spawnPos = VTMapManager.GlobalToWorldPoint(pos);
        Quaternion spawnRot = rot;

        aircraftObj.SetActive(false);
        ForceUnit_Aircraft ai = aircraftObj.AddComponent<ForceUnit_Aircraft>();
        ai.SetForce(manager);

        Debug.Log("setting up floating origin");
        FloatingOriginTransform floatingTransform = aircraftObj.GetComponent<FloatingOriginTransform>();

        if (floatingTransform == null)
        {
            floatingTransform = aircraftObj.AddComponent<FloatingOriginTransform>();
        }

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
        loadout.hpLoadout = new string[0];
        loadout.normalizedFuel = 1;
        loadout.cmLoadout = new int[] { 30, 30 };
        if (ai.wm)
        {
            ai.wm.EquipWeapons(loadout);
        }
        aircraftSpawnerTraverse.Field("loadout").SetValue(loadout);

        if (ai.pilot.actor.team == Teams.Enemy || false)
        {
            Debug.Log("forcing team to allied");

            ai.pilot.actor.team = Teams.Allied;
            TargetManager.instance.UnregisterActor(ai.pilot.actor);
            TargetManager.instance.RegisterActor(ai.pilot.actor);
        }
    }
}

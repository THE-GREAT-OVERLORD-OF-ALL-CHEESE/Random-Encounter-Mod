using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;

public class ForceManager : MonoBehaviour
{
    public List<ForceAircraft> aircrafts;
    public List<AITask> tasks;

    public Waypoint waypoint;

    public AIMission mission;
    public float missionDirection;

    public void Awake() {
        SpawnManager.AddForce(this);

        aircrafts = new List<ForceAircraft>();
        tasks = new List<AITask>();
    }

    public void SetUp(AIMission newMission) {
        missionDirection = UnityEngine.Random.Range(0f, 360f);

        mission = newMission;



        Vector3D playerPos = SpawnManager.GetPlayerPosition();

        Vector3D spawnPos3D = new Vector3D(-Mathf.Sin(missionDirection * Mathf.Deg2Rad) * SpawnManager.GetTrafficRadius() + playerPos.x, mission.altitude, -Mathf.Cos(missionDirection * Mathf.Deg2Rad) * SpawnManager.GetTrafficRadius() + playerPos.z);
        Vector3 spawnPos = VTMapManager.GlobalToWorldPoint(spawnPos3D);
        Quaternion spawnRot = Quaternion.LookRotation(new Vector3(Mathf.Sin(missionDirection), 0, Mathf.Cos(missionDirection)));
        Debug.Log("Mission altitude: " + mission.altitude);



        foreach (AircraftLoadout aircraft in mission.aircraft) {
            Debug.Log("Spawning a " +  aircraft.aircraftName + " at pos: " + spawnPos3D.ToString());
            GameObject aircraftObj = Instantiate(UnitCatalogue.GetUnitPrefab(aircraft.aircraftName), spawnPos, spawnRot, null);

            aircraftObj.SetActive(false);

            ForceAircraft ai = aircraftObj.AddComponent<ForceAircraft>();
            ai.SetForce(this);

            ai.rb.velocity = aircraftObj.transform.forward * mission.speed;
            ai.aircraft.aiPilot.navSpeed = mission.speed;
            ai.aircraft.aiPilot.defaultAltitude = mission.altitude;
            //ai.aircraft.OnPreSpawnUnit();
            ai.kPlane.SetToKinematic();
            ai.kPlane.SetSpeed(mission.speed);

            FloatingOriginTransform floatingTransform = aircraftObj.AddComponent<FloatingOriginTransform>();
            floatingTransform.SetRigidbody(aircraftObj.GetComponent<Rigidbody>());
            aircraftObj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

            GameObject unitSpawner = new GameObject();
            ai.aircraft.unitSpawner = unitSpawner.AddComponent<UnitSpawner>();
            Traverse unitSpawnerTraverse = Traverse.Create(ai.aircraft.unitSpawner);
            unitSpawnerTraverse.Field("_spawned").SetValue(true);
            Traverse aircraftSpawnerTraverse = Traverse.Create(ai.aircraft);
            aircraftSpawnerTraverse.Field("taxiSpeed").SetValue(ai.aircraft.aiPilot.taxiSpeed);

            IEngageEnemies[] engagers = ai.aircraft.gameObject.GetComponentsInChildrenImplementing<IEngageEnemies>(true);
            aircraftSpawnerTraverse.Field("engagers").SetValue(engagers);

            ai.pilot.startLanded = false;

            ai.pilot.actor.discovered = false;

            aircraftObj.SetActive(true);

            Loadout loadout = new Loadout();
            loadout.hpLoadout = aircraft.hardpoints;
            loadout.normalizedFuel = 1;
            loadout.cmLoadout = new int[] { 30, 30 };
            ai.wm.EquipWeapons(loadout);

            if (ai.pilot.actor.team == Teams.Allied) {
                ai.pilot.actor.team = Teams.Enemy;
                TargetManager.instance.UnregisterActor(ai.pilot.actor);
                TargetManager.instance.RegisterActor(ai.pilot.actor);
            }

            if (RandomEncounterMod.instance.mpMode)
            {
                Debug.Log("MP is enabled, networking this aircraft!");
                RandomEncounterMod.instance.MPSetUpAircraft(ai.aircraft.actor);
            }
        }

        Vector3D missionTgt = new Vector3D();

        switch (mission.missionType) {
            case AIMissionType.Recon:
                missionTgt = MissionPointManager.GetRandomReconPoint();
                tasks.Add(new AITask_FlyToObjective(this, missionTgt));
                tasks.Add(new AITask_Recon(this, missionTgt));
                tasks.Add(new AITask_AirSupport(this, missionTgt));
                tasks.Add(new AITask_RTB(this));
                break;
            case AIMissionType.Bombing:
                missionTgt = MissionPointManager.GetRandomBombingPoint();
                tasks.Add(new AITask_FlyToObjective(this, missionTgt));
                tasks.Add(new AITask_Bomb(this, missionTgt));
                tasks.Add(new AITask_AirSupport(this, missionTgt));
                tasks.Add(new AITask_RTB(this));
                break;
            case AIMissionType.Strike:
                missionTgt = MissionPointManager.GetRandomStrikePoint();
                tasks.Add(new AITask_FlyToObjective(this, missionTgt));
                tasks.Add(new AITask_Strike(this, missionTgt));
                tasks.Add(new AITask_AirSupport(this, missionTgt));
                tasks.Add(new AITask_RTB(this));
                break;
            case AIMissionType.CAP:
                missionTgt = MissionPointManager.GetRandomCAPPoint();
                tasks.Add(new AITask_FlyToObjective(this, missionTgt));
                tasks.Add(new AITask_CAP(this, missionTgt));
                tasks.Add(new AITask_RTB(this));
                break;
            default:
                tasks.Add(new AITask_RTB(this));
                break;
        }
    }

    public void FixedUpdate() {
        foreach (AITask task in tasks) {
            task.UpdateTask();
        }
    }

    public void AddAircraft(ForceAircraft aircraft) {
        aircrafts.Add(aircraft);
    }

    public void RemoveAircraft(ForceAircraft aircraft)
    {
        aircrafts.Remove(aircraft);
        if (aircrafts.Count == 0) {
            Debug.Log("The force " + mission.missionName + " was completely wiped out.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        SpawnManager.RemoveForce(this);
        foreach (AITask task in tasks) {
            task.Cleanup();
        }
    }
}

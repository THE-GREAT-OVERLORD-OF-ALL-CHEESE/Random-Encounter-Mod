﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;

public class ForceManager_Aircraft : ForceManager
{
    public List<ForceUnit_Aircraft> aircrafts;
    public List<AITask> tasks;

    public Waypoint waypoint;

    public AIMission mission;
    public Vector3D missionSpawn;

    public override void SetUp(FactionManager faction, AIMission newMission) {
        base.SetUp(newMission);

        aircrafts = new List<ForceUnit_Aircraft>();
        tasks = new List<AITask>();

        missionSpawn = faction.planeSpawns[UnityEngine.Random.Range(0, faction.planeSpawns.Count)];

        mission = newMission;



        Vector3D playerPos = faction.GetPlayerPosition();

        Vector3D spawnPos3D = new Vector3D(missionSpawn.x, mission.altitude, missionSpawn.y);
        Quaternion spawnRot = Quaternion.LookRotation((faction.mapCenter - missionSpawn).toVector3);
        Debug.Log("Mission altitude: " + mission.altitude);

        foreach (AircraftLoadout aircraft in mission.aircraft) {
            Debug.Log("Spawning a " +  aircraft.aircraftName + " at pos: " + spawnPos3D.ToString());
            MPUnitSpawnerManager.SpawnUnit(aircraft.aircraftName, this, MPUnitSpawnerManager.UnitType.Aircraft, spawnPos3D, spawnRot);
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
            case AIMissionType.Landing:
                missionTgt = MissionPointManager.GetRandomLandingPoint();
                tasks.Add(new AITask_FlyToObjective(this, missionTgt));
                tasks.Add(new AITask_Landing(this, missionTgt));
                tasks.Add(new AITask_AirSupport(this, missionTgt));
                tasks.Add(new AITask_RTB(this));
                break;
            default:
                tasks.Add(new AITask_RTB(this));
                break;
        }
    }

    public void FixedUpdate() {
        foreach (AITask task in tasks) {
            task.UpdateTask(Time.fixedDeltaTime);
        }
    }

    public override void AddUnit(ForceUnit unit) {
        base.AddUnit(unit);

        aircrafts.Add((ForceUnit_Aircraft)unit);
    }

    public override void RemoveUnit(ForceUnit unit)
    {
        base.RemoveUnit(unit);

        aircrafts.Remove((ForceUnit_Aircraft)unit);
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        foreach (AITask task in tasks) {
            task.Cleanup();
        }
    }
}
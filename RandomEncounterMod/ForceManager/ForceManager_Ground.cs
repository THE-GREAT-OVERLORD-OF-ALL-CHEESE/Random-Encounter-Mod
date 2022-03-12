using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using VTNetworking;
using VTOLVR.Multiplayer;

public class ForceManager_Ground : ForceManager
{
    public AIGroundMission mission;

    public Vector3D missionSpawn;
    public Waypoint waypoint;

    public bool arrived = false;

    public GroundSquad squad;
    public List<GroundUnitSpawn> spawns;

    public CheesesPathfindingAgent agent;

    public void Awake() {

        spawns = new List<GroundUnitSpawn>();

        agent = gameObject.AddComponent<CheesesPathfindingAgent>();
        agent.oceanMovementSpeedPenalty = 3;
    }

    public override void SetUp(FactionManager faction, AIMission newMission)
    {
        base.SetUp(faction, newMission);
        faction.AddGroundForce(this);

        mission = faction.missions.groundMissions[UnityEngine.Random.Range(0, faction.missions.groundMissions.Count)];
        Debug.Log("Spawning the ground force " + mission.missionName);

        missionSpawn = faction.missionPoints.GetRandomGndSpawnPoint().globalPoint;

        squad = gameObject.AddComponent<GroundSquad>();

        foreach (string vehicle in mission.vehicles) {
            Debug.Log("Spawning a " + vehicle + " at pos: " + missionSpawn.ToString());
            MPUnitSpawnerManager.SpawnUnit(vehicle, this, MPUnitSpawnerManager.UnitType.Land, missionSpawn, Quaternion.identity, null);
        }

        //set up the S-400s with their radars
        /*
        Debug.Log("Setting up sams with their radars");
        foreach (GroundUnitSpawn spawn in spawns) {
            if (spawn is AIFixedSAMSpawn) {
                Debug.Log("Finding locking radars for this sam");
                AIFixedSAMSpawn samSpawn = (AIFixedSAMSpawn)spawn;
                //Traverse samTraverse = new Traverse(samSpawn);

                List<LockingRadar> lrs = new List<LockingRadar>();

                foreach (GroundUnitSpawn spawn2 in spawns)
                {
                    if (spawn2 is AILockingRadarSpawn)
                    {
                        Debug.Log("Found a locking radar to assign to the sam");
                        lrs.Add(((AILockingRadarSpawn)spawn2).lockingRadar);
                    }
                }

                samSpawn.samLauncher.lockingRadars = lrs.ToArray();
                Debug.Log("Setting locking radars on this sam");
            }
        }
        */

        squad.formationType = mission.formation;

        waypoint = faction.missionPoints.GetRandomGndRdvPoint();
        //squad.MoveTo(waypoint.GetTransform());

        transform.position = VTMapManager.GlobalToWorldPoint(missionSpawn);
        agent.MoveTo(waypoint.globalPoint);
    }

    private void FixedUpdate() {
        if (squad.leaderMover.path == null && squad.leaderMover.behavior == GroundUnitMover.Behaviors.Parked && agent.request.followPath != null)
        {
            squad.MovePath(agent.request.followPath);
            //squad.leaderMover.behavior = GroundUnitMover.Behaviors.RailPath;
        }

        if (squad.leaderMover.path != null && squad.leaderMover.behavior == GroundUnitMover.Behaviors.Parked && arrived == false) {
            Debug.Log("Arrived at destination");
            
            arrived = true;
            foreach (GroundUnitSpawn spawn in spawns) {
                if (spawn != null) {
                    spawn.SetEngageEnemies(true);
                }
            }
        }
        CheckAlive();
    }

    private void CheckAlive() {
        foreach (GroundUnitSpawn spawn in spawns)
        {
            if (spawn.actor.alive)
            {
                return;
            }
        }
        Destroy(this);
    }

    private void OnDestroy() {
        faction.RemoveGroundForce(this);

        if (VTOLMPUtils.IsMultiplayer())
        {
            foreach (GroundUnitSpawn spawn in spawns)
            {
                if (spawn != null)
                {
                    VTNetworkManager.NetDestroyDelayed(spawn.gameObject, 15f);
                }
            }
        }
        else
        {
            foreach (GroundUnitSpawn spawn in spawns)
            {
                if (spawn != null)
                {
                    Destroy(spawn.gameObject, 15f);
                }
            }
        }
    }
}

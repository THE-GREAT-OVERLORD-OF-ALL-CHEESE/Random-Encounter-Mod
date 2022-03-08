using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;

public class GroundForceManager : MonoBehaviour
{
    public AIGroundMission mission;

    public GroundSquad squad;
    public List<GroundUnitSpawn> spawns;

    public Teams team;

    public Waypoint waypoint;

    public bool arrived = false;

    public void Awake() {

        spawns = new List<GroundUnitSpawn>();
    }

    public void SetUp(AIGroundMission newMission, Teams team, Vector3 position) {
        this.team = team;
        //SpawnManager.AddGroundForce(this, team);

        mission = newMission;

        squad = gameObject.AddComponent<GroundSquad>();

        foreach (string vehicle in mission.vehicles) {
            Debug.Log("Spawning a " + vehicle + " at pos: " + position.ToString());
            GameObject vehicleObj = Instantiate(UnitCatalogue.GetUnitPrefab(vehicle), position, Quaternion.identity, null);

            vehicleObj.SetActive(false);

            Actor actor = vehicleObj.GetComponent<Actor>();
            GroundUnitMover mover = vehicleObj.GetComponent<GroundUnitMover>();
            squad.RegisterUnit(mover);
            GroundUnitSpawn spawn = vehicleObj.GetComponent<GroundUnitSpawn>();
            spawn.OnPreSpawnUnit();
            spawns.Add(spawn);

            mover.moveSpeed = mission.speed;

            GameObject unitSpawner = new GameObject();
            spawn.unitSpawner = unitSpawner.AddComponent<UnitSpawner>();
            spawn.unitSpawner.unitName = vehicle;

            //modify arty to attack
            /*ArtilleryUnit arty = vehicleObj.GetComponent<ArtilleryUnit>();
            if (arty != null && arty.targetFinder == null) {
                arty.targetFinder = vehicleObj.GetComponent<VisualTargetFinder>();
                arty.SetEngageEnemies(true);
            }*/

            vehicleObj.SetActive(true);

            if (actor.team != team)
            {
                actor.team = team;
                TargetManager.instance.UnregisterActor(actor);
                TargetManager.instance.RegisterActor(actor);
            }

            if (RandomEncounterMod.instance.mpMode)
            {
                Debug.Log("MP is enabled, networking this vehicle!");
                //RandomEncounterMod.instance.MPSetUpAircraft(actor);
            }
        }

        //set up the S-400s with their radars
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

        squad.formationType = mission.formation;

        waypoint = MissionPointManager.GetRandomGndRdvPoint(team);
        squad.MoveTo(waypoint.GetTransform());
    }

    private void FixedUpdate() {
        if (squad.leaderMover.behavior == GroundUnitMover.Behaviors.Parked && arrived == false) {
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
        //SpawnManager.RemoveGroundForce(this, team);
        foreach (GroundUnitSpawn spawn in spawns)
        {
            if (spawn != null)
            {
                Destroy(spawn.gameObject);
            }
        }
    }
}

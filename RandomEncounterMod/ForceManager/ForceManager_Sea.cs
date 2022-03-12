using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using VTNetworking;
using VTOLVR.Multiplayer;

public class ForceManager_Sea : ForceManager
{
    public Vector3D missionSpawn;
    public Vector3D missionEnd;

    public bool arrived = false;

    public ShipMover ship;

    public CheesesPathfindingAgent agent;

    public void Awake()
    {
        agent = gameObject.AddComponent<CheesesPathfindingAgent>();

        agent.landMovementSpeedPenalty = 1000;
        agent.maximumNodeDistance = 2000;
        agent.terrainSamples = 50;
    }

    public override void SetUp(FactionManager faction, AIMission newMission)
    {
        base.SetUp(faction, newMission);
        faction.AddSeaForce(this);

        //mission = faction.missions.groundMissions[UnityEngine.Random.Range(0, faction.missions.groundMissions.Count)];
        //Debug.Log("Spawning the sea force " + mission.missionName);

        missionSpawn = VTMapManager.WorldToGlobalPoint(faction.missionPoints.GetRandomSeaSpawnPoint().position);
        missionEnd = VTMapManager.WorldToGlobalPoint(faction.missionPoints.GetRandomSeaRdvPoint().position);


        string vehicle = "";
        if (faction.team == Teams.Allied)
        {
            vehicle = "EscortCruiser";
        }
        else
        {
            vehicle = "DroneMissileCruiser";
        }

        Debug.Log("Spawning a " + vehicle + " at pos: " + missionSpawn.ToString());
        MPUnitSpawnerManager.SpawnUnit(vehicle, this, MPUnitSpawnerManager.UnitType.Sea, missionSpawn, Quaternion.identity, null);

        transform.position = VTMapManager.GlobalToWorldPoint(missionSpawn);
        agent.MoveTo(missionEnd);
    }

    private void FixedUpdate()
    {
        if (ship != null && ship.currPath == null && agent.request.status == CheesesSimplePathfinder.RequestStatus.Generated && arrived == false)
        {
            Debug.Log("Recived path, ordering ship to move!");
            ship.MovePath(agent.request.followPath);
            arrived = true;
        }
    }

    private void OnDestroy() {
        faction.RemoveSeaForce(this);

        if (VTOLMPUtils.IsMultiplayer())
        {
            VTNetworkManager.NetDestroyDelayed(ship.gameObject, 60f);
        }
        else
        {
            Destroy(ship.gameObject, 60f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VTOLVR.Multiplayer;

public class FactionManager
{
    public Teams team;

    public float spawnCooldown;
    public float groundSpawnCooldown;
    public float shipSpawnCooldown;

    public AIMissionGroup missions;
    public MissionPoints missionPoints;

    public List<ForceManager_Aircraft> activeForces;
    public List<ForceManager_Ground> activeGroundForces;
    public List<ForceManager_Sea> activeShipForces;

    public FactionManager(Teams team)
    {
        this.team = team;

        missions = new AIMissionGroup();

        activeForces = new List<ForceManager_Aircraft>();
        activeGroundForces = new List<ForceManager_Ground>();
        activeShipForces = new List<ForceManager_Sea>();

        missionPoints = new MissionPoints();
    }

    public void StartSpawning()
    {
        missionPoints.FindAllMissionPoints(team);
    }

    public void SpawnerUpdate(float deltaTime)
    {
        spawnCooldown -= deltaTime;

        if (activeForces.Count < SettingsManager.settings.maxActiveForces && spawnCooldown < 0)
        {
            if (SpawnRandomAirGroup()) {
                Debug.Log("Spawning was sucsessful!");
                spawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minSpawnTime * 60, SettingsManager.settings.maxSpawnTime * 60);
            }
        }


        groundSpawnCooldown -= deltaTime;

        if (activeGroundForces.Count < SettingsManager.settings.maxActiveGroundForces && groundSpawnCooldown < 0)
        {
            //if (SpawnRandomGroundGroup())
            //{
            //    Debug.Log("Spawning was sucsessful!");
            //    groundSpawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minGroundSpawnTime * 60, SettingsManager.settings.maxGroundSpawnTime * 60);
            //}
        }


        shipSpawnCooldown -= deltaTime;

        if (activeShipForces.Count < SettingsManager.settings.maxActiveGroundForces && shipSpawnCooldown < 0)
        {
            if (SpawnRandomSeaGroup())
            {
                Debug.Log("Spawning was sucsessful!");
                shipSpawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minGroundSpawnTime * 60, SettingsManager.settings.maxGroundSpawnTime * 60);
            }
        }
    }

    public void AddForce(ForceManager_Aircraft force)
    {
        activeForces.Add(force);
    }

    public void RemoveForce(ForceManager_Aircraft force)
    {
        activeForces.Remove(force);
        Debug.Log("Force " + force.forceName + " has been removed.");
    }

    public void AddGroundForce(ForceManager_Ground force)
    {
        activeGroundForces.Add(force);
    }

    public void RemoveGroundForce(ForceManager_Ground force)
    {
        activeGroundForces.Remove(force);
        Debug.Log("Ship force " + force.mission.missionName + " has been removed.");
    }

    public void AddSeaForce(ForceManager_Sea force)
    {
        activeShipForces.Add(force);
    }

    public void RemoveSeaForce(ForceManager_Sea force)
    {
        activeShipForces.Remove(force);
        Debug.Log("Ship force has been removed.");
    }

    private bool SpawnRandomAirGroup()
    {
        if (VTOLMPUtils.IsMultiplayer() && VTOLMPLobbyManager.isLobbyHost == false)
        {
            return false;
        }

        if (missions.missions.Count > 0)
        {
            AIMission mission = missions.missions[UnityEngine.Random.Range(0, missions.missions.Count)];

            if (IsMissionValid(mission) == false)
            {
                return false;
            }
            else
            {
                Debug.Log("Spawning the force " + mission.missionName + " which is going to carry out its mission: " + mission.missionType.ToString());

                GameObject forceObject = new GameObject();
                ForceManager_Aircraft force = forceObject.AddComponent<ForceManager_Aircraft>();
                force.SetUp(this, mission);
                return true;
            }
        }
        else
        {
            Debug.Log("No forces are available, cannot spawn aircraft forces.");
            return false;
        }
    }

    private bool SpawnRandomGroundGroup()
    {
        if (VTOLMPUtils.IsMultiplayer() && VTOLMPLobbyManager.isLobbyHost == false)
        {
            return false;
        }

        if (missions.missions.Count > 0 && missionPoints.groundSpawnWaypoints.Count > 0 && missionPoints.groundRdvWaypoint.Count > 0)
        {
            //AIGroundMission mission = missions.groundMissions[UnityEngine.Random.Range(0, missions.groundMissions.Count)];
            //Debug.Log("Spawning the ground force " + mission.missionName);

            GameObject forceObject = new GameObject();
            ForceManager_Ground force = forceObject.AddComponent<ForceManager_Ground>();

            force.SetUp(this, null);
            return true;
        }
        else
        {
            //Debug.Log("No forces are available, cannot spawn ground forces.");
        }
        return false;
    }

    private bool SpawnRandomSeaGroup()
    {
        if (VTOLMPUtils.IsMultiplayer() && VTOLMPLobbyManager.isLobbyHost == false)
        {
            return false;
        }

        if (missions.missions.Count > 0 && missionPoints.seaSpawnWaypoints.Count > 0 && missionPoints.seaRdvWaypoints.Count > 0)
        {
            //AIGroundMission mission = missions.groundMissions[UnityEngine.Random.Range(0, missions.groundMissions.Count)];
            //Debug.Log("Spawning the ground force " + mission.missionName);

            GameObject forceObject = new GameObject();
            ForceManager_Sea force = forceObject.AddComponent<ForceManager_Sea>();

            force.SetUp(this, null);
            return true;
        }
        else
        {
            //Debug.Log("No forces are available, cannot spawn sea forces.");
        }
        return false;
    }

    public bool IsMissionValid(AIMission mission) {
        if (missionPoints.spawnWaypoints == null || missionPoints.spawnWaypoints.Count() <= 0)
            return false;

        switch (mission.missionType) {
            case AIMissionType.Bombing:
                return missionPoints.bombWaypoints != null && missionPoints.bombWaypoints.Count() > 0;
            case AIMissionType.Strike:
                return missionPoints.strikeWaypoints != null && missionPoints.strikeWaypoints.Count() > 0;
            case AIMissionType.CAP:
                return missionPoints.CAPWaypoints != null && missionPoints.CAPWaypoints.Count() > 0;
            case AIMissionType.Recon:
                return missionPoints.reconWaypoints != null && missionPoints.reconWaypoints.Count() > 0;
            case AIMissionType.Landing:
                return missionPoints.landingWaypoints != null && missionPoints.landingWaypoints.Count() > 0 &&
                    missionPoints.groundRdvWaypoint != null && missionPoints.groundRdvWaypoint.Count() > 0;
            default:
                return false;
        }
    }
}

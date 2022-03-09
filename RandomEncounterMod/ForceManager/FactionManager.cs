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

    public List<ForceManager> activeForces;
    public List<GroundForceManager> activeGroundForces;
    public List<GroundForceManager> activeShipForces;

    public FactionManager(Teams team)
    {
        this.team = team;

        missions = new AIMissionGroup();

        activeForces = new List<ForceManager>();
        activeGroundForces = new List<GroundForceManager>();
        activeShipForces = new List<GroundForceManager>();

        missionPoints = new MissionPoints();
    }

    public void StartSpawning()
    {
        missionPoints.FindAllMissionPoints(team);
    }

    public void SpawnerUpdate(float deltaTime)
    {
        spawnCooldown -= deltaTime;

        if (activeForces.Count < GetMaxAircraft() && spawnCooldown < 0)
        {
            if (SpawnRandomAirGroup()) {
                Debug.Log("Spawning was sucsessful!");
                spawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minSpawnTime * 60, SettingsManager.settings.maxSpawnTime * 60);
            }
        }

        /*
        groundSpawnCooldown -= deltaTime;
        if (activeGroundForces.Count < SettingsManager.settings.maxActiveGroundForces && groundSpawnCooldown < 0 && MissionPointManager.aGroundSpawnWaypoints.Count > 0 && MissionPointManager.aGroundRdvWaypoint.Count > 0)
        {
            groundSpawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minGroundSpawnTime * 60, SettingsManager.settings.maxGroundSpawnTime * 60);
            SpawnRandomGroundGroup(Teams.Allied);
        }
        */
    }

    public float GetMaxAircraft()
    {
        if (SettingsManager.settings.autoBalancing && false)
        {
            int alliedAircraft = 0;
            foreach (Actor actor in TargetManager.instance.allActors) {
                if (actor.team == Teams.Allied && actor.role == Actor.Roles.Air) {
                    alliedAircraft++;
                }
            }

            return alliedAircraft * SettingsManager.settings.enemyRatio;
        }
        else {
            return SettingsManager.settings.maxActiveForces;
        }
    }

    public void AddForce(ForceManager force)
    {
        activeForces.Add(force);
    }

    public void RemoveForce(ForceManager force)
    {
        activeForces.Remove(force);
        Debug.Log("Force " + force.forceName + " has been removed.");
    }

    public void AddGroundForce(GroundForceManager force)
    {
        activeGroundForces.Add(force);
    }

    public void RemoveGroundForce(GroundForceManager force, Teams team)
    {
        activeGroundForces.Remove(force);
        Debug.Log("Ground force " + force.mission.missionName + " has been removed.");
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
            Debug.Log("No forces are available, cannot spawn enemy forces aircraft.");
            return false;
        }
    }

    private void SpawnRandomGroundGroup(Teams team)
    {
        if (VTOLMPUtils.IsMultiplayer() && VTOLMPLobbyManager.isLobbyHost == false)
        {
            return;
        }

        if (missions.missions.Count > 0)
        {
            AIGroundMission mission = missions.groundMissions[UnityEngine.Random.Range(0, missions.groundMissions.Count)];


            Debug.Log("Spawning the ground force " + mission.missionName);

            GameObject forceObject = new GameObject();
            GroundForceManager force = forceObject.AddComponent<GroundForceManager>();

            Waypoint spawn = missionPoints.GetRandomGndSpawnPoint();
            force.SetUp(mission, team, spawn.worldPosition);
        }
        else
        {
            Debug.Log("No forces are available, cannot spawn enemy ground forces.");
        }
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

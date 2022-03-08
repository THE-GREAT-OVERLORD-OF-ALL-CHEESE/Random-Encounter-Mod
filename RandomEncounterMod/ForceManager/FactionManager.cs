using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FactionManager
{
    public float spawnCooldown;
    public float groundSpawnCooldown;
    public float shipSpawnCooldown;

    public float spawnRadius = 50000;//50000
    public float mapRadius;

    public AIMissionGroup missions;

    public List<ForceManager> activeForces;
    public List<GroundForceManager> activeGroundForces;
    public List<GroundForceManager> activeShipForces;

    public Vector3D mapCenter;
    public List<Vector3D> planeSpawns;

    public FactionManager()
    {
        missions = new AIMissionGroup();

        activeForces = new List<ForceManager>();
        activeGroundForces = new List<GroundForceManager>();
        activeShipForces = new List<GroundForceManager>();
    }

    public void SpawnerUpdate(float deltaTime)
    {
        spawnCooldown -= deltaTime;
        if (activeForces.Count < GetMaxAircraft() && spawnCooldown < 0)
        {
            if (SpawnRandomAirGroup()) {
                spawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minSpawnTime * 60, SettingsManager.settings.maxSpawnTime * 60);
            }
        }

        groundSpawnCooldown -= deltaTime;
        if (activeGroundForces.Count < SettingsManager.settings.maxActiveGroundForces && groundSpawnCooldown < 0 && MissionPointManager.aGroundSpawnWaypoints.Count > 0 && MissionPointManager.aGroundRdvWaypoint.Count > 0)
        {
            groundSpawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minGroundSpawnTime * 60, SettingsManager.settings.maxGroundSpawnTime * 60);
            SpawnRandomGroundGroup(Teams.Allied);
        }
    }

    public float GetMaxAircraft()
    {
        if (SettingsManager.settings.autoBalancing)
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
        if (RandomEncounterMod.instance.mpMode && RandomEncounterMod.instance.host == false)
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
                ForceManager force = forceObject.AddComponent<ForceManager>();
                force.SetUp(mission);
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
        if (RandomEncounterMod.instance.mpMode && RandomEncounterMod.instance.host == false)
        {
            return;
        }

        if (missions.missions.Count > 0)
        {
            AIGroundMission mission = missions.groundMissions[UnityEngine.Random.Range(0, missions.groundMissions.Count)];


            Debug.Log("Spawning the ground force " + mission.missionName);

            GameObject forceObject = new GameObject();
            GroundForceManager force = forceObject.AddComponent<GroundForceManager>();

            Waypoint spawn = MissionPointManager.GetRandomGndSpawnPoint(team);
            force.SetUp(mission, team, spawn.worldPosition);
        }
        else
        {
            Debug.Log("No forces are available, cannot spawn enemy ground forces.");
        }
    }

    public bool IsMissionValid(AIMission mission) {
        switch (mission.missionType) {
            case AIMissionType.Bombing:
                return MissionPointManager.bombWaypoints != null && MissionPointManager.bombWaypoints.Count() > 0;
            case AIMissionType.Strike:
                return MissionPointManager.strikeWaypoints != null && MissionPointManager.strikeWaypoints.Count() > 0;
            case AIMissionType.CAP:
                return MissionPointManager.CAPWaypoints != null && MissionPointManager.CAPWaypoints.Count() > 0;
            case AIMissionType.Recon:
                return MissionPointManager.reconWaypoints != null && MissionPointManager.reconWaypoints.Count() > 0;
            case AIMissionType.Landing:
                return MissionPointManager.landingWaypoints != null && MissionPointManager.landingWaypoints.Count() > 0 &&
                    MissionPointManager.eGroundRdvWaypoint != null && MissionPointManager.eGroundRdvWaypoint.Count() > 0;
            default:
                return false;
        }
    }

    public Vector3D GetPlayerPosition()
    {
        if (RandomEncounterMod.instance.akutan == false)
        {
            return new Vector3D(mapRadius, 0, mapRadius);
        }
        else
        {
            return VTMapManager.WorldToGlobalPoint(FlightSceneManager.instance.playerActor.gameObject.transform.position);
        }
    }

    public float GetTrafficRadius()
    {
        if (RandomEncounterMod.instance.akutan == false)
        {
            return mapRadius * 1.4f;
        }
        else
        {
            return spawnRadius;
        }
    }

    public Vector3D PointInCruisingRadius(float alt)
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle;
        Vector3D playerPos = GetPlayerPosition();

        return new Vector3D(randomCircle.x * GetTrafficRadius() + playerPos.x, alt, randomCircle.y * GetTrafficRadius() + playerPos.z); ;
    }

    public Vector3D PointOnCruisingRadius(float alt)
    {
        float bearing = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3D playerPos = GetPlayerPosition();

        return new Vector3D(Mathf.Sin(bearing) * GetTrafficRadius() + playerPos.x, alt, Mathf.Cos(bearing) * GetTrafficRadius() + playerPos.z);
    }

    public Vector3D PointOnMapRadius(float alt)
    {
        float bearing = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float mapRadius = VTMapManager.fetch.map.mapSize * 1500;

        return new Vector3D(Mathf.Sin(bearing) * mapRadius * 1.5f + mapRadius, alt, Mathf.Cos(bearing) * mapRadius * 1.5f + mapRadius);
    }

    public float DistanceFromOrigin(Vector3D otherPos)
    {
        Vector3D playerPos = GetPlayerPosition();
        playerPos.y = 0;
        otherPos.y = 0;
        return (float)(playerPos - otherPos).magnitude;
    }
}

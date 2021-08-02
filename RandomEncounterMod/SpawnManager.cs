using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SpawnManager
{
    public static float spawnCooldown;

    public static float spawnRadius = 50000;//50000
    public static float mapRadius;

    public static AIMissionGroup missions;

    public static List<ForceManager> activeForces;

    public static void SpawnerUpdate(float deltaTime)
    {
        spawnCooldown -= deltaTime;
        if (activeForces.Count < SettingsManager.settings.maxActiveForces && spawnCooldown < 0)
        {
            spawnCooldown = UnityEngine.Random.Range(SettingsManager.settings.minSpawnTime * 60, SettingsManager.settings.maxSpawnTime * 60);
            SpawnRandomAirGroup();
        }
    }

    public static void AddForce(ForceManager force)
    {
        activeForces.Add(force);
    }

    public static void RemoveForce(ForceManager force)
    {
        activeForces.Remove(force);
        Debug.Log("Force " + force.mission.missionName + " has been removed.");
    }

    private static void SpawnRandomAirGroup()
    {
        if (RandomEncounterMod.instance.mpMode && RandomEncounterMod.instance.host == false)
        {
            return;
        }

        if (missions.missions.Count > 0)
        {
            AIMission mission = missions.missions[UnityEngine.Random.Range(0, missions.missions.Count)];


            Debug.Log("Spawning the force " + mission.missionName + " which is going to carry out its mission: " + mission.missionType.ToString());

            GameObject forceObject = new GameObject();
            ForceManager force = forceObject.AddComponent<ForceManager>();
            force.SetUp(mission);
        }
        else
        {
            Debug.Log("No forces are available, cannot spawn enemy forces aircraft.");
        }
    }

    public static Vector3D GetPlayerPosition()
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

    public static float GetTrafficRadius()
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

    public static Vector3D PointInCruisingRadius(float alt)
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle;
        Vector3D playerPos = GetPlayerPosition();

        return new Vector3D(randomCircle.x * GetTrafficRadius() + playerPos.x, alt, randomCircle.y * GetTrafficRadius() + playerPos.z); ;
    }

    public static Vector3D PointOnCruisingRadius(float alt)
    {
        float bearing = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3D playerPos = GetPlayerPosition();

        return new Vector3D(Mathf.Sin(bearing) * GetTrafficRadius() + playerPos.x, alt, Mathf.Cos(bearing) * GetTrafficRadius() + playerPos.z);
    }

    public static Vector3D PointOnMapRadius(float alt)
    {
        float bearing = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float mapRadius = VTMapManager.fetch.map.mapSize * 1500;

        return new Vector3D(Mathf.Sin(bearing) * mapRadius * 1.5f + mapRadius, alt, Mathf.Cos(bearing) * mapRadius * 1.5f + mapRadius);
    }

    public static float DistanceFromOrigin(Vector3D otherPos)
    {
        Vector3D playerPos = GetPlayerPosition();
        playerPos.y = 0;
        otherPos.y = 0;
        return (float)(playerPos - otherPos).magnitude;
    }
}

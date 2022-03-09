using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MissionPoints
{
    public List<Transform> spawnWaypoints;

    public List<Transform> bombWaypoints;
    public List<Transform> strikeWaypoints;
    public List<Transform> CAPWaypoints;
    public List<Transform> reconWaypoints;
    public List<Transform> landingWaypoints;

    public List<Transform> seaSpawnWaypoints;
    public List<Transform> seaRdvWaypoints;

    public List<Waypoint> groundSpawnWaypoints;
    public List<Waypoint> groundRdvWaypoint;

    public void FindAllMissionPoints(Teams team) {
        spawnWaypoints = new List<Transform>();

        bombWaypoints = new List<Transform>();
        strikeWaypoints = new List<Transform>();
        CAPWaypoints = new List<Transform>();
        reconWaypoints = new List<Transform>();
        landingWaypoints = new List<Transform>();

        groundSpawnWaypoints = new List<Waypoint>();
        groundRdvWaypoint = new List<Waypoint>();

        seaSpawnWaypoints = new List<Transform>();
        seaRdvWaypoints = new List<Transform>();

        string opositeTeamPrefix = "";

        switch (team) {
            case Teams.Allied:
                opositeTeamPrefix = "a_";
                break;
            case Teams.Enemy:
                opositeTeamPrefix = "e_";
                break;
        }

        foreach (Waypoint waypoint in VTScenario.current.waypoints.GetWaypoints())
        {
            if (waypoint.GetName().ToLower().Contains(opositeTeamPrefix))
            {
                continue;
            }

            switch (waypoint.GetName().ToLower()) {
                case "spawn":
                case "a_spawn":
                case "e_spawn":
                    spawnWaypoints.Add(waypoint.GetTransform());
                    break;
                case "bomb":
                case "a_bomb":
                case "e_bomb":
                    bombWaypoints.Add(waypoint.GetTransform());
                    break;
                case "strike":
                case "a_strike":
                case "e_strike":
                    strikeWaypoints.Add(waypoint.GetTransform());
                    break;
                case "cap":
                case "a_cap":
                case "e_cap":
                    CAPWaypoints.Add(waypoint.GetTransform());
                    break;
                case "recon":
                case "a_recon":
                case "e_recon":
                    reconWaypoints.Add(waypoint.GetTransform());
                    break;
                case "landing":
                case "a_landing":
                case "e_landing":
                    landingWaypoints.Add(waypoint.GetTransform());
                    break;

                case "gnd_spawn":
                case "a_gnd_spawn":
                case "e_gnd_spawn":
                    groundSpawnWaypoints.Add(waypoint);
                    break;
                case "gnd_rdv":
                case "a_gnd_rdv":
                case "e_gnd_rdv":
                    groundRdvWaypoint.Add(waypoint);
                    break;
                case "sea_spawn":
                case "a_sea_spawn":
                case "e_sea_spawn":
                    seaSpawnWaypoints.Add(waypoint.GetTransform());
                    break;
                case "sea_rdv":
                case "a_sea_rdv":
                case "e_sea_rdv":
                    seaRdvWaypoints.Add(waypoint.GetTransform());
                    break;
            }
        }

        foreach (AirportManager airport in GameObject.FindObjectsOfType<AirportManager>()) {
            if (airport.isCarrier)
                continue;

            if (airport.team != team)
            {
                bombWaypoints.Add(airport.transform);
                strikeWaypoints.Add(airport.transform);
                reconWaypoints.Add(airport.transform);
                landingWaypoints.Add(airport.transform);
            }

            CAPWaypoints.Add(airport.transform);
        }

        if (spawnWaypoints.Count == 0)//if no spawnpoints exist on this map place 36 spawns around the edge of the map so it at least does something
        {
            Vector3D mapCenter = GetCenterPosition();
            
            for (int i = 0; i < 36; i++)
            {
                float heading = i * 10f;
                Vector3D spawnPos = new Vector3D(-Mathf.Sin(heading * Mathf.Deg2Rad) * GetTrafficRadius() + mapCenter.x, 0, -Mathf.Cos(heading * Mathf.Deg2Rad) * GetTrafficRadius() + mapCenter.z);
                GameObject wpt = new GameObject();
                Transform tf = wpt.transform;
                tf.position = VTMapManager.GlobalToWorldPoint(spawnPos);

                spawnWaypoints.Add(tf);
            }
        }

        Debug.Log($"Mission Points found on this map (for team {team.ToString()}): ");
        Debug.Log($"{spawnWaypoints.Count} spawn points");
        Debug.Log($"{bombWaypoints.Count} bombing points");
        Debug.Log($"{strikeWaypoints.Count} strike points");
        Debug.Log($"{CAPWaypoints.Count} CAP points");
        Debug.Log($"{reconWaypoints.Count} recon points");
        Debug.Log($"{landingWaypoints.Count} recon points");

        Debug.Log($"{groundSpawnWaypoints.Count} ground spawn points");
        Debug.Log($"{groundRdvWaypoint.Count} ground rdv points");
        Debug.Log($"{seaSpawnWaypoints.Count} sea spawn points");
        Debug.Log($"{seaRdvWaypoints.Count} sea rdv points");
    }

    public Vector3D GetCenterPosition()
    {
        if (RandomEncounterMod.instance.akutan == false)
        {
            return new Vector3D(VTMapManager.fetch.map.mapSize * 1500, 0, VTMapManager.fetch.map.mapSize * 1500);
        }
        else
        {
            return new Vector3D(32126, 0, 37201);
        }
    }

    private float GetTrafficRadius()
    {
        if (RandomEncounterMod.instance.akutan == false)
        {
            return VTMapManager.fetch.map.mapSize * 1500 * 1.4f;
        }
        else
        {
            return 50000;
        }
    }

    public Vector3D GetRandomBombingPoint()
    {
        return VTMapManager.WorldToGlobalPoint(bombWaypoints[Random.Range(0, bombWaypoints.Count)].position);
    }

    public Vector3D GetRandomStrikePoint()
    {
        return VTMapManager.WorldToGlobalPoint(strikeWaypoints[Random.Range(0, strikeWaypoints.Count)].position);
    }

    public Vector3D GetRandomCAPPoint()
    {
        return VTMapManager.WorldToGlobalPoint(CAPWaypoints[Random.Range(0, CAPWaypoints.Count)].position);
    }

    public Vector3D GetRandomReconPoint()
    {
        return VTMapManager.WorldToGlobalPoint(reconWaypoints[Random.Range(0, reconWaypoints.Count)].position);
    }

    public Vector3D GetRandomLandingPoint()
    {
        return VTMapManager.WorldToGlobalPoint(landingWaypoints[Random.Range(0, landingWaypoints.Count)].position);
    }

    public Waypoint GetRandomGndSpawnPoint()
    {
        return groundSpawnWaypoints[Random.Range(0, groundSpawnWaypoints.Count)];
    }

    public Waypoint GetRandomGndRdvPoint()
    {
        return groundRdvWaypoint[Random.Range(0, groundRdvWaypoint.Count)];
    }

    public Transform GetRandomSeaSpawnPoint()
    {
        return seaSpawnWaypoints[Random.Range(0, seaSpawnWaypoints.Count)];
    }

    public Transform GetRandomSeaRdvPoint()
    {
        return seaRdvWaypoints[Random.Range(0, seaRdvWaypoints.Count)];
    }
}

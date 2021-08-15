using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MissionPointManager
{
    public static List<Transform> bombWaypoints;
    public static List<Transform> strikeWaypoints;
    public static List<Transform> CAPWaypoints;
    public static List<Transform> reconWaypoints;
    public static List<Transform> landingWaypoints;
    public static List<Transform> rondevousWaypoints;

    public static List<Transform> seaSpawnWaypoints;
    public static List<Transform> seaRondevousWaypoints;

    public static List<Waypoint> aGroundSpawnWaypoints;
    public static List<Waypoint> aGroundRdvWaypoint;

    public static List<Waypoint> eGroundSpawnWaypoints;
    public static List<Waypoint> eGroundRdvWaypoint;

    public static void FindAllMissionPoints() {
        bombWaypoints = new List<Transform>();
        strikeWaypoints = new List<Transform>();
        CAPWaypoints = new List<Transform>();
        reconWaypoints = new List<Transform>();
        landingWaypoints = new List<Transform>();
        rondevousWaypoints = new List<Transform>();

        seaSpawnWaypoints = new List<Transform>();
        seaRondevousWaypoints = new List<Transform>();

        aGroundSpawnWaypoints = new List<Waypoint>();
        aGroundRdvWaypoint = new List<Waypoint>();

        eGroundSpawnWaypoints = new List<Waypoint>();
        eGroundRdvWaypoint = new List<Waypoint>();

        foreach (Waypoint waypoint in VTScenario.current.waypoints.GetWaypoints())
        {
            switch (waypoint.GetName()) {
                case "bomb":
                    bombWaypoints.Add(waypoint.GetTransform());
                    break;
                case "strike":
                    strikeWaypoints.Add(waypoint.GetTransform());
                    break;
                case "CAP":
                    CAPWaypoints.Add(waypoint.GetTransform());
                    break;
                case "recon":
                    reconWaypoints.Add(waypoint.GetTransform());
                    break;
                case "landing":
                    reconWaypoints.Add(waypoint.GetTransform());
                    break;

                case "a_gnd_spawn":
                    aGroundSpawnWaypoints.Add(waypoint);
                    break;
                case "a_gnd_rdv":
                    aGroundRdvWaypoint.Add(waypoint);
                    break;
                case "e_gnd_spawn":
                    eGroundSpawnWaypoints.Add(waypoint);
                    break;
                case "e_gnd_rdv":
                    eGroundRdvWaypoint.Add(waypoint);
                    break;
            }
        }

        foreach (AirportManager airport in GameObject.FindObjectsOfType<AirportManager>()) {
            if (airport.team == Teams.Allied) {
                bombWaypoints.Add(airport.transform);
                strikeWaypoints.Add(airport.transform);
                CAPWaypoints.Add(airport.transform);
                reconWaypoints.Add(airport.transform);
                landingWaypoints.Add(airport.transform);
            }
        }

        Debug.Log("Mission Points found on this map: ");
        Debug.Log($"{bombWaypoints.Count} bombing points");
        Debug.Log($"{strikeWaypoints.Count} strike points");
        Debug.Log($"{CAPWaypoints.Count} CAP points");
        Debug.Log($"{reconWaypoints.Count} recon points");

        Debug.Log($"{aGroundSpawnWaypoints.Count} allied ground spawn points");
        Debug.Log($"{aGroundRdvWaypoint.Count} allied ground rdv points");
        Debug.Log($"{eGroundSpawnWaypoints.Count} enemy ground spawn points");
        Debug.Log($"{eGroundRdvWaypoint.Count} enemy ground rdv points");
    }

    public static Vector3D GetRandomBombingPoint()
    {
        return VTMapManager.WorldToGlobalPoint(bombWaypoints[Random.Range(0, bombWaypoints.Count)].position);
    }

    public static Vector3D GetRandomStrikePoint()
    {
        return VTMapManager.WorldToGlobalPoint(strikeWaypoints[Random.Range(0, strikeWaypoints.Count)].position);
    }

    public static Vector3D GetRandomCAPPoint()
    {
        return VTMapManager.WorldToGlobalPoint(CAPWaypoints[Random.Range(0, CAPWaypoints.Count)].position);
    }

    public static Vector3D GetRandomReconPoint()
    {
        return VTMapManager.WorldToGlobalPoint(reconWaypoints[Random.Range(0, reconWaypoints.Count)].position);
    }

    public static Vector3D GetRandomLandingPoint()
    {
        return VTMapManager.WorldToGlobalPoint(landingWaypoints[Random.Range(0, landingWaypoints.Count)].position);
    }

    public static Waypoint GetRandomGndSpawnPoint(Teams team)
    {
        switch (team) {
            case Teams.Allied:
                return aGroundSpawnWaypoints[Random.Range(0, aGroundSpawnWaypoints.Count)];
            case Teams.Enemy:
            default:
                return eGroundSpawnWaypoints[Random.Range(0, eGroundSpawnWaypoints.Count)];
        }
    }

    public static Waypoint GetRandomGndRdvPoint(Teams team)
    {
        switch (team)
        {
            case Teams.Allied:
                return aGroundRdvWaypoint[Random.Range(0, aGroundRdvWaypoint.Count)];
            case Teams.Enemy:
            default:
                return eGroundRdvWaypoint[Random.Range(0, eGroundRdvWaypoint.Count)];
        }
    }
}

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

    public static void FindAllMissionPoints() {
        bombWaypoints = new List<Transform>();
        strikeWaypoints = new List<Transform>();
        CAPWaypoints = new List<Transform>();
        reconWaypoints = new List<Transform>();
        landingWaypoints = new List<Transform>();
        rondevousWaypoints = new List<Transform>();

        seaSpawnWaypoints = new List<Transform>();
        seaRondevousWaypoints = new List<Transform>();
        
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
            }
        }

        foreach (AirportManager airport in GameObject.FindObjectsOfType<AirportManager>()) {
            if (airport.team == Teams.Allied) {
                bombWaypoints.Add(airport.transform);
                strikeWaypoints.Add(airport.transform);
                CAPWaypoints.Add(airport.transform);
                reconWaypoints.Add(airport.transform);
            }
        }

        Debug.Log("Mission Points found on this map: ");
        Debug.Log($"{bombWaypoints.Count} bombing points");
        Debug.Log($"{strikeWaypoints.Count} strike points");
        Debug.Log($"{CAPWaypoints.Count} CAP points");
        Debug.Log($"{reconWaypoints.Count} recon points");
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
}

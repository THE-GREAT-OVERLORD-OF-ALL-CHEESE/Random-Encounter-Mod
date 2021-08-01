using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_Bomb : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;

    public AITask_Bomb(ForceManager force) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        objectivePos = VTMapManager.WorldToGlobalPoint(GameObject.FindObjectOfType<AirportManager>().transform.position);
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "bomb";

        Debug.Log("Setup AITask_Bomb");
    }

    public override void StartTask(ForceAircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.BombWaypoint(waypoint, force.missionDirection, 64, force.mission.altitude);
        aircraft.aircraft.SetEngageEnemies(true);
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return aircraft.pilot.commandState != AIPilot.CommandStates.Orbit;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        GameObject.Destroy(waypoint.GetTransform().gameObject);
    }
}

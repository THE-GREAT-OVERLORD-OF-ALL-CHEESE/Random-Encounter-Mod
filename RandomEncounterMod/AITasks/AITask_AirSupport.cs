using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_AirSupport : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;
    public bool supportComplete;

    public AITask_AirSupport(ForceManager force) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        taskName = "airsupport";

        Debug.Log("Setup AITask_AirSupport");
    }

    public override void StartTask(ForceAircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.SetOrbitNow(waypoint, 5000, force.mission.altitude);
        aircraft.aircraft.SetEngageEnemies(true);
    }

    public override void AgentUpdateTask(ForceAircraft aircraft)
    {
        base.AgentUpdateTask(aircraft);
        if (taskAircraft.Count() == aircraft.force.aircrafts.Count()) {
            supportComplete = true;
        }
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return supportComplete == false;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        GameObject.Destroy(waypoint.GetTransform().gameObject);
    }
}

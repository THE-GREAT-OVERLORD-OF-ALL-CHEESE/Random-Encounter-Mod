using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_FlyToObjective : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;

    public AITask_FlyToObjective(ForceManager force, Vector3D objective) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        objectivePos = objective;
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "fly to objective";

        Debug.Log("Setup AITask_FlyToObjective");
    }

    public override void StartTask(ForceAircraft aircraft)
    {
        base.StartTask(aircraft);
        if (taskAircraft.Count() > 1)
        {
            aircraft.aircraft.SetOrbitNow(waypoint, 1000, force.mission.altitude);
            aircraft.pilot.CommandCancelOverride();
            aircraft.pilot.FormOnPilot(taskAircraft[0].pilot);
        }
        else {
            aircraft.aircraft.SetOrbitNow(waypoint, 1000, force.mission.altitude);
        }
        aircraft.aircraft.SetEngageEnemies(true);

        aircraft.tilter?.SetTiltImmediate(90);
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        Vector3 offset = VTMapManager.GlobalToWorldPoint(objectivePos) - aircraft.transform.position;
        offset.y = 0;
        return offset.magnitude > 20000;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        GameObject.Destroy(waypoint.GetTransform().gameObject);
    }
}

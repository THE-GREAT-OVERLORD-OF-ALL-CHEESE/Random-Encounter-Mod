using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_CAP : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;
    public float capTimer = 300;

    public AITask_CAP(ForceManager force, Vector3D missionTgt) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        objectivePos = missionTgt;
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "cap";

        Debug.Log("Setup AITask_Cap");
    }

    public override void StartTask(ForceAircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.SetOrbitNow(waypoint, 5000, force.mission.altitude);
        aircraft.aircraft.SetEngageEnemies(false);
    }

    public override void UpdateTask()
    {
        base.UpdateTask();
        if (taskAircraft.Count() > 0) {
            capTimer -= Time.fixedDeltaTime;
        }
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return capTimer > 0;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        if (waypoint.GetTransform() != null)
        {
            GameObject.Destroy(waypoint.GetTransform().gameObject);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_RTB : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;

    public AITask_RTB(ForceManager_Aircraft force) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);


        objectivePos = force.missionSpawn;
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);
        Debug.Log("RTB pos: " + objectivePos.ToString());

        taskName = "rtb";

        Debug.Log("Setup AITask_RTB");
    }

    public override void StartTask(ForceUnit_Aircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.SetOrbitNow(waypoint, 1000, force.mission.altitude);
        if (taskAircraft.Count() > 1)
        {
            aircraft.aircraft.SetOrbitNow(waypoint, 1000, force.mission.altitude);
            aircraft.pilot.CommandCancelOverride();
            aircraft.pilot.FormOnPilot(taskAircraft[0].pilot);
        }
        else
        {
            aircraft.aircraft.SetOrbitNow(waypoint, 1000, force.mission.altitude);
        }
        aircraft.aircraft.SetEngageEnemies(false);
    }

    public override bool TaskStatus(ForceUnit_Aircraft aircraft)
    {
        return true;
    }

    public override void AgentUpdateTask(float deltaTime, ForceUnit_Aircraft aircraft) {
        base.AgentUpdateTask(deltaTime, aircraft);
        Vector3 offset = VTMapManager.GlobalToWorldPoint(objectivePos) - aircraft.transform.position;
        offset.y = 0;
        if (offset.magnitude < 5000 && aircraft.health.normalizedHealth > 0) {
            aircraft.health.Kill();
        }
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

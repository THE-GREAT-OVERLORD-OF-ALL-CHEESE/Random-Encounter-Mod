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

    public AITask_RTB(ForceManager force) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);


        Vector3D playerPos = RandomEncounterMod.GetPlayerPosition();
        objectivePos = new Vector3D(-Mathf.Sin(force.missionDirection * Mathf.Deg2Rad) * RandomEncounterMod.GetTrafficRadius() + playerPos.x, 0, -Mathf.Cos(force.missionDirection * Mathf.Deg2Rad) * RandomEncounterMod.GetTrafficRadius() + playerPos.z);
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);
        Debug.Log("RTB pos: " + objectivePos.ToString());

        taskName = "rtb";

        Debug.Log("Setup AITask_RTB");
    }

    public override void StartTask(ForceAircraft aircraft)
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

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return true;
    }

    public override void AgentUpdateTask(ForceAircraft aircraft) {
        Vector3 offset = VTMapManager.GlobalToWorldPoint(objectivePos) - aircraft.transform.position;
        offset.y = 0;
        if (offset.magnitude < 5000 && aircraft.health.normalizedHealth > 0) {
            aircraft.health.Kill();
        }
    }

    public override void Cleanup()
    {
        base.Cleanup();
        GameObject.Destroy(waypoint.GetTransform().gameObject);
    }
}

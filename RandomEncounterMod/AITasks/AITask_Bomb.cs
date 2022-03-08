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

    public AITask_Bomb(ForceManager_Aircraft force, Vector3D missionTgt) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        objectivePos = missionTgt;
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "bomb";

        Debug.Log("Setup AITask_Bomb");
    }

    public override void StartTask(ForceUnit_Aircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.BombWaypoint(waypoint, UnityEngine.Random.Range(0f, 360f), 64, force.mission.altitude);
        aircraft.aircraft.SetEngageEnemies(true);
    }

    public override bool TaskStatus(ForceUnit_Aircraft aircraft)
    {
        return aircraft.pilot.commandState != AIPilot.CommandStates.Orbit;
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

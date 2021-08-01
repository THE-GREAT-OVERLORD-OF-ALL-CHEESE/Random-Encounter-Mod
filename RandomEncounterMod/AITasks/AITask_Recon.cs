﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_Recon : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;
    public float reconTimer;

    public AITask_Recon(ForceManager force) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        reconTimer = UnityEngine.Random.Range(120f, 180f);
        objectivePos = VTMapManager.WorldToGlobalPoint(GameObject.FindObjectOfType<AirportManager>().transform.position);
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "recon";

        Debug.Log("Setup AITask_Recon");
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
            reconTimer -= Time.fixedDeltaTime;
        }
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return reconTimer > 0;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        GameObject.Destroy(waypoint.GetTransform().gameObject);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_Strike : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;
    public float strikeTimer = 300;

    public AITask_Strike(ForceManager force, Vector3D missionTgt) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        objectivePos = missionTgt;
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "strike";

        Debug.Log("Setup AITask_Strike");
    }

    public override void StartTask(ForceAircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.SetOrbitNow(waypoint, 5000, 1000);
        aircraft.aircraft.SetEngageEnemies(true);

        if (aircraft.pilot.combatRole == AIPilot.CombatRoles.FighterAttack || aircraft.pilot.combatRole == AIPilot.CombatRoles.Attack) {
            aircraft.pilot.targetFinder.targetsToFind.ground = true;
            aircraft.pilot.targetFinder.targetsToFind.groundArmor = true;
            aircraft.pilot.targetFinder.targetsToFind.ship = true;
            if (aircraft.pilot.targetFinder.visionRadius < 10000) {
                aircraft.pilot.targetFinder.visionRadius = 10000;
            }
        }
    }
    public override void UpdateTask()
    {
        base.UpdateTask();
        if (taskAircraft.Count() > 0)
        {
            strikeTimer -= Time.fixedDeltaTime;
        }
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return strikeTimer > 0;
    }

    public override void Cleanup()
    {
        base.Cleanup();
        GameObject.Destroy(waypoint.GetTransform().gameObject);
    }
}

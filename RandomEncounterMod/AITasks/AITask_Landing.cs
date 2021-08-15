using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask_Landing : AITask
{
    public Waypoint waypoint;
    public Vector3D objectivePos;
    public float strikeTimer = 300;

    public AITask_Landing(ForceManager force, Vector3D missionTgt) : base(force)
    {
        waypoint = new Waypoint();
        GameObject waypointObject = new GameObject();
        waypointObject.AddComponent<FloatingOriginTransform>();
        waypoint.SetTransform(waypointObject.transform);

        objectivePos = missionTgt;
        waypoint.GetTransform().position = VTMapManager.GlobalToWorldPoint(objectivePos);

        taskName = "landing";

        Debug.Log("Setup AITask_Landing");
    }

    public override void StartTask(ForceAircraft aircraft)
    {
        base.StartTask(aircraft);
        aircraft.aircraft.LandAtWpt(waypoint);
        aircraft.aircraft.SetEngageEnemies(false);

        if (aircraft.pilot.combatRole == AIPilot.CombatRoles.FighterAttack || aircraft.pilot.combatRole == AIPilot.CombatRoles.Attack) {
            aircraft.pilot.targetFinder.targetsToFind.ground = true;
            aircraft.pilot.targetFinder.targetsToFind.groundArmor = true;
            aircraft.pilot.targetFinder.targetsToFind.ship = true;
            if (aircraft.pilot.targetFinder.visionRadius < 10000) {
                aircraft.pilot.targetFinder.visionRadius = 10000;
            }
        }
    }

    public override void AgentUpdateTask(float deltaTime, ForceAircraft aircraft)
    {
        base.AgentUpdateTask(deltaTime, aircraft);

        if (aircraft.pilot.commandState == AIPilot.CommandStates.Park) {
            Debug.Log("AI AV-42 Landed, Spawning infantry!");

            GameObject forceObject = new GameObject();
            GroundForceManager force = forceObject.AddComponent<GroundForceManager>();

            force.SetUp(new AIGroundMission("AV-42 Landed Infantry",
                new List<string> {
                    "EnemySoldierMANPAD",
                    "EnemySoldier",
                    "EnemySoldier",
                    "EnemySoldier",
                    "EnemySoldier",
                    "EnemySoldier",
                    "EnemySoldier",
                    "EnemySoldier"
                },
                GroundSquad.GroundFormations.Vee,
                3),
                Teams.Enemy,
                aircraft.aircraft.actor.position);

            aircraft.aircraft.TakeOff();
        }
    }

    public override bool TaskStatus(ForceAircraft aircraft)
    {
        return (aircraft.pilot.commandState == AIPilot.CommandStates.Orbit || aircraft.pilot.commandState == AIPilot.CommandStates.Combat || aircraft.pilot.commandState == AIPilot.CommandStates.FollowLeader) == false;
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

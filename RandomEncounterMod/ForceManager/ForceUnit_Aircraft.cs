using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ForceUnit_Aircraft : ForceUnit
{
    public ForceManager_Aircraft forceAircraft;

    public AIAircraftSpawn aircraft;
    public AIPilot pilot;
    public Rigidbody rb;
    public TiltController tilter;

    public GearAnimator gearAnimator;
    public Tailhook tailHook;
    public CatapultHook catHook;
    public RefuelPort refuelPort;
    public RotationToggle wingRotator;

    public KinematicPlane kPlane;
    public FuelTank fuelTank;

    public WeaponManager wm;
    public Health health;

    public bool doingTask;
    public int currentTaskID;
    public AITask currentTask;

    protected override void SetUp() {
        aircraft = GetComponent<AIAircraftSpawn>();
        pilot = GetComponent<AIPilot>();
        rb = GetComponent<Rigidbody>();
        //doorAnimator = GetComponentInChildren<Animator>();

        gearAnimator = GetComponentInChildren<GearAnimator>();
        tailHook = GetComponentInChildren<Tailhook>();
        catHook = GetComponentInChildren<CatapultHook>();
        refuelPort = GetComponentInChildren<RefuelPort>();
        wingRotator = pilot.wingRotator;

        kPlane = GetComponent<KinematicPlane>();
        fuelTank = GetComponent<FuelTank>();

        tilter = GetComponent<TiltController>();

        wm = GetComponent<WeaponManager>();
        health = GetComponent<Health>();
        health.OnDeath.AddListener(OnDeath);
    }

    public override void SetForce(ForceManager force) {
        base.SetForce(force);
        forceAircraft = (ForceManager_Aircraft)force;

        gearAnimator?.RetractImmediate();
        tailHook?.RetractHook();
        catHook?.SetState(0);
        wingRotator?.SetDefault();
        refuelPort?.Close();
        tilter?.SetTiltImmediate(90);

        kPlane.SetToKinematic();

        fuelTank?.SetNormFuel(1);

        rb.velocity = transform.forward * forceAircraft.mission.speed;
        aircraft.aiPilot.navSpeed = forceAircraft.mission.speed;
        aircraft.aiPilot.defaultAltitude = forceAircraft.mission.altitude;
        //ai.aircraft.OnPreSpawnUnit();
        kPlane.SetToKinematic();
        kPlane.SetSpeed(forceAircraft.mission.speed);
    }

    private void FixedUpdate() {
        AIUpdate();
    }

    private void AIUpdate() {
        if (currentTaskID < forceAircraft.tasks.Count())
        {
            if (doingTask)
            {
                if (currentTask.TaskStatus(this))
                {
                    currentTask.AgentUpdateTask(Time.fixedDeltaTime, this);
                }
                else
                {
                    currentTask.EndTask(this);
                    Debug.Log(gameObject.name + " of force " + forceAircraft.mission.missionName + " completed task task " + currentTask.taskName + ", id: " + currentTaskID);
                    currentTask = null;
                    doingTask = false;
                    currentTaskID++;
                }
            }
            else {
                doingTask = true;
                currentTask = forceAircraft.tasks[currentTaskID];
                currentTask.StartTask(this);
                Debug.Log(gameObject.name + " of force " + forceAircraft.mission.missionName + " starting task " + currentTask.taskName + ", id: " + currentTaskID);
            }
        }
    }

    protected override void OnDeath() {
        base.OnDeath();
        Destroy(aircraft.unitSpawner.gameObject);
    }
}

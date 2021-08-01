using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ForceAircraft : MonoBehaviour
{
    public ForceManager force;

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

    private void Awake()
    {
        //SetUp();
    }

    private void SetUp() {
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

    public void SetForce(ForceManager force) {
        SetUp();

        this.force = force;
        force.AddAircraft(this);

        gearAnimator?.RetractImmediate();
        tailHook?.RetractHook();
        catHook?.SetState(0);
        wingRotator?.SetDefault();
        refuelPort?.Close();
        tilter?.SetTiltImmediate(90);

        kPlane.SetToKinematic();

        fuelTank?.SetNormFuel(1);
    }

    private void FixedUpdate() {
        AIUpdate();
    }

    private void AIUpdate() {
        if (currentTaskID < force.tasks.Count())
        {
            if (doingTask)
            {
                if (currentTask.TaskStatus(this))
                {
                    currentTask.AgentUpdateTask(this);
                }
                else
                {
                    currentTask.EndTask(this);
                    Debug.Log(gameObject.name + " of force " + force.mission.missionName + " completed task task " + currentTask.taskName + ", id: " + currentTaskID);
                    currentTask = null;
                    doingTask = false;
                    currentTaskID++;
                }
            }
            else {
                doingTask = true;
                currentTask = force.tasks[currentTaskID];
                currentTask.StartTask(this);
                Debug.Log(gameObject.name + " of force " + force.mission.missionName + " starting task " + currentTask.taskName + ", id: " + currentTaskID);
            }
        }
    }

    private void OnDeath() {
        force.RemoveAircraft(this);
        Destroy(aircraft.unitSpawner.gameObject);
    }
}

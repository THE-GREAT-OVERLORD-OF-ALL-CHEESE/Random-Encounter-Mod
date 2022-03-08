using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask
{
    public ForceManager_Aircraft force;

    public List<ForceUnit_Aircraft> taskAircraft;

    public string taskName;

    public AITask(ForceManager_Aircraft force) {
        this.force = force;
        taskAircraft = new List<ForceUnit_Aircraft>();
        taskName = "default task name";

        Debug.Log("Setup AITask");
    } 

    public virtual void StartTask(ForceUnit_Aircraft aircraft) {
        taskAircraft.Add(aircraft);
    }

    public virtual bool TaskStatus(ForceUnit_Aircraft aircraft)
    {
        return false;
    }

    public virtual void UpdateTask(float deltaTime)
    {

    }

    public virtual void AgentUpdateTask(float deltaTime, ForceUnit_Aircraft aircraft)
    {

    }

    public virtual void EndTask(ForceUnit_Aircraft aircraft)
    {
        taskAircraft.Remove(aircraft);
    }

    public virtual void Cleanup() {
        Debug.Log("Clearning up " + taskName);
    }
}
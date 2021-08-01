using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AITask
{
    public ForceManager force;

    public List<ForceAircraft> taskAircraft;

    public string taskName;

    public AITask(ForceManager force) {
        this.force = force;
        taskAircraft = new List<ForceAircraft>();
        taskName = "default task name";

        Debug.Log("Setup AITask");
    } 

    public virtual void StartTask(ForceAircraft aircraft) {
        taskAircraft.Add(aircraft);
    }

    public virtual bool TaskStatus(ForceAircraft aircraft)
    {
        return false;
    }

    public virtual void UpdateTask()
    {

    }

    public virtual void AgentUpdateTask(ForceAircraft aircraft)
    {

    }

    public virtual void EndTask(ForceAircraft aircraft)
    {
        taskAircraft.Remove(aircraft);
    }

    public virtual void Cleanup() {
        Debug.Log("Clearning up " + taskName);
    }
}
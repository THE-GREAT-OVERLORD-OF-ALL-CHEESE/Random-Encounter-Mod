using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;

public class ForceManager : MonoBehaviour
{
    public FactionManager faction;
    public List<ForceUnit> units;

    public string forceName;

    public virtual void SetUp(FactionManager faction, AIMission newMission) {
        this.faction = faction;

        units = new List<ForceUnit>();
    }

    public virtual void AddUnit(ForceUnit unit) {
        units.Add(unit);
    }

    public virtual void RemoveUnit(ForceUnit unit)
    {
        units.Remove(unit);
        if (units.Count == 0) {
            Debug.Log("The force " + forceName + " was completely wiped out.");
            Destroy(gameObject);
        }
    }
}

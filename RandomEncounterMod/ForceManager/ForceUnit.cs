using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ForceUnit : MonoBehaviour
{
    public ForceManager force;

    protected virtual void SetUp() {

    }

    public virtual void SetForce(ForceManager force) {
        SetUp();

        this.force = force;
        force.AddUnit(this);
    }

    protected virtual void OnDeath() {
        force.RemoveUnit(this);
    }
}

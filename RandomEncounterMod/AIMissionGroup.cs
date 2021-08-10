﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AIMissionGroup
{
    public List<AIMission> missions;
    public List<AIGroundMission> groundMissions;

    public AIMissionGroup() {
        missions = new List<AIMission>();
        groundMissions = new List<AIGroundMission>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AIGroundMission
{
    public string missionName;

    public List<string> vehicles;
    public GroundSquad.GroundFormations formation;

    public float speed;

    public AIGroundMission(string missionName, List<string> vehicles, GroundSquad.GroundFormations formation, float speed) {
        this.missionName = missionName;
        this.vehicles = vehicles;
        this.formation = formation;
        this.speed = speed;
    }
}

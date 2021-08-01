using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AIMission
{
    public string missionName;

    public AIMissionType missionType;
    public List<AircraftLoadout> aircraft;

    public float altitude;
    public float speed;

    public AIMission(string missionName, AIMissionType missionType, List<AircraftLoadout> aircraft, float altitude, float speed) {
        this.missionName = missionName;
        this.missionType = missionType;
        this.aircraft = aircraft;
        this.altitude = altitude;
        this.speed = speed;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using System.Reflection;
using Valve.Newtonsoft.Json;
using System.IO;
using UnityEngine.Events;

public class RandomEncounterSettings
{
    public float minSpawnTime = 1;
    public float maxSpawnTime = 3;

    public int maxActiveForces = 3;
}

public class RandomEncounterMod : VTOLMOD
{
    public static RandomEncounterMod instance;


    public static RandomEncounterSettings settings;
    public static AIMissionGroup missions;

    public List<ForceManager> activeForces;


    public bool settingsChanged;

    public UnityAction<float> minSpawnTime_changed;
    public UnityAction<float> maxSpawnTime_changed;
    public UnityAction<int> maxActiveForces_changed;

    public static float spawnRadius = 50000;//50000
    public static float mapRadius;

    public float spawnCooldown;

    public bool mpMode = false;
    public bool host = false;

    public VTOLScenes currentScene;
    public bool akutan = false;

    public override void ModLoaded()
    {
        HarmonyInstance harmony = HarmonyInstance.Create("cheese.randomEncounters");
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Debug.Log("Random encounter patch!");

        instance = this;

        base.ModLoaded();
        VTOLAPI.SceneLoaded += SceneLoaded;
        VTOLAPI.MissionReloaded += MissionReloaded;

        settings = new RandomEncounterSettings();
        LoadSettingsFromFile();

        Settings modSettings = new Settings(this);
        modSettings.CreateCustomLabel("Random Encounter Settings");

        modSettings.CreateCustomLabel("");

        minSpawnTime_changed += minSpawnTime_Setting;
        modSettings.CreateCustomLabel("Minimum Spawn Interval:");
        modSettings.CreateFloatSetting("(Default = 1 minute)", minSpawnTime_changed, settings.minSpawnTime, 0, 60);

        maxSpawnTime_changed += maxSpawnTime_Setting;
        modSettings.CreateCustomLabel("Maxium Spawn Interval:");
        modSettings.CreateFloatSetting("(Default = 3 minutes)", maxSpawnTime_changed, settings.maxSpawnTime, 0, 60);

        maxActiveForces_changed += maxActiveForces_changed;
        modSettings.CreateCustomLabel("Maximum Active Enemy Forces:");
        modSettings.CreateIntSetting("(Default = 3 forces)", maxActiveForces_changed, settings.maxActiveForces, 0, 10);
        VTOLAPI.CreateSettingsMenu(modSettings);

        missions = new AIMissionGroup();
        LoadMissionGroupFromFile();

        activeForces = new List<ForceManager>();
    }

    private void OnApplicationQuit()
    {
        CheckSave();
    }

    void SceneLoaded(VTOLScenes scene)
    {
        CheckSave();
        currentScene = scene;
        switch (scene)
        {
            case VTOLScenes.Akutan:
                akutan = true;
                StartCoroutine("SetupScene");
                break;
            case VTOLScenes.CustomMapBase:
                akutan = true;
                StartCoroutine("SetupScene");
                break;
            default:
                break;
        }
    }

    private void MissionReloaded()
    {
        CheckSave();
        StartCoroutine("SetupScene");
    }

    private IEnumerator SetupScene()
    {
        while (VTMapManager.fetch == null || !VTMapManager.fetch.scenarioReady || FlightSceneManager.instance.switchingScene)
        {
            yield return null;
        }

        LoadMissionGroupFromFile();

        mapRadius = VTMapManager.fetch.map.mapSize * 1500;
        MPCheck();
        spawnCooldown = 0;
        //SetupTasks();
        //InitialSpawnTraffic(targetAircraftAmmount);
    }

    private void FixedUpdate()
    {
        if ((currentScene == VTOLScenes.Akutan || currentScene == VTOLScenes.CustomMapBase || currentScene == VTOLScenes.CustomMapBase_OverCloud))
        {
            SpawnerUpdate();
        }
    }

    private void SpawnerUpdate() {
        spawnCooldown -= Time.fixedDeltaTime;
        if (activeForces.Count < settings.maxActiveForces && spawnCooldown < 0) {
            spawnCooldown = UnityEngine.Random.Range(settings.minSpawnTime * 60, settings.maxSpawnTime * 60);
            SpawnRandomAirGroup();
        }
    }

    private void MPCheck()
    {
        foreach (Mod mod in VTOLAPI.GetUsersMods())
        {
            if (mod.name == "Multiplayer")
            {
                Debug.Log("Random encounters has detected MP, enbling MP mode");
                mpMode = true;
                HostCheck();
                break;
            }
        }
        Debug.Log("The MP mod is not installed.");
    }

    private void HostCheck()
    {
        host = Networker.isHost;
    }

    public void AddForce(ForceManager force) {
        activeForces.Add(force);
    }

    public void RemoveForce(ForceManager force) {
        activeForces.Remove(force);
        Debug.Log("Force " + force.mission.missionName + " has been removed.");
    }

    private void SpawnRandomAirGroup()
    {
        if (mpMode && host == false)
        {
            return;
        }

        if (missions.missions.Count > 0)
        {
            AIMission mission = missions.missions[UnityEngine.Random.Range(0, missions.missions.Count)];


            Debug.Log("Spawning the force " + mission.missionName + " which is going to carry out its mission: " + mission.missionType.ToString());

            GameObject forceObject = new GameObject();
            ForceManager force = forceObject.AddComponent<ForceManager>();
            force.SetUp(mission);
        }
        else
        {
            Debug.Log("No forces are available, cannot spawn enemy forces aircraft.");
        }
    }

    public void MPSetUpAircraft(Actor actor)
    {
        AIManager.setupAIAircraft(actor);
        AIManager.TellClientAboutAI(new Steamworks.CSteamID(0));
    }

    public static Vector3D GetPlayerPosition()
    {
        if (instance.akutan == false)
        {
            return new Vector3D(mapRadius, 0, mapRadius);
        }
        else
        {
            return VTMapManager.WorldToGlobalPoint(FlightSceneManager.instance.playerActor.gameObject.transform.position);
        }
    }

    public static float GetTrafficRadius()
    {
        if (instance.akutan == false)
        {
            return mapRadius * 1.4f;
        }
        else
        {
            return spawnRadius;
        }
    }

    public static Vector3D PointInCruisingRadius(float alt)
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle;
        Vector3D playerPos = GetPlayerPosition();

        return new Vector3D(randomCircle.x * GetTrafficRadius() + playerPos.x, alt, randomCircle.y * GetTrafficRadius() + playerPos.z); ;
    }

    public static Vector3D PointOnCruisingRadius(float alt)
    {
        float bearing = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3D playerPos = GetPlayerPosition();

        return new Vector3D(Mathf.Sin(bearing) * GetTrafficRadius() + playerPos.x, alt, Mathf.Cos(bearing) * GetTrafficRadius() + playerPos.z);
    }

    public static Vector3D PointOnMapRadius(float alt)
    {
        float bearing = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float mapRadius = VTMapManager.fetch.map.mapSize * 1500;

        return new Vector3D(Mathf.Sin(bearing) * mapRadius * 1.5f + mapRadius, alt, Mathf.Cos(bearing) * mapRadius * 1.5f + mapRadius);
    }

    public static float DistanceFromOrigin(Vector3D otherPos)
    {
        Vector3D playerPos = GetPlayerPosition();
        playerPos.y = 0;
        otherPos.y = 0;
        return (float)(playerPos - otherPos).magnitude;
    }

    private void CheckSave()
    {
        Debug.Log("Checking if settings were changed.");
        if (settingsChanged)
        {
            Debug.Log("Settings were changed, saving changes!");
            SaveSettingsToFile();
        }
    }

    public void LoadSettingsFromFile()
    {
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log(address + " exists!");
            try
            {
                Debug.Log("Checking for: " + address + @"\settings.json");
                string temp = File.ReadAllText(address + @"\settings.json");

                settings = JsonConvert.DeserializeObject<RandomEncounterSettings>(temp);
                settingsChanged = false;
                Debug.Log("Loaded: " + address + @"\settings.json");
            }
            catch
            {
                Debug.Log("no json found, making one");
                SaveSettingsToFile();
            }
        }
        else
        {
            Debug.Log("Mod folder not found?");
        }
    }

    public void SaveSettingsToFile()
    {
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log("Saving settings!");
            File.WriteAllText(address + @"\settings.json", JsonConvert.SerializeObject(settings));
            settingsChanged = false;
        }
        else
        {
            Debug.Log("Mod folder not found?");
        }
    }

    public void LoadMissionGroupFromFile()
    {
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log(address + " exists!");
            try
            {
                Debug.Log("Checking for: " + address + @"\enemyForces.json");
                string temp = File.ReadAllText(address + @"\enemyForces.json");

                missions = JsonConvert.DeserializeObject<AIMissionGroup>(temp);
                Debug.Log("Loaded: " + address + @"\enemyForces.json");
            }
            catch
            {
                Debug.Log("no json found, making one");
                missions = DefaultMissions.GenerateDefaultMissions();
                SaveMissionGroupToFile();
            }
        }
        else
        {
            Debug.Log("Mod folder not found?");
        }
    }

    public void SaveMissionGroupToFile()
    {
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log("Saving missions!");
            File.WriteAllText(address + @"\enemyForces.json", JsonConvert.SerializeObject(missions));
        }
        else
        {
            Debug.Log("Mod folder not found?");
        }
    }

    public void minSpawnTime_Setting(float newval)
    {
        settings.minSpawnTime = newval;
        settingsChanged = true;
    }

    public void maxSpawnTime_Setting(float newval)
    {
        settings.maxSpawnTime = newval;
        settingsChanged = true;
    }

    public void maxActiveForces_Setting(int newval)
    {
        settings.maxActiveForces = newval;
        settingsChanged = true;
    }
}
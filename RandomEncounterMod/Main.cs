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


public class RandomEncounterMod : VTOLMOD
{
    public static RandomEncounterMod instance;

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

        SettingsManager.SetupSettingsMenu(this);

        SpawnManager.missions = new AIMissionGroup();
        LoadMissionGroupFromFile();

        SpawnManager.activeForces = new List<ForceManager>();
        SpawnManager.aActiveGroundForces = new List<GroundForceManager>();
        SpawnManager.eActiveGroundForces = new List<GroundForceManager>();
    }

    private void OnApplicationQuit()
    {
        SettingsManager.CheckSave();
    }

    void SceneLoaded(VTOLScenes scene)
    {
        SettingsManager.CheckSave();
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
        SettingsManager.CheckSave();
        StartCoroutine("SetupScene");
    }

    private IEnumerator SetupScene()
    {
        while (VTMapManager.fetch == null || !VTMapManager.fetch.scenarioReady || FlightSceneManager.instance.switchingScene)
        {
            yield return null;
        }

        LoadMissionGroupFromFile();

        SpawnManager.mapRadius = VTMapManager.fetch.map.mapSize * 1500;
        MPCheck();
        SpawnManager.spawnCooldown = SettingsManager.settings.delaySpawnTime * 60;
        SpawnManager.eGroundSpawnCooldown = SettingsManager.settings.delayGroundSpawnTime * 60;
        SpawnManager.aGroundSpawnCooldown = 0; //Set allied ground units to spawn immediately
        Debug.Log("Set the Initial delay to... Air: " + SpawnManager.spawnCooldown + ", Ground: " + SpawnManager.eGroundSpawnCooldown);

        MissionPointManager.FindAllMissionPoints();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Reloading settings from file!");
            SettingsManager.LoadSettingsFromFile();
            LoadMissionGroupFromFile();
        }
    }

    private void FixedUpdate()
    {
        if ((currentScene == VTOLScenes.Akutan || currentScene == VTOLScenes.CustomMapBase || currentScene == VTOLScenes.CustomMapBase_OverCloud))
        {
            SpawnManager.SpawnerUpdate(Time.fixedDeltaTime);
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

    public void MPSetUpAircraft(Actor actor)
    {
        AIManager.setupAIAircraft(actor);
        AIManager.TellClientAboutAI(new Steamworks.CSteamID(0));
    }

    public void SaveMissionGroupToFile()
    {
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log("Saving missions!");
            File.WriteAllText(address + @"\enemyForces.json", JsonConvert.SerializeObject(SpawnManager.missions));
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

                SpawnManager.missions = JsonConvert.DeserializeObject<AIMissionGroup>(temp);
                Debug.Log("Loaded: " + address + @"\enemyForces.json");
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
                Debug.Log("json not found or invalid, making new one");
                SpawnManager.missions = DefaultMissions.GenerateDefaultMissions();
                SaveMissionGroupToFile();
            }
        }
        else
        {
            Debug.Log("Mod folder not found?");
        }
    }
}
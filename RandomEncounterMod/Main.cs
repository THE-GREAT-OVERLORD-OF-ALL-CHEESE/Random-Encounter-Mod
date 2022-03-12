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
using VTOLVR.Multiplayer;

public class RandomEncounterMod : VTOLMOD
{
    public static RandomEncounterMod instance;

    public VTOLScenes currentScene;
    public bool akutan = false;

    public AIFactionMissions allMissions;

    public FactionManager allied;
    public FactionManager enemy;

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

        allied = new FactionManager(Teams.Allied);
        enemy = new FactionManager(Teams.Enemy);
        LoadMissionGroupFromFile();
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

        //allied.spawnCooldown = SettingsManager.settings.delaySpawnTime * 60;
        //allied.groundSpawnCooldown = SettingsManager.settings.delayGroundSpawnTime * 60;
        //enemy.spawnCooldown = SettingsManager.settings.delaySpawnTime * 60;
        //enemy.groundSpawnCooldown = SettingsManager.settings.delayGroundSpawnTime * 60;
        Debug.Log("Set the Initial delay to... Air: " + allied.spawnCooldown + ", Ground: " + allied.groundSpawnCooldown);

        GameObject pathfinderObj = new GameObject();
        pathfinderObj.AddComponent<CheesesSimplePathfinder>();

        allied.StartSpawning();
        enemy.StartSpawning();
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
            if (VTOLMPUtils.IsMultiplayer() && VTOLMPLobbyManager.isLobbyHost == false)
            {
                return;
            }

            allied.SpawnerUpdate(Time.fixedDeltaTime);
            enemy.SpawnerUpdate(Time.fixedDeltaTime);
        }
    }

    public void SaveMissionGroupToFile()
    {
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log("Saving missions!");
            File.WriteAllText(address + @"\forces.json", JsonConvert.SerializeObject(allMissions));
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
                Debug.Log("Checking for: " + address + @"\forces.json");
                string temp = File.ReadAllText(address + @"\forces.json");

                allMissions = JsonConvert.DeserializeObject<AIFactionMissions>(temp);
                Debug.Log("Loaded: " + address + @"\forces.json");
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
                Debug.Log("json not found or invalid, making new one");
                allMissions = new AIFactionMissions();
                allMissions.alliedMissions = DefaultMissions.GenerateDefaultAlliedMissions();
                allMissions.enemyMissions = DefaultMissions.GenerateDefaultEnemyMissions();
                SaveMissionGroupToFile();
            }
        }
        else
        {
            Debug.Log("Mod folder not found?");
        }

        allied.missions = allMissions.alliedMissions;
        enemy.missions = allMissions.enemyMissions;
    }
}
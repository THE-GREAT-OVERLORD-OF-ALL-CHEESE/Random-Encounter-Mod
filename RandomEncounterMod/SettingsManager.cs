﻿using System;
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

    public bool autoBalancing = false;
    public float enemyRatio = 1.5f;
}

public static class SettingsManager
{
    public static VTOLMOD mod;

    public static RandomEncounterSettings settings;

    public static bool settingsChanged;

    public static UnityAction<float> minSpawnTime_changed;
    public static UnityAction<float> maxSpawnTime_changed;

    public static UnityAction<int> maxActiveForces_changed;

    public static UnityAction<bool> autoBalancing_changed;
    public static UnityAction<float> enemyRatio_changed;

    public static void SetupSettingsMenu(VTOLMOD mod) {
        SettingsManager.mod = mod;

        settings = new RandomEncounterSettings();
        LoadSettingsFromFile();

        Settings modSettings = new Settings(mod);
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

        autoBalancing_changed += autoBalancing_Setting;
        modSettings.CreateCustomLabel("Autobalance the teams based on the number of players:");
        modSettings.CreateBoolSetting("(Default = false)", autoBalancing_changed, settings.autoBalancing);

        enemyRatio_changed += enemyRatio_Setting;
        modSettings.CreateCustomLabel("Enemy Ratio (enemy groups per player):");
        modSettings.CreateFloatSetting("(Default = false)", enemyRatio_changed, settings.enemyRatio, 0, 10);

        VTOLAPI.CreateSettingsMenu(modSettings);
    }

    public static void CheckSave()
    {
        Debug.Log("Checking if settings were changed.");
        if (settingsChanged)
        {
            Debug.Log("Settings were changed, saving changes!");
            SaveSettingsToFile();
        }
    }

    public static void SaveSettingsToFile()
    {
        string address = mod.ModFolder;
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

    public static void LoadSettingsFromFile()
    {
        string address = mod.ModFolder;
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

    public static void minSpawnTime_Setting(float newval)
    {
        settings.minSpawnTime = newval;
        settingsChanged = true;
    }

    public static void maxSpawnTime_Setting(float newval)
    {
        settings.maxSpawnTime = newval;
        settingsChanged = true;
    }

    public static void maxActiveForces_Setting(int newval)
    {
        settings.maxActiveForces = newval;
        settingsChanged = true;
    }

    public static void autoBalancing_Setting(bool newval)
    {
        settings.autoBalancing = newval;
        settingsChanged = true;
    }


    public static void enemyRatio_Setting(float newval)
    {
        settings.enemyRatio = newval;
        settingsChanged = true;
    }
}

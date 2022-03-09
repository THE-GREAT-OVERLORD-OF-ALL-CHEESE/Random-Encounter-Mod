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
    public float delaySpawnTime = 0;

    public int maxActiveForces = 3;

    public bool autoBalancing = false;
    public float enemyRatio = 1.5f;

    public bool giveRWR = true;

    public float minGroundSpawnTime = 1;
    public float maxGroundSpawnTime = 3;
    public float delayGroundSpawnTime = 0;

    public int maxActiveGroundForces = 5;
}

public static class SettingsManager
{
    public static VTOLMOD mod;

    public static RandomEncounterSettings settings;

    public static bool settingsChanged;

    public static UnityAction<float> minSpawnTime_changed;
    public static UnityAction<float> maxSpawnTime_changed;
    public static UnityAction<float> delaySpawnTime_changed;

    public static UnityAction<int> maxActiveForces_changed;

    public static UnityAction<bool> autoBalancing_changed;
    public static UnityAction<float> enemyRatio_changed;

    public static UnityAction<bool> giveRWR_changed;

    public static UnityAction<float> minGroundSpawnTime_changed;
    public static UnityAction<float> maxGroundSpawnTime_changed;
    public static UnityAction<float> delayGroundSpawnTime_changed;

    public static UnityAction<int> maxActiveGroundForces_changed;

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

        delaySpawnTime_changed += delaySpawnTime_Setting;
        modSettings.CreateCustomLabel("Delay Spawn Time:");
        modSettings.CreateFloatSetting("(Default = 2 minutes)", delaySpawnTime_changed, settings.delaySpawnTime, 0, 60);

        maxActiveForces_changed += maxActiveForces_Setting;
        modSettings.CreateCustomLabel("Maximum Active Air Forces:");
        modSettings.CreateIntSetting("(Default = 3 forces)", maxActiveForces_changed, settings.maxActiveForces, 0, 10);

        autoBalancing_changed += autoBalancing_Setting;
        modSettings.CreateCustomLabel("Autobalance the teams based on the number of players:");
        modSettings.CreateBoolSetting("(Default = false)", autoBalancing_changed, settings.autoBalancing);

        enemyRatio_changed += enemyRatio_Setting;
        modSettings.CreateCustomLabel("Enemy Ratio (enemy groups per player):");
        modSettings.CreateFloatSetting("(Default = false)", enemyRatio_changed, settings.enemyRatio, 0, 10);

        giveRWR_changed += giveRWR_Setting;
        modSettings.CreateCustomLabel("Modifies the ASF to give them RWRs:");
        modSettings.CreateBoolSetting("(Default = true)", giveRWR_changed, settings.giveRWR);

        minGroundSpawnTime_changed += minGroundSpawnTime_Setting;
        modSettings.CreateCustomLabel("Minimum Ground Spawn Interval:");
        modSettings.CreateFloatSetting("(Default = 0 minute)", minGroundSpawnTime_changed, settings.minGroundSpawnTime, 0, 60);

        maxGroundSpawnTime_changed += maxGroundSpawnTime_Setting;
        modSettings.CreateCustomLabel("Maxium Ground Spawn Interval:");
        modSettings.CreateFloatSetting("(Default = 1 minutes)", maxGroundSpawnTime_changed, settings.maxGroundSpawnTime, 0, 60);

        delayGroundSpawnTime_changed += delayGroundSpawnTime_Setting;
        modSettings.CreateCustomLabel("Delay Ground Spawn Time:");
        modSettings.CreateFloatSetting("(Default = 2 minutes)", delayGroundSpawnTime_changed, settings.delayGroundSpawnTime, 0, 60);

        maxActiveForces_changed += maxActiveForces_Setting;
        modSettings.CreateCustomLabel("Maximum Active Enemy Ground Forces:");
        modSettings.CreateIntSetting("(Default = 5 forces)", maxActiveGroundForces_changed, settings.maxActiveGroundForces, 0, 10);

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
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
                Debug.Log("json not found or invalid, making new one");
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
        settings.delaySpawnTime = newval;
        settingsChanged = true;
    }

    public static void delaySpawnTime_Setting(float newval)
    {
        settings.delaySpawnTime = newval;
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

    public static void giveRWR_Setting(bool newval)
    {
        settings.giveRWR = newval;
        settingsChanged = true;
    }

    public static void minGroundSpawnTime_Setting(float newval)
    {
        settings.minGroundSpawnTime = newval;
        settingsChanged = true;
    }

    public static void maxGroundSpawnTime_Setting(float newval)
    {
        settings.maxGroundSpawnTime = newval;
        settingsChanged = true;
    }

    public static void delayGroundSpawnTime_Setting(float newval)
    {
        settings.delayGroundSpawnTime = newval;
        settingsChanged = true;
    }

    public static void maxActiveGroundForces_Setting(int newval)
    {
        settings.maxActiveGroundForces = newval;
        settingsChanged = true;
    }
}

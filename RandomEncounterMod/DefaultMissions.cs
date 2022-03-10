using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DefaultMissions
{
    public static AIMissionGroup GenerateDefaultAlliedMissions() {
        AIMissionGroup missionGroup = new AIMissionGroup();

        #region recon missions
        missionGroup.missions.Add(new AIMission("MQ-31 Recon",
            AIMissionType.Recon,
            new List<AircraftLoadout>() {
                new AircraftLoadout("MQ-31")
            },
            5000,
            200));

        missionGroup.missions.Add(new AIMission("MQ-31 LR Recon",
            AIMissionType.Recon,
            new List<AircraftLoadout>() {
                new AircraftLoadout("MQ-31", new string[]{ "mq31-46lt", "mq31-46lt"})
            },
            5000,
            200));
        #endregion

        /*
        //Strike F/A-26B
        new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "af_amraam", "af_amraam", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_gbu12x3", "af_tgp", ""});

        //Bomber F/A-26B
        new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""});

        //Glide Bomber F/A-26B
        new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu39x4uFront", "fa26_gbu39x4uRear", "fa26_gbu39x4uRear", "fa26_gbu39x4uFront", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""});

        //Chute Bomber F/A-26B
        new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_mk82HDx3", "fa26_mk82HDx3", "fa26_mk82HDx3", "fa26_mk82HDx3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_mk82HDx3", "fa26_mk82HDx3", "fa26_mk82HDx3", "af_tgp", ""});

        //Cluster Strike F/A-26B
        new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "h70-x7ld", "fa26_maverickx1", "fa26_cagm-6", "af_amraam", "af_amraam", "fa26_cagm-6", "fa26_maverickx1", "h70-x7ld", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_gbu12x3", "af_tgp", ""});

        //Antiship F/A-26B
        new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "af_amraam", "af_amraam", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_agm89x1", "af_tgp", ""});

        //Stock B-11
        new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"});

        //Low Alt B-11
        new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"});

        //Antiship B-11
        new AircraftLoadout("ABomberAI", new string[] { "abomber_agm89x2", "abomber_agm89x2", "abomber_agm89x2", "abomber_agm89x2"});

        //Stock AV-42C
        new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""});

        //Antiship AV-42C
        new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "agm89x1", "agm89x1"});

        //Cluster AV-42C
        new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "cagm-6", "h70-4x4", "h70-4x4", "cagm-6", "", ""});
        
        //Stock F-45
        new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "", "", "", "", "", ""});

        //CAP F-45
        new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"});

        //Stealth Strike F-45
        new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "", "", "", "", "", ""});

        //Strike F-45
        new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "f45_aim9x1", "f45-agm145x3", "f45_amraamRail", "f45_amraamRail", "f45-agm145x3", "f45_aim9x1"});

        //Stealth Bomber F-45
        new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-gbu39", "f45-gbu39", "f45_sidewinderx2", "", "", "", "", "", ""});

        //Bomber F-45
        new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_gbu38x4Internal", "f45_gbu38x4Internal", "f45_sidewinderx2", "f45_aim9x1", "f45_gbu38x1", "f45_gbu38x1", "f45_gbu38x1", "f45_gbu38x1", "f45_aim9x1"});
        */

        #region bombing missions
        //bombing missions
        missionGroup.missions.Add(new AIMission("2x Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"})
            },
            8000,
            200));

        missionGroup.missions.Add(new AIMission("2x Low Alt Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"})
            },
            8000,
            200));

        missionGroup.missions.Add(new AIMission("2x F/A-26B Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""})
            },
            8000,
            250));

        missionGroup.missions.Add(new AIMission("3x F/A-26B Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_amraamRail", "af_aim9", "af_aim9", "fa26_gbu38x3", "fa26_gbu38x3", "fa26_gbu38x3", "af_tgp", ""})
            },
            8000,
            250));
        #endregion

        #region bombers with escorts
        missionGroup.missions.Add(new AIMission("Bomber w/ F/A-26B escort",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
            },
            8000,
            200));

        missionGroup.missions.Add(new AIMission("Low Alt Bomber w/ F/A-26B escort",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
            },
            1000,
            200));

        missionGroup.missions.Add(new AIMission("Bomber w/ 2x F/A-26B escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("Bomber w/ 2x F-45 escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("3x Bomber w/ 2x F/A-26B escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x F-45 escorts",
        AIMissionType.Bombing,
        new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"})
        },
        5000,
        200));
        #endregion

        #region strike missions
        missionGroup.missions.Add(new AIMission("2x AV-42C Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("3x AV-42C Strikers",
        AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("2x F/A-26B Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("3x F/A-26B Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x F-45A Stealth Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "", "", "", "", "", ""}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "", "", "", "", "", ""})
           },
           1000,
           200));

        missionGroup.missions.Add(new AIMission("2x F-45A Stealth Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "", "", "", "", "", ""}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "", "", "", "", "", ""}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "", "", "", "", "", ""})
           },
           1000,
           200));

        missionGroup.missions.Add(new AIMission("2x F-45A Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "f45_aim9x1", "f45-agm145x3", "f45_amraamRail", "f45_amraamRail", "f45-agm145x3", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "f45_aim9x1", "f45-agm145x3", "f45_amraamRail", "f45_amraamRail", "f45-agm145x3", "f45_aim9x1"})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("3x F-45A Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "f45_aim9x1", "f45-agm145x3", "f45_amraamRail", "f45_amraamRail", "f45-agm145x3", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "f45_aim9x1", "f45-agm145x3", "f45_amraamRail", "f45_amraamRail", "f45-agm145x3", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45-agm145I", "f45-agm145I", "f45_sidewinderx2", "f45_aim9x1", "f45-agm145x3", "f45_amraamRail", "f45_amraamRail", "f45-agm145x3", "f45_aim9x1"})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("3x AV-42C Strikers w/ 2x F/A-26B Escort",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
                },
           1000,
           200));

        missionGroup.missions.Add(new AIMission("3x AV-42C Strikers w/ 2x F-45A Escort",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"})
                },
           1000,
           200));
        #endregion

        #region cluster strike mission
        missionGroup.missions.Add(new AIMission("2x AV-42C Cluster Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "cagm-6", "h70-4x4", "h70-4x4", "cagm-6", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "cagm-6", "h70-4x4", "h70-4x4", "cagm-6", "", ""})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("2x F/A-26B Cluster Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "h70-x7ld", "fa26_maverickx1", "fa26_cagm-6", "af_amraam", "af_amraam", "fa26_cagm-6", "fa26_maverickx1", "h70-x7ld", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_gbu12x3", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "h70-x7ld", "fa26_maverickx1", "fa26_cagm-6", "af_amraam", "af_amraam", "fa26_cagm-6", "fa26_maverickx1", "h70-x7ld", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_gbu12x3", "af_tgp", ""})
           },
           3500,
           250));
        #endregion

        #region CAP missions
        missionGroup.missions.Add(new AIMission("2x AV-42C CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x AV-42C CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x F/A-26B CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x F/A-26B CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x F-45A Stealth CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "", "", "", "", "", ""}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "", "", "", "", "", ""}),
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x F-45A Stealth CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "", "", "", "", "", ""}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "", "", "", "", "", ""}),
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x F-45A CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x F-45A CAP",
          AIMissionType.CAP,
          new List<AircraftLoadout>() {
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"}),
                new AircraftLoadout("F-45A AI", new string[] { "f45_gun", "f45_sidewinderx2", "f45_amraamInternal", "f45_amraamInternal", "f45_sidewinderx2", "f45_aim9x1", "f45_amraamRail", "f45_amraamRail", "f45_amraamRail", "f45_aim9x1", "f45_aim9x1"})
          },
          3500,
          250));
        #endregion

        #region landing missions
        missionGroup.missions.Add(new AIMission("2x AV-42 Landing",
           AIMissionType.Landing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "m230", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "m230", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("AV-42 Landing w/ AV-42 Escort",
           AIMissionType.Landing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "m230", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""}),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("AV-42 Landing w/ F/A-26B Escort",
           AIMissionType.Landing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "m230", "hellfirex4", "h70-4x4", "h70-4x4", "hellfirex4", "", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));
        #endregion

        #region ground units
        missionGroup.groundMissions.Add(new AIGroundMission("4x Tanks 2",
            new List<string>() {
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1"
            },
            GroundSquad.GroundFormations.Vee,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("8x Tanks 2",
            new List<string>() {
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1",
                "alliedMBT1"
            },
            GroundSquad.GroundFormations.Vee,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("4x Rocket Artillery",
            new List<string>() {
                "ARocketTruck",
                "ARocketTruck",
                "ARocketTruck",
                "ARocketTruck"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("Rocket Escort 2",
            new List<string>() {
                "alliedMBT1",
                "alliedMBT1",
                "ARocketTruck",
                "ARocketTruck",
                "alliedMBT1",
                "alliedMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("Rocket Escort 3",
            new List<string>() {
                "SAAW",
                "ARocketTruck",
                "ARocketTruck",
                "ARocketTruck",
                "ARocketTruck"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("Arty Escort",
            new List<string>() {
                "alliedMBT1",
                "alliedMBT1",
                "Artillery",
                "Artillery",
                "Artillery",
                "Artillery",
                "alliedMBT1",
                "alliedMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("Phallanx Escort",
            new List<string>() {
                "alliedMBT1",
                "alliedMBT1",
                "SRADTruck",
                "PhallanxTruck",
                "alliedMBT1",
                "alliedMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("SAAW Escort",
            new List<string>() {
                "alliedMBT1",
                "alliedMBT1",
                "SRADTruck",
                "alliedMBT1",
                "alliedMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));
        #endregion

        return missionGroup;
    }

    public static AIMissionGroup GenerateDefaultEnemyMissions()
    {
        AIMissionGroup missionGroup = new AIMissionGroup();

        #region recon missions
        missionGroup.missions.Add(new AIMission("AIUCAV Recon",
            AIMissionType.Recon,
            new List<AircraftLoadout>() {
                new AircraftLoadout("AIUCAV")
            },
            5000,
            200));
        #endregion
        
        #region bombing missions
        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ ASF-58 escort",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""})
            },
            8000,
            200));

        missionGroup.missions.Add(new AIMission("2x Enemy Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"})
            },
            8000,
            200));

        missionGroup.missions.Add(new AIMission("2x ASF-30 Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1"})
            },
            8000,
            250));

        missionGroup.missions.Add(new AIMission("3x ASF-30 Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1", "asf-srmx1", "sb1x3", "sb1x3", "sb1x3", "asf-srmx1"})
            },
            8000,
            250));
        #endregion

        #region bombers with escorts
        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ 2x ASF-30 escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ 2x ASF-33 escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank"})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ 2x ASF-58 escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x ASF-30 escorts",
           AIMissionType.Bombing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"})
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x ASF-33 escorts",
        AIMissionType.Bombing,
        new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank"})
        },
        5000,
        200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x ASF-58 escorts",
        AIMissionType.Bombing,
        new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""})
        },
        5000,
        200));
        #endregion

        #region strike missions
        missionGroup.missions.Add(new AIMission("2x ASF-30 Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25","asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25","asf-srmx1"})
           },
           1000,
           250));

        missionGroup.missions.Add(new AIMission("3x ASF-30 Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25","asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25","asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25","asf-srmx1"})
           },
           1000,
           250));

        missionGroup.missions.Add(new AIMission("2x GAV-25 Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"})
                },
           1000,
           200));

        missionGroup.missions.Add(new AIMission("3x GAV-25 Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"})
                },
           1000,
           200));

        missionGroup.missions.Add(new AIMission("3x UCAV Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("5x UCAV Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("8x UCAV Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"})
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("3x GAV-25 Strikers w/ 2x ASF-30 Escort",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"})
                },
           1000,
           200));

        missionGroup.missions.Add(new AIMission("3x GAV-25 Strikers w/ 2x ASF-33 Escort",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""})
                },
           1000,
           200));
        #endregion

        #region CAP missions
        missionGroup.missions.Add(new AIMission("2x ASF-30 CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x ASF-30 CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-33",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-33 LR",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank"})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x ASF-33",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-58",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", ""})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-58 LR",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "asf58_droptank", "asf58_droptank"}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "asf58_droptank", "asf58_droptank"})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("ASF-30 w/ UCAV Wingmen",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("ASF-33 w/ UCAV Wingmen",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire"}),
           },
           3500,
           250));
        #endregion

        #region landing missions
        missionGroup.missions.Add(new AIMission("2x GAV Landing",
           AIMissionType.Landing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
               new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("GAV Landing w/ ASF-30 Escort",
           AIMissionType.Landing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1"})
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("GAV Landing w/ ASF-33 Escort",
           AIMissionType.Landing,
           new List<AircraftLoadout>() {
                new AircraftLoadout("GAV-25", new string[] { "gav_gun", "gma-14x3", "wr-25", "wr-25", "gma-6x2", "gma-6x2", "wr-25", "wr-25", "gma-14x3"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", ""})
           },
           3500,
           250));
        #endregion

        #region ground units
        missionGroup.groundMissions.Add(new AIGroundMission("4x Tanks",
            new List<string>() {
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1"
            },
            GroundSquad.GroundFormations.Vee,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("8x Tanks",
            new List<string>() {
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1",
                "enemyMBT1"
            },
            GroundSquad.GroundFormations.Vee,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("4x Rocket Artillery 2",
            new List<string>() {
                "ERocketTruck",
                "ERocketTruck",
                "ERocketTruck",
                "ERocketTruck"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("Rocket Escort",
            new List<string>() {
                "enemyMBT1",
                "enemyMBT1",
                "ERocketTruck",
                "ERocketTruck",
                "enemyMBT1",
                "enemyMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("Rocket Escort 3",
            new List<string>() {
                "SAAW",
                "ERocketTruck",
                "ERocketTruck",
                "ERocketTruck",
                "ERocketTruck"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("IRAPC Escort",
            new List<string>() {
                "enemyMBT1",
                "enemyMBT1",
                "IRAPC",
                "IRAPC",
                "enemyMBT1",
                "enemyMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));

        missionGroup.groundMissions.Add(new AIGroundMission("SAAW Escort",
            new List<string>() {
                "enemyMBT1",
                "enemyMBT1",
                "SAAW",
                "enemyMBT1",
                "enemyMBT1"
            },
            GroundSquad.GroundFormations.Column,
            30
            ));
        #endregion

        return missionGroup;
    }
}
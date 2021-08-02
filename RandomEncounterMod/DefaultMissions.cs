using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DefaultMissions
{
    public static AIMissionGroup GenerateDefaultMissions() {
        AIMissionGroup missionGroup = new AIMissionGroup();


        //recon missions
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

        missionGroup.missions.Add(new AIMission("AIUCAV Recon",
            AIMissionType.Recon,
            new List<AircraftLoadout>() {
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "", "", "", "" })
            },
            5000,
            200));


        //bombing missions
        missionGroup.missions.Add(new AIMission("Enemy Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"})
            },
            8000,
            200));

        missionGroup.missions.Add(new AIMission("2x Enemy Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack" }),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack" })
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

        missionGroup.missions.Add(new AIMission("Low Alt Bomber",
            AIMissionType.Bombing,
            new List<AircraftLoadout>() {
                new AircraftLoadout("ABomberAI", new string[] { "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack", "abomber_mk82AIRRack" })
            },
            1000,
            200));

        //bombers with escorts
        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ 2x ASF-30 escorts",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" }),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" })
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ 2x ASF-33 escorts",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank" })
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("Enemy Bomber w/ 2x ASF-58 escorts",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", "" }),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", "" })
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x ASF-30 escorts",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" }),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" })
           },
           5000,
           200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x ASF-33 escorts",
        AIMissionType.Strike,
        new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank" })
        },
        5000,
        200));

        missionGroup.missions.Add(new AIMission("3x Enemy Bomber w/ 2x ASF-58 escorts",
        AIMissionType.Strike,
        new List<AircraftLoadout>() {
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("EBomberAI", new string[] { "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack", "ebomber_stdRack"}),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", "" }),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", "" })
        },
        5000,
        200));

        //strike missions
        missionGroup.missions.Add(new AIMission("2x ASF-30 Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25" ,"asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25" ,"asf-srmx1"})
           },
           1000,
           250));

        missionGroup.missions.Add(new AIMission("3x ASF-30 Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25" ,"asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25" ,"asf-srmx1"}),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "wr-25", "wr-25", "wr-25", "asf-srmx1", "asf-srmx1", "wr-25", "wr-25", "wr-25" ,"asf-srmx1"})
           },
           1000,
           250));

        /*missionGroup.missions.Add(new AIMission("2x GAV-25 Strikers",
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
           200));*/

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
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" })
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("8x UCAV Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" }),
                new AircraftLoadout("AIUCAV", new string[] { "eucav_gun", "eucav_hellfire",  "eucav_hellfire", "eucav_hellfire", "eucav_hellfire" })
           },
           3500,
           200));

        missionGroup.missions.Add(new AIMission("2x Rebel F/A-26B Strikers",
           AIMissionType.Strike,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "fa26_aim9x3", "fa26_maverickx1", "fa26_maverickx1", "h70-x14ld-under", "fa26_mk82x3", "fa26_mk82x3", "h70-x14ld-under", "fa26_maverickx1", "fa26_maverickx1", "fa26_aim9x3", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));

        //CAP missions
        missionGroup.missions.Add(new AIMission("2x ASF-30 CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" }),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" })
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x ASF-30 CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" }),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" }),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" }),
                new AircraftLoadout("ASF-30", new string[] { "asf30_gun", "asf-srmx1", "asf-srmx1", "asf_mrmRail", "asf_mrmRail", "asf_mrmDrop", "asf_mrmDrop", "asf_mrmRail", "asf_mrmRail", "asf-srmx1", "asf-srmx1" })
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-33",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", "" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", "" })
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-33 LR",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "asf_droptank", "asf_droptank" })
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("4x ASF-33",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", "" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", "" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", "" }),
                new AircraftLoadout("ASF-33", new string[] { "af_gun", "asf_mrmx6", "asf_srmx2Left", "asf_srmx2Left", "", "" })
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-58",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", "" }),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "", "" })
           },
           3500,
           250));

        missionGroup.missions.Add(new AIMission("2x ASF-58 LR",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "asf58_droptank", "asf58_droptank" }),
                new AircraftLoadout("ASF-58", new string[] { "asf58_gun", "asf58_mrmx8", "asf58_mrmx8", "asf58_srmx2Left", "asf58_srmx2Right", "asf58_droptank", "asf58_droptank" })
           },
           3500,
           250));

        //rebel CAP
        /*missionGroup.missions.Add(new AIMission("2x Rebel AV-42C CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", "" }),
                new AircraftLoadout("AV-42CAI", new string[] { "gau-8", "sidewinderx3", "sidewinderx3", "sidewinderx3", "sidewinderx3", "", "" })
           },
           3500,
           250));
        */

        missionGroup.missions.Add(new AIMission("2x Rebel F/A-26B CAP",
           AIMissionType.CAP,
           new List<AircraftLoadout>() {
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""}),
                new AircraftLoadout("FA-26B AI", new string[] { "af_gun", "af_aim9", "af_aim9", "af_amraamRail", "af_amraam", "af_amraam", "af_amraam", "af_amraam", "af_amraamRail", "af_aim9", "af_aim9", "fa26_droptank", "fa26_droptank", "fa26_droptank", "af_tgp", ""})
           },
           3500,
           250));

        return missionGroup;
    }
}

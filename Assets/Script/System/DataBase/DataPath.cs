using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPath 
{
    //INFOR
    public static readonly string INFO = "info";
    public static readonly string NICKNAME = INFO +  "/nickname";
    public static readonly string ENQUIPS = INFO + "/enquip";

    //STAT
    public static readonly string STAT = "stat";
    public static readonly string DAMAGELEVEL = STAT + "/damageLevel";
    public static readonly string FIRERATELEVEL = STAT + "/fireRateLevel";
    public static readonly string GOLDVALUELEVEL = STAT + "/goldValueLevel";
    public static readonly string GOLDDAILYLEVEL = STAT + "/dailyGoldLevel";

    //INVENTORY
    public static readonly string INVENTORY = "inventory";
    public static readonly string GOLD = INVENTORY + "/gold";
    public static readonly string ENERGY = INVENTORY + "/energy";
    public static readonly string DIAMOND = INVENTORY + "/diamond";
    public static readonly string GUNS = INVENTORY + "/guns";



    public static readonly string GOLDTSTARTTIME = "goldStartTime";
    public static readonly string ENERGYSTARTTIME = "energyStartTime";
    public static readonly string GOLDDAILYSAVED = "goldDailySaved";
    public static readonly string CURRENT_MISSION = "currentMisison";

}

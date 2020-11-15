using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public PlayerInfo info = new PlayerInfo();
    public PlayerStat stat = new PlayerStat();
    public PlayerInventory inventory = new PlayerInventory();
    public QuestData quest = new QuestData();
    public int currentMisison;
    public long goldStartTime;
    public long energyStartTime;
    public int goldDailySaved;


}

[Serializable]
public class PlayerInfo
{
    public string nickname;
    public int enquip;

}
[Serializable]
public class PlayerStat
{
    public int damageLevel;
    public int fireRateLevel;
    public int goldValueLevel;
    public int dailyGoldLevel;
}
[Serializable]
public class PlayerInventory
{
    public int gold;
    public int energy;
    public int diamond;
    [SerializeField]
    public Dictionary<string, GunInfor> guns = new Dictionary<string, GunInfor>();
}

[Serializable]
public class GunInfor
{
    public int id;
    public int damageLevel;
    public int rofLevel;
}

[Serializable]
public class QuestData
{
    public int time;
    public Dictionary<string, QuestDetail> detail = new Dictionary<string, QuestDetail>();
}
[Serializable]
public class QuestDetail
{
    public int id;
    public int require;
    public int currentProgress;
    public int reward;
    public QuestExtraDetail other;
    public bool isRewarded;
}
[Serializable]
public class QuestExtraDetail { }

[Serializable]
public class KillSpecificEnemyDetail : QuestExtraDetail
{
    public int enemyId;
}




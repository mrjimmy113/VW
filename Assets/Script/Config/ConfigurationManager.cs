using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : Singleton<ConfigurationManager>
{

    //MISSION
    public ConfigEnemyType enemyType;

    public ConfigEnemy enemy;
    
    public ConfigMission mission;
    
    public ConfigWave wave;

    //GUN
    public ConfigGun gun;

    public ConfigGunDamage gunDmg;

    public ConfigGunRof gunRof;

    //PLAYER
    public ConfigPlayerDamage playerDamage;

    public ConfigPlayerFireRate playerFireRate;

    public ConfigPlayerCoinValue playerCoinValue;

    public ConfigPlayerDailyCoin playerDailyCoin;

    //TIP
    public ConfigTip tip;

    public ConfigBuffDebuff buffDebuff;

    public ConfigProjectileBuff pBuff;

    public ConfigDailyQuest dailyQuest;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }


    public void InitConfig(Action callback)
    {
        StartCoroutine(LoadConfig(callback));
    }

    IEnumerator LoadConfig(Action callback)
    {
        enemyType = Resources.Load("DataTable/ConfigEnemyType", typeof(ScriptableObject)) as ConfigEnemyType;
        yield return new WaitUntil(() => enemyType != null);

        enemy = Resources.Load("DataTable/ConfigEnemy", typeof(ScriptableObject)) as ConfigEnemy;
        yield return new WaitUntil(() => enemy != null);

        mission = Resources.Load("DataTable/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => mission != null);

        wave = Resources.Load("DataTable/ConfigWave", typeof(ScriptableObject)) as ConfigWave;
        yield return new WaitUntil(() => wave != null);

        gun = Resources.Load("DataTable/ConfigGun", typeof(ScriptableObject)) as ConfigGun;
        yield return new WaitUntil(() => gun != null);

        gunDmg = Resources.Load("DataTable/ConfigGunDamage", typeof(ScriptableObject)) as ConfigGunDamage;
        yield return new WaitUntil(() => gunDmg != null);

        gunRof = Resources.Load("DataTable/ConfigGunRof", typeof(ScriptableObject)) as ConfigGunRof;
        yield return new WaitUntil(() => gunRof != null);

        playerDamage = Resources.Load("DataTable/ConfigPlayerDamage", typeof(ScriptableObject)) as ConfigPlayerDamage;
        yield return new WaitUntil(() => playerDamage != null);

        playerFireRate = Resources.Load("DataTable/ConfigPlayerFireRate", typeof(ScriptableObject)) as ConfigPlayerFireRate;
        yield return new WaitUntil(() => playerFireRate != null);

        tip = Resources.Load("DataTable/ConfigTip", typeof(ScriptableObject)) as ConfigTip;
        yield return new WaitUntil(() => tip != null);

        playerCoinValue = Resources.Load("DataTable/ConfigPlayerCoinValue", typeof(ScriptableObject)) as ConfigPlayerCoinValue;
        yield return new WaitUntil(() => playerCoinValue != null);

        playerDailyCoin = Resources.Load("DataTable/ConfigPlayerDailyCoin", typeof(ScriptableObject)) as ConfigPlayerDailyCoin;
        yield return new WaitUntil(() => playerDailyCoin != null);

        buffDebuff = Resources.Load("DataTable/ConfigBuffDebuff", typeof(ScriptableObject)) as ConfigBuffDebuff;
        yield return new WaitUntil(() => buffDebuff != null);

        pBuff = Resources.Load("DataTable/ConfigProjectileBuff", typeof(ScriptableObject)) as ConfigProjectileBuff;
        yield return new WaitUntil(() => pBuff != null);


        dailyQuest = Resources.Load("DataTable/ConfigDailyQuest", typeof(ScriptableObject)) as ConfigDailyQuest;
        yield return new WaitUntil(() => dailyQuest != null);



        callback?.Invoke();
    }
}

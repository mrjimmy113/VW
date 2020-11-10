
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class DataAPIController : Singleton<DataAPIController>
{
    [SerializeField]
    private DataModel dataModel = null;
    private Dictionary<string, DataEvent> dicEvent = new Dictionary<string, DataEvent>();

    #region Base Func

    public void RegisterEvent(string path,UnityAction<object> callBack)
    {
        if(dicEvent.ContainsKey(path))
        {
            dicEvent[path].AddListener(callBack);
        }
        else
        {
            DataEvent event_ = new DataEvent();
            event_.AddListener(callBack);           
            dicEvent.Add(path, event_);
        }
    }
    public void UnregisterEvent(string path, UnityAction<object> callBack)
    {
        if (dicEvent.ContainsKey(path))
        {
            dicEvent[path].RemoveListener(callBack);
        }
    }
    private void OnDataChange(string path, object data)
    {
        if (dicEvent.ContainsKey(path))
        {
            dicEvent[path].Invoke(data);
        }
    }
    // Start is called before the first frame update
    public void OnInitData(Action callback)
    {
        

        if (dataModel.LoadData())
        {
            callback?.Invoke();
        }
        else
        {
            
            dataModel.CreateNewData();
            callback?.Invoke();

        }
        dataModel.onDataChange.AddListener(OnDataChange);
    }

    #endregion

    #region CurrentMission API

    public int GetCurrentMission()
    {
        return (int)dataModel.Read(DataPath.CURRENT_MISSION);
    }

    public int UpdateCurrentMission()
    {
        int currentMission = GetCurrentMission();
        
        ConfigMissionRecord cf = ConfigurationManager.instance.mission.GetRecordByKeySearch(currentMission);
        AddEnergy(cf.ClearEnergyReward);
        

        dataModel.UpdateData(DataPath.CURRENT_MISSION, currentMission  + 1);

        return cf.ClearGunUnLockId;
    }

    #endregion

    #region Level + EXP API

    /*public int GetCurrentUserLevel()
    {
        return (int)dataModel.Read(DataPath.LEVEL);
    }

    public int GetCurrentExp()
    {
        return (int)dataModel.Read(DataPath.EXP);
    }

    public bool AddExp(int amount, out int gunUnlocked)
    {
        bool isLevelUp = false;

        int currentLevel = GetCurrentUserLevel();
        gunUnlocked = 0;
        ConfigUserLevelRecord nextLevelConfig =
            ConfigurationManager.instance.userLevel.GetRecordByKeySearch(currentLevel + 1);
        int currentExp = GetCurrentExp();
        currentExp += amount;
        if(currentExp >= nextLevelConfig.RequiredExp)
        {
            currentExp -= nextLevelConfig.RequiredExp;
            currentLevel++;
            dataModel.UpdateData(DataPath.LEVEL, currentLevel);
            isLevelUp = true;
            gunUnlocked = nextLevelConfig.GunUnlockId;
        }
        dataModel.UpdateData(DataPath.EXP, currentExp);

        return isLevelUp;
    }*/

    #endregion

    #region Gold + Energy + Diamond API
    //GOLD
    public int GetCurrentGold()
    {
        return (int)dataModel.Read(DataPath.GOLD);
    }

    public void AddGold(int amount)
    {
        int gold = GetCurrentGold();
        gold += amount;
        dataModel.UpdateData(DataPath.GOLD, gold);
    }
    //ENERGY
    public int GetCurrentEnergy()
    {
        return (int)dataModel.Read(DataPath.ENERGY);
    }

    public void AddEnergy(int amount)
    {
        int energy = GetCurrentEnergy();
        if(energy < ResoucesConfig.ENERGY_CAPACITY)
        {
            energy += amount;
            dataModel.UpdateData(DataPath.ENERGY, energy);
        }
     
    }

    public void SubstractEnergy(int amount)
    {
        int energy = GetCurrentEnergy();
        energy -= amount;
        dataModel.UpdateData(DataPath.ENERGY, energy);

    }
    //DIAMOND
    public int GetCurrentDiamond()
    {
        return (int)dataModel.Read(DataPath.DIAMOND);
    }

    public void AddDiamond(int amount)
    {
        int diamond = GetCurrentDiamond();
        diamond += amount;
        dataModel.UpdateData(DataPath.DIAMOND, diamond);
    }

    #endregion

    #region Inventory Gun API

    public List<int> GetAllActiveGun()
    {
        Dictionary<string, GunInfor> guns =
            (Dictionary<string, GunInfor>)dataModel.Read(DataPath.GUNS);

        List<int> result = new List<int>();
        foreach(var g in guns)
        {
            result.Add(g.Key.ReverseKey());
        }

        return result;
    }

    public int GetCurrentEnquipId()
    {
        return (int)dataModel.Read(DataPath.ENQUIPS);
    }

    public void ChangeEnquipGun(int id)
    {
        dataModel.UpdateData(DataPath.ENQUIPS, id);
    }

    public void UnlockGun(int id)
    {
        GunInfor gun = new GunInfor();
        gun.id = id;
        gun.damageLevel = 1;
        gun.rofLevel = 1;
        dataModel.UpdateDataDic(DataPath.GUNS, id, gun);

    }

    public GunInfor GetGunInfor(int id)
    {
        Dictionary<string, GunInfor> guns =
            (Dictionary<string, GunInfor>)dataModel.Read(DataPath.GUNS);
        if (guns == null) return null;

        if (guns.ContainsKey(id.ToKey())) return guns[id.ToKey()];
        else return null;
    }

    public bool LevelUpGunDamage(int id)
    {
        GunInfor gun = GetGunInfor(id);

        ConfigGunDamageRecord nextLevelConfig =
            ConfigurationManager.instance.gunDmg.
            GetRecordByKeySearch(new Compare2KeySearch<int, int> 
            {key_1 = id,key_2 = gun.damageLevel + 1 });
        if (nextLevelConfig == null) return false;
        int gold = GetCurrentGold();
        

        if(nextLevelConfig.UnlockFee <= gold)
        {
            gold -= nextLevelConfig.UnlockFee;
            gun.damageLevel++;
            dataModel.UpdateData(DataPath.GOLD, gold);
            dataModel.UpdateDataDic(DataPath.GUNS, id, gun);
            return true;
        }

        return false;
    }

    public bool LevelUpGunRof(int id)
    {
        GunInfor gun = GetGunInfor(id);

        ConfigGunRofRecord nextLevelConfig =
            ConfigurationManager.instance.gunRof.
            GetRecordByKeySearch(new Compare2KeySearch<int, int>
            { key_1 = id, key_2 = gun.damageLevel + 1 });
        if (nextLevelConfig == null) return false;
        int gold = GetCurrentGold();

        if (nextLevelConfig.UnlockFee <= gold)
        {
            gold -= nextLevelConfig.UnlockFee;
            gun.rofLevel++;
            dataModel.UpdateData(DataPath.GOLD, gold);
            dataModel.UpdateDataDic(DataPath.GUNS, id, gun);
            return true;
        }

        return false;
    }


    #endregion

    #region Player Stat API

    public int GetCurrentDamageLevel()
    {
        return (int)dataModel.Read(DataPath.DAMAGELEVEL);
    }

    public int GetCurrentFireRateLevel()
    {
        return (int)dataModel.Read(DataPath.FIRERATELEVEL);
    }

    public int GetCurrentGoldValueLevel()
    {
        return (int)dataModel.Read(DataPath.GOLDVALUELEVEL);
    }

    public int GetCurrentGoldDailyLevel()
    {
        return (int)dataModel.Read(DataPath.GOLDDAILYLEVEL);
    }

    public bool LevelUpDamage()
    {
        int currentLevel = GetCurrentDamageLevel();
        ConfigPlayerDamageRecord nextLevelConfig =
            ConfigurationManager.instance.playerDamage.GetRecordByKeySearch(currentLevel + 1);
        if (nextLevelConfig == null) return false;

        int gold = GetCurrentGold();
        if(gold >= nextLevelConfig.UnlockFee)
        {
            gold -= nextLevelConfig.UnlockFee;
            currentLevel++;
            dataModel.UpdateData(DataPath.GOLD, gold);
            dataModel.UpdateData(DataPath.DAMAGELEVEL, currentLevel);
            return true;
        }
        return false;
    }

    public bool LevelUpFireRate()
    {
        int currentLevel = GetCurrentFireRateLevel();
        ConfigPlayerFireRateRecord nextLevelConfig =
            ConfigurationManager.instance.playerFireRate.GetRecordByKeySearch(currentLevel + 1);
        if (nextLevelConfig == null) return false;

        int gold = GetCurrentGold();
        if (gold >= nextLevelConfig.UnlockFee)
        {
            gold -= nextLevelConfig.UnlockFee;
            currentLevel++;
            dataModel.UpdateData(DataPath.GOLD, gold);
            dataModel.UpdateData(DataPath.FIRERATELEVEL, currentLevel);
            return true;
        }
        return false;
    }

    public bool LevelUpCoinValue()
    {
        int currentLevel = GetCurrentGoldValueLevel();
        ConfigPlayerCoinValueRecord nextLevelConfig =
            ConfigurationManager.instance.playerCoinValue.GetRecordByKeySearch(currentLevel + 1);

        if (nextLevelConfig == null) return false;
        int gold = GetCurrentGold();
        
        if (gold >= nextLevelConfig.UnlockFee)
        {
            gold -= nextLevelConfig.UnlockFee;
            currentLevel++;
            dataModel.UpdateData(DataPath.GOLD, gold);
            dataModel.UpdateData(DataPath.GOLDVALUELEVEL, currentLevel);
            return true;
        }
        return false;
    }

    public bool LevelUpDailyCoin()
    {
        int currentLevel = GetCurrentGoldDailyLevel();
        ConfigPlayerDailyCoinRecord nextLevelConfig =
            ConfigurationManager.instance.playerDailyCoin.GetRecordByKeySearch(currentLevel + 1);
        if (nextLevelConfig == null) return false;

        int gold = GetCurrentGold();
        if (gold >= nextLevelConfig.UnlockFee)
        {
            gold -= nextLevelConfig.UnlockFee;
            currentLevel++;
            dataModel.UpdateData(DataPath.GOLD, gold);
            dataModel.UpdateData(DataPath.GOLDDAILYLEVEL, currentLevel);
            return true;
        }
        return false;
    }

    #endregion

    #region Gold Energy Start time

    public long GetGoldStartTime()
    {
        return (long)dataModel.Read(DataPath.GOLDTSTARTTIME);
    }

    public long GetEnergyStartTime()
    {
        return (long)dataModel.Read(DataPath.ENERGYSTARTTIME);
    }

    public int GetGoldSaved()
    {
        return (int)dataModel.Read(DataPath.GOLDDAILYSAVED);
    }

    public void AddGoldSaved(int amount)
    {
        int currentGoldSaved = GetGoldSaved();
        currentGoldSaved += amount;
        dataModel.UpdateData(DataPath.GOLDDAILYSAVED, currentGoldSaved);
    }

    public void ResetGoldSaved()
    {
        dataModel.UpdateData(DataPath.GOLDDAILYSAVED, 0);
    }

    public void SetGoldStartTime(long time)
    {
        dataModel.UpdateData(DataPath.GOLDTSTARTTIME, time);
    }

    public void SetEnergyStartTime(long time)
    {
        dataModel.UpdateData(DataPath.ENERGYSTARTTIME, time);
    }

    


    #endregion

}

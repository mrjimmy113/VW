using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoucesConfig
{
    public static readonly int GOLD_GAIN_TIME = 10;
    public static readonly int ENERGY_GAIN_TIME = 30;
    public static readonly int ENERGY_VALUE = 1;
    public static readonly int ENERGY_CAPACITY = 80;
}


public class ResourcesGainHandler : Singleton<ResourcesGainHandler>
{
    public event Action<long> GoldEarnedHandler;
    public event Action<long> EnergyEarnedHandler;

    private long goldStartTime = 0;
    private long goldTimeTillEarned = 0;
    private long energyStartTime = 0;
    private long energyTimeTillEarned = 0;

    private int goldDailyValue = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void InitHandler()
    {
        goldStartTime = DataAPIController.instance.GetGoldStartTime();
        energyStartTime = DataAPIController.instance.GetEnergyStartTime();

        long currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        ConfigPlayerDailyCoinRecord record = ConfigurationManager.instance.playerDailyCoin.
            GetRecordByKeySearch(DataAPIController.instance.GetCurrentGoldDailyLevel());
        goldDailyValue = record.Value;

        long goldRemainTime = currentTime - goldStartTime;
        long energyRemainTime = currentTime - energyStartTime;

        int goldEarned = (int)goldRemainTime / ResoucesConfig.GOLD_GAIN_TIME;
        int energyEarned = (int)energyRemainTime / ResoucesConfig.ENERGY_GAIN_TIME;


        if (goldEarned > 0) DataAPIController.instance.AddGoldSaved(goldEarned * goldDailyValue);

        goldTimeTillEarned = (goldStartTime % ResoucesConfig.GOLD_GAIN_TIME);
        goldStartTime = currentTime + goldTimeTillEarned;
        DataAPIController.instance.SetGoldStartTime(goldStartTime);
        

        if (energyEarned > 0) DataAPIController.instance.AddEnergy(energyEarned * ResoucesConfig.ENERGY_VALUE);

        energyTimeTillEarned = (energyStartTime % ResoucesConfig.ENERGY_GAIN_TIME);
        energyStartTime = currentTime + energyTimeTillEarned;
        DataAPIController.instance.SetEnergyStartTime(energyStartTime);

        DataAPIController.instance.RegisterEvent(DataPath.GOLDDAILYLEVEL, OnGoldDailyLevelChange);


        StartCoroutine(UpdateEarnedHandler());
    }

    private void OnGoldDailyLevelChange(object arg0)
    {
        int level = (int)arg0;
        ConfigPlayerDailyCoinRecord record = ConfigurationManager.instance.playerDailyCoin.GetRecordByKeySearch(level);
        goldDailyValue = record.Value;
    }

    IEnumerator UpdateEarnedHandler()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        
        while(true)
        {
            yield return waitForSeconds;

            goldTimeTillEarned += 1;
            energyTimeTillEarned += 1;

            long time = DateTimeOffset.Now.ToUnixTimeSeconds();

            if(goldTimeTillEarned >= ResoucesConfig.GOLD_GAIN_TIME)
            {
                goldTimeTillEarned -= ResoucesConfig.GOLD_GAIN_TIME;
                goldStartTime = time;
                DataAPIController.instance.SetGoldStartTime(goldStartTime);
                DataAPIController.instance.AddGoldSaved(goldDailyValue);
            }

            if(energyTimeTillEarned >= ResoucesConfig.ENERGY_GAIN_TIME)
            {
                energyTimeTillEarned -= ResoucesConfig.ENERGY_GAIN_TIME;
                energyStartTime = time;
                DataAPIController.instance.SetEnergyStartTime(energyStartTime);
                DataAPIController.instance.AddEnergy(ResoucesConfig.ENERGY_VALUE);
            }
            
            GoldEarnedHandler?.Invoke(goldTimeTillEarned);
            EnergyEarnedHandler?.Invoke(energyTimeTillEarned);
        }
    }
}

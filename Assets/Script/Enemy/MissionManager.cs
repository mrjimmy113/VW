using DoozyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfor
{
    public float timeDelayOnEachSpawn;
    public float timeDelayToSpawn;
    public EnemyInfor[] enemyInfor;
    public List<int> numbers;

    

    public WaveInfor (ConfigWaveRecord config)
    {
        timeDelayOnEachSpawn = config.TimeDelayOnEachSpawn;
        timeDelayToSpawn = config.TimeDelayToSpawn;
        numbers = config.Numbers;
        enemyInfor = new EnemyInfor[config.EnemyIds.Count];
        for (int i = 0; i < config.EnemyIds.Count; i++)
        {
            EnemyInfor infor 
                = new EnemyInfor(ConfigurationManager.instance.enemy.GetRecordByKeySearch(config.EnemyIds[i]));
            enemyInfor[i] = infor;
        }
        

    }
}

public class EnemyInfor
{
    public int hp;
    public float sizeScale;
    public GameObject prefab;
    public bool isDuplicate;
    public EnemyInfor[] duplicate;
    public int coinAmount;
    public bool spawnBuffDebuff;

    public EnemyInfor() { }

    public EnemyInfor(ConfigEnemyRecord config)
    {
        hp = config.Hp;
        sizeScale = config.SizeScale;
        prefab = Resources.Load(config.Prefab) as GameObject;
        isDuplicate = config.IsDuplicate;
        coinAmount = config.CoinAmount;
        spawnBuffDebuff = config.SpawnBuffDebuff;
        if(config.IsDuplicate)
        {
            duplicate = new EnemyInfor[config.EnemyDuplicateIds.Count];
            for (int i = 0; i < duplicate.Length; i++)
            {

                
                duplicate[i] = new EnemyInfor(ConfigurationManager.instance.enemy.GetRecordByKeySearch(config.EnemyDuplicateIds[i]));
            }
        }
        
    }
}


public class MissionManager : Singleton<MissionManager>
{
    private int waveIndex = 0;
    private int totalEnemy;
    private int totalEnemyDead;
    public int totalMissionEnemy;
    public int totalMissionEnemyDead;

    public event Action<int,int> OnEnemyDeadEvent;
    public event Action<int> OnGoldEarnedIncrease;
    public event Action OnMissionClearEvent;
    public event Action OnMissionStart;
    public event Action<bool> OnGameEnd;

    private WaveInfor[] waves;
    private bool isLastMission = false;

    public int goldEarned = 0;
    private PlayerControl playerControl;
    private int goldValue = 0;

    public int currentMission = 0;

    public bool isStartMission = false;

    private void Awake()
    {
        currentMission = DataAPIController.instance.GetCurrentMission();
    }

    void Start()
    {
        ConfigMissionRecord record = ConfigurationManager.instance.mission.GetRecordByKeySearch(currentMission);
        if(record == null)
        {
            isLastMission = true;
            record = ConfigurationManager.instance.mission.GetRecordByKeySearch(DataAPIController.instance.GetCurrentMission() - 1);
        }
        waves = new WaveInfor[record.Waves.Count];
        for (int i = 0; i < record.Waves.Count; i++)
        {
            
            WaveInfor infor = new WaveInfor(
                ConfigurationManager.instance.wave.GetRecordByKeySearch(record.Waves[i]));
            
            for(int j = 0; j < infor.enemyInfor.Length; j++)
            {
                if(infor.enemyInfor[j].isDuplicate)
                {
                    totalMissionEnemy += infor.numbers[j] + infor.enemyInfor[j].duplicate.Length;
                }else
                {
                    totalMissionEnemy += infor.numbers[j];
                }
            }
            waves[i] = infor;
        }
        waveIndex = -1;
        
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        playerControl.OnPlayerDead += LoseGame;

    }

    public void StartMission()
    {
        int currentEnergy = DataAPIController.instance.GetCurrentEnergy();
        
        if(currentEnergy >= 5)
        {
            OnMissionStart?.Invoke();
            StartCoroutine(StartWave());
            DataAPIController.instance.SubstractEnergy(5);
            ConfigPlayerCoinValueRecord coinValueCf = ConfigurationManager.instance.playerCoinValue.GetRecordByKeySearch(DataAPIController.instance.GetCurrentGoldValueLevel());
            goldValue = coinValueCf.Value;
            isStartMission = true;

            MyDoozyUIHelper.HideAllElementInCategory(DooName.MASTER_VIEW);
            MyDoozyUIHelper.ShowView(DooName.INGAME_VIEW);
            
        }
        else
        {
            //
        }
    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(1f);
        
        foreach(var w in waves)
        {
            waveIndex++;
            totalEnemy = 0;
            totalEnemyDead = 0;
            yield return new WaitForSeconds(w.timeDelayToSpawn);
            for (int i = 0; i < w.numbers.Count; i++)
            {
                
                for (int j = 0; j < w.numbers[i];j++)
                {

                    EnemyInfor infor = w.enemyInfor[i];

                    EnemyControl enemy = EnemyFactory.instance.CreateEnemy(infor.prefab);
                    enemy.Setup(infor,false,true);
                    enemy.OnEnemyDead += OnEnemyDead;
                    enemy.transform.position = enemy.GetTopPos();
                    
                    totalEnemy++;
                    if(infor.isDuplicate)
                    {
                        totalEnemy += infor.duplicate.Length;
                        
                    }
                    yield return new WaitForSeconds(w.timeDelayOnEachSpawn);
                }
           
            }
            WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
            while(true)
            {
                yield return waitForSeconds;
                if (totalEnemyDead == totalEnemy) break;
            }

        }

        StartCoroutine(CheckEndGame());

    }

    private void OnEnemyDead(OnEnemyDeadParam param)
    {
        if(param.isCountDead)
        {
            goldEarned += goldValue * param.coinAmount;
            OnGoldEarnedIncrease?.Invoke(goldEarned);
            totalMissionEnemyDead++;
            totalEnemyDead++;
            OnEnemyDeadEvent?.Invoke(totalMissionEnemyDead, totalMissionEnemy);
        }
    }

    IEnumerator CheckEndGame()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (EnemyFactory.instance.activeEnemy.Count == 0)
            {
                Time.timeScale = 1f;
                OnMissionClearEvent?.Invoke();
                if (!isLastMission)
                {
                    int unlockGunId =  DataAPIController.instance.UpdateCurrentMission();
                    OnGameEnd?.Invoke(true);
                    DataAPIController.instance.AddGold(goldEarned);
                    if(unlockGunId != 0)
                    {
                        //Bla bla bla
                    }
                }

                break;
            }
        }
    }

    public void AddGoldEanred(int amount)
    {
        goldEarned += amount * goldValue;
        OnGoldEarnedIncrease?.Invoke(goldEarned);
    }

    private void LoseGame()
    {
        Time.timeScale = 1f;
        
        OnGameEnd?.Invoke(false);
    }


    

}

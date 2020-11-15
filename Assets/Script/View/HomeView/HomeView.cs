using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public Text txtGold;
    public Text txtEnergy;
    public Text txtDiamond;
    public Image imgEnergy;
    public Text txtEnergyProgress;
    public LevelPanel levelPanel;
    

    private DownPanelExtendControl downPanelExtendControl;
    public event Action OpenPanelEvent;
    public event Action ClosePanelEvent;

    private PlayerControl playerControl;

    private bool isSetupOneTime = true;

    public Button btnUpgradePlayer;
    public Button btnUpgradeGun;
    public Button btnUpgradeCoin;

    private bool isDownPanelOpen = false;

    private void OnEnable()
    {
        downPanelExtendControl = GetComponentInChildren<DownPanelExtendControl>();
        
    }

    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        txtGold.text = DataAPIController.instance.GetCurrentGold().NumberNormalize();

        int energy = DataAPIController.instance.GetCurrentEnergy();
        txtEnergy.text = energy.NumberNormalize();
        imgEnergy.fillAmount = (float)energy / ResoucesConfig.ENERGY_CAPACITY;
        if(energy >= 80)
        {
            txtEnergyProgress.gameObject.SetActive(false);
        }else
        {
            txtEnergyProgress.gameObject.SetActive(true);
        }

        txtDiamond.text = DataAPIController.instance.GetCurrentDiamond().ToString();
        int currentLevel = MissionManager.instance.currentMission;
 
       




        downPanelExtendControl.Setup();
        levelPanel.Setup(currentLevel);
        
        

        if(isSetupOneTime)
        {
            isSetupOneTime = false;
            GetComponentInChildren<GoldEarnedPanel>().Setup();
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            DataAPIController.instance.RegisterEvent(DataPath.GOLD, OnGoldChange);
            DataAPIController.instance.RegisterEvent(DataPath.ENERGY, OnEnergyChange);
            ResourcesGainHandler.instance.EnergyEarnedHandler += EnergyProgress;

            btnUpgradePlayer.onClick.AddListener(() =>
            {
                OpenPanel(0, DooName.PLAYER_UPGRADE);
            });


            btnUpgradeGun.onClick.AddListener(() =>
            {
                OpenPanel(1, DooName.GUN_UPGRADE);
            });


            btnUpgradeCoin.onClick.AddListener(() =>
            {
                OpenPanel(2, DooName.COIN_UPGRADE);
            });
        }
        
    }

    private void OnEnergyChange(object arg0)
    {
        int energy = (int)arg0;
        txtEnergy.text = energy.NumberNormalize();
        imgEnergy.fillAmount = (float)energy / ResoucesConfig.ENERGY_CAPACITY;
        if (energy >= 80)
        {
            txtEnergyProgress.gameObject.SetActive(false);
        }
        else
        {
            txtEnergyProgress.gameObject.SetActive(true);
        }
    }

    private void OnGoldChange(object arg0)
    {
        int gold = (int)arg0;
        txtGold.text = gold.NumberNormalize();
    }

    private void EnergyProgress(long remainTime)
    {
        string time = (ResoucesConfig.ENERGY_GAIN_TIME - (int)remainTime).ToMinuteAndSecond() + " + 1";
        txtEnergyProgress.text = time;
    }

    public override void OnHideView()
    {
        
        base.OnShowView();
        
    }

    public void OpenPanel(int index, string viewName)
    {
        downPanelExtendControl.Open(index, viewName);
        playerControl.OnPanelOpen();
        isDownPanelOpen = true;
        
    }

    public void ClosePanel()
    {
        downPanelExtendControl.HideAll();
        playerControl.OnPanelClose();
        isDownPanelOpen = false;
    }

    public void StartGame()
    {
        if (isDownPanelOpen)
        {
            ClosePanel();
        }
        else MissionManager.instance.StartMission();
        
    }  
}

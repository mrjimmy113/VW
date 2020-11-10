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
    public Text txtPreviousLevel;
    public Text txtCurrentLevel;
    public Text txtNextLevel;
    public Image imgEnergy;
    public Text txtEnergyProgress;

    private DownPanelExtendControl downPanelExtendControl;
    public event Action OpenPanelEvent;
    public event Action ClosePanelEvent;

    private PlayerControl playerControl;

    private bool isSetupOneTime = true;

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
        txtPreviousLevel.text = currentLevel - 1 + "";
        txtCurrentLevel.text = currentLevel + "";
        txtNextLevel.text = currentLevel + 1 + "";
        downPanelExtendControl.Setup();
        InputManager.instance.OnControlDownWithOutParam += ClosePanel;
        
        

        if(isSetupOneTime)
        {
            isSetupOneTime = false;
            GetComponentInChildren<GoldEarnedPanel>().Setup();
            playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            DataAPIController.instance.RegisterEvent(DataPath.GOLD, OnGoldChange);
            DataAPIController.instance.RegisterEvent(DataPath.ENERGY, OnEnergyChange);
            ResourcesGainHandler.instance.EnergyEarnedHandler += EnergyProgress;
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
        InputManager.instance.OnControlDownWithOutParam -= ClosePanel;
        base.OnShowView();
        
    }

    public void OpenPanel(int index)
    {
        downPanelExtendControl.Open(index);
        playerControl.OnPanelOpen();
        MissionManager.instance.isDownPanelOpen = true;
    }

    public void ClosePanel()
    {
        if(!MissionManager.instance.isStartMission)
        {
            downPanelExtendControl.HideAll();
            playerControl.OnPanelClose();
            
        }else
        {
            InputManager.instance.OnControlDownWithOutParam -= ClosePanel;
        }
        MissionManager.instance.isDownPanelOpen = false;
    }

   
}

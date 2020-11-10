using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPanelExtendItem_Player : DownPanelExtendItemControl
{

    private ConfigPlayerDamageRecord dmgCf;
    private ConfigPlayerDamageRecord dmgCfNext;

    private ConfigPlayerFireRateRecord frCf;
    private ConfigPlayerFireRateRecord frCfNext;

    private int currentGold;
    private bool isSetupOneTime = false;

    public override void Setup()
    {
        
        currentGold = DataAPIController.instance.GetCurrentGold();


        
        
        if(root == null) root = GetComponent<DownPanelExtendItem>();

        if (!isSetupOneTime)
        {
            isSetupOneTime = true;

            int damageLevel = DataAPIController.instance.GetCurrentDamageLevel();
            int fireRateLevel = DataAPIController.instance.GetCurrentFireRateLevel();
            dmgCf = ConfigurationManager.instance.playerDamage.GetRecordByKeySearch(damageLevel);
            dmgCfNext = ConfigurationManager.instance.playerDamage.GetRecordByKeySearch(damageLevel + 1);
            frCf = ConfigurationManager.instance.playerFireRate.GetRecordByKeySearch(fireRateLevel);
            frCfNext = ConfigurationManager.instance.playerFireRate.GetRecordByKeySearch(fireRateLevel + 1);

            root.btnDownBuy.onClick.AddListener(() =>
            {
                LevelUpFirePower();
            });


            root.btnMidBuy.onClick.AddListener(() =>
            {
                LevelUpFireRate();
            });

            DataAPIController.instance.RegisterEvent(DataPath.GOLD, OnGoldChangeEvent);
        }

        UpdateDownUI();
        UpdateMidUI();

        
        

    }

    private void UpdateDownUI()
    {
        if(dmgCfNext != null)
        {
            
            root.txtDownFieldName.text = "Firepower [Lv" + dmgCf.Level + "]";
            root.txtDownCoin.text = dmgCfNext.UnlockFee.NumberNormalize();
            root.btnDownBuy.interactable = currentGold >= dmgCfNext.UnlockFee;
        }
        else
        {
            root.txtDownFieldName.text = "Firepower [Lv MAX]";
            root.txtDownCoin.text = "MAX";
            root.btnDownBuy.interactable = false;
        }

        root.txtDownValue.text = dmgCf.Value.ToString();
        
        
    }

    private void UpdateMidUI()
    {
        if(frCfNext != null)
        {
            root.txtMidFieldName.text = "Firerate [Lv" + frCf.Level + "]";
            root.txtMidCoin.text = frCfNext.UnlockFee.NumberNormalize();
            root.btnMidBuy.interactable = currentGold >= frCfNext.UnlockFee;
        }
        else
        {
            root.txtMidFieldName.text = "Firerate [Lv MAX]";
            root.txtMidCoin.text = "MAX";
            root.btnMidBuy.interactable = false;
        }

        root.txtMidValue.text = frCf.Value.ToString();
        
        
    }

    private void OnGoldChangeEvent(object obj)
    {
        currentGold = (int)obj;
        if (dmgCfNext != null) root.btnDownBuy.interactable = currentGold >= dmgCfNext.UnlockFee;
        else root.btnDownBuy.interactable = false;

        if (frCfNext != null) root.btnMidBuy.interactable = currentGold >= frCfNext.UnlockFee && frCfNext != null;
        else root.btnMidBuy.interactable = false;


    }

    private void LevelUpFirePower()
    {
        if(DataAPIController.instance.LevelUpDamage())
        {
            dmgCf = dmgCfNext;
            dmgCfNext = ConfigurationManager.instance.playerDamage.GetRecordByKeySearch(dmgCf.Level + 1);
            UpdateDownUI();
        }
    }

    private void LevelUpFireRate()
    {
        if (DataAPIController.instance.LevelUpFireRate())
        {
            frCf = frCfNext;
            frCfNext = ConfigurationManager.instance.playerFireRate.GetRecordByKeySearch(frCfNext.Level + 1);
            UpdateMidUI();
        }
    }


}

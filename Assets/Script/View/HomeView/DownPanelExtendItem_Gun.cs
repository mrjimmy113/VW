using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownPanelExtendItem_Gun : DownPanelExtendItemControl
{
    public HorizontalLayoutGroup content;
    private List<GunSelectionItem> gunSelectionItems;
    public Transform gunSelectionItemPrefab;
    private float contentWidth = 300;
    private int enquipId;

    private ConfigGunDamageRecord dmgCf;
    private ConfigGunDamageRecord dmgCfNext;

    private ConfigGunRofRecord rofCf;
    private ConfigGunRofRecord rofCfNext;

    private int currentGold;

    public override void Setup()
    {
        if(gunSelectionItems == null)
        {
            gunSelectionItems = new List<GunSelectionItem>();
            List<ConfigGunRecord> gunRecords = ConfigurationManager.instance.gun.GetAll();
            List<int> activeGuns = DataAPIController.instance.GetAllActiveGun();
            RectTransform rect = content.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(contentWidth * gunRecords.Count, rect.sizeDelta.y);

            enquipId = DataAPIController.instance.GetCurrentEnquipId();

            foreach(var g in gunRecords)
            {
                Transform obj = Instantiate(gunSelectionItemPrefab);
                obj.SetParent(content.transform);
                obj.localScale = Vector3.one;
                GunSelectionItem item = obj.GetComponent<GunSelectionItem>();
                item.Setup(g, activeGuns.Contains(g.Id), enquipId);
                gunSelectionItems.Add(item);
            }

            root.btnDownBuy.onClick.AddListener(() =>
            {
                LevelUpFirePower();
            });

            root.btnMidBuy.onClick.AddListener(() =>
            {
                LevelUpStrength();
            });


            DataAPIController.instance.RegisterEvent(DataPath.ENQUIPS, OnChangeEnquip);
            DataAPIController.instance.RegisterEvent(DataPath.GOLD, OnGoldChangeEvent);

            OnChangeEnquip(enquipId);
        }

        




    }

    private void OnChangeEnquip(object arg0)
    {
        enquipId = (int)arg0;
        GunInfor gunInfor = DataAPIController.instance.GetGunInfor(enquipId);
        currentGold = DataAPIController.instance.GetCurrentGold();
        dmgCf = ConfigurationManager.instance.gunDmg.
            GetRecordByKeySearch(new Compare2KeySearch<int, int> 
            { key_1 = enquipId , key_2 = gunInfor.damageLevel}); ;
        dmgCfNext = ConfigurationManager.instance.gunDmg.
            GetRecordByKeySearch(new Compare2KeySearch<int, int>
            { key_1 = enquipId, key_2 = gunInfor.damageLevel + 1 }); ;

        rofCf = ConfigurationManager.instance.gunRof.
            GetRecordByKeySearch(new Compare2KeySearch<int, int>
            { key_1 = enquipId, key_2 = gunInfor.rofLevel }); ;
        rofCfNext = ConfigurationManager.instance.gunRof.
            GetRecordByKeySearch(new Compare2KeySearch<int, int>
            { key_1 = enquipId, key_2 = gunInfor.rofLevel + 1 }); ;

        if (root == null) root = GetComponent<DownPanelExtendItem>();

        UpdateDownUI();
        UpdateMidUI();
    }

    private void UpdateDownUI()
    {
        if (dmgCfNext != null)
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
        if (rofCfNext != null)
        {
            root.txtMidFieldName.text = "Strength [Lv" + rofCf.Level + "]";
            root.txtMidCoin.text = rofCfNext.UnlockFee.NumberNormalize();
            root.btnMidBuy.interactable = currentGold >= rofCfNext.UnlockFee;
        }
        else
        {
            root.txtMidFieldName.text = "Strength [Lv MAX]";
            root.txtMidCoin.text = "MAX";
            root.btnMidBuy.interactable = false;
        }

        root.txtMidValue.text = rofCf.Value.ToString();


    }

    private void OnGoldChangeEvent(object obj)
    {
        currentGold = (int)obj;
        if (dmgCfNext != null) root.btnDownBuy.interactable = currentGold >= dmgCfNext.UnlockFee;
        else root.btnDownBuy.interactable = false;

        if (rofCfNext != null) root.btnMidBuy.interactable = currentGold >= rofCfNext.UnlockFee && rofCfNext != null;
        else root.btnMidBuy.interactable = false;


    }

    private void LevelUpFirePower()
    {
        if (DataAPIController.instance.LevelUpGunDamage(enquipId))
        {
            dmgCf = dmgCfNext;
            dmgCfNext = ConfigurationManager.instance.gunDmg.
                GetRecordByKeySearch(new Compare2KeySearch<int, int>
                { key_1 = enquipId, key_2 = dmgCfNext.Level + 1 }); ;
            UpdateDownUI();
        }
    }

    private void LevelUpStrength()
    {
        if (DataAPIController.instance.LevelUpGunRof(enquipId))
        {
            rofCf = rofCfNext;
            rofCfNext = ConfigurationManager.instance.gunRof.
                GetRecordByKeySearch(new Compare2KeySearch<int, int>
                { key_1 = enquipId, key_2 = rofCfNext.Level + 1 }); ;
            UpdateMidUI();
        }
    }



}

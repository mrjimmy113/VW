using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.UI;

public static class GunSelectionItemConfig
{
    public static readonly string INACTIVE_BG = "icnBg_3";
    public static readonly string ENQUIP_BG = "icnBg_2";
    public static readonly string ACTIVE_BG = "icnBg_1";
}

public class GunSelectionItem : MonoBehaviour
{
    public Image bg;
    public Image gun;
    private Button btn;

    private ConfigGunRecord record;


    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        
        btn.onClick.AddListener(() =>
        {
            DataAPIController.instance.ChangeEnquipGun(record.Id);
        });
        btn.interactable = false;
    }

    public void Setup(ConfigGunRecord cf, bool isActive, int enquipId) 
    {
        record = cf;
       if(isActive)
        {
            btn.interactable = true;
            if (enquipId == cf.Id)
            {
                bg.sprite = SpriteLiblary.instance.GetSpriteByName(GunSelectionItemConfig.ENQUIP_BG);
                btn.interactable = false;
            }
            else bg.sprite = SpriteLiblary.instance.GetSpriteByName(GunSelectionItemConfig.ACTIVE_BG);
            gun.sprite = SpriteLiblary.instance.GetSpriteByName(cf.ActiveSprite);
            DataAPIController.instance.RegisterEvent(DataPath.ENQUIPS, OnEnquipChange);
        }
        else
        {
            bg.sprite = SpriteLiblary.instance.GetSpriteByName(GunSelectionItemConfig.INACTIVE_BG);
            gun.sprite = SpriteLiblary.instance.GetSpriteByName(cf.InActiveSprite);
        }

        
    }

    private void OnEnquipChange(object arg0)
    {
        int id = (int)arg0;
        if (id == record.Id)
        {
            bg.sprite = SpriteLiblary.instance.GetSpriteByName(GunSelectionItemConfig.ENQUIP_BG);
            btn.interactable = false;
        }
        else
        {
            bg.sprite = SpriteLiblary.instance.GetSpriteByName(GunSelectionItemConfig.ACTIVE_BG);
            btn.interactable = true;
        }
    }
}

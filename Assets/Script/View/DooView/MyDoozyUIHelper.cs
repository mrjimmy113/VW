using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class DooName
{
    public static readonly string HOME_VIEW_DOWN_PANEL = "HomeViewDownPanel";
    public static readonly string COIN_UPGRADE = "CoinUpgrade";
    public static readonly string GUN_UPGRADE = "GunUpgrade";
    public static readonly string PLAYER_UPGRADE = "PlayerUpgrade";

    public static readonly string MASTER_VIEW = "MasterElement";
    public static readonly string LOADING_VIEW = "LoadingView";
    public static readonly string HOME_VIEW = "HomeView";
    public static readonly string INGAME_VIEW = "IngameView";
}


public class MyDoozyUIHelper
{



    public static BaseView ShowView(string name)
    {
        UIManager.ShowUiElement(name,DooName.MASTER_VIEW);
        UIElement element = UIManager.GetUiElements(name, DooName.MASTER_VIEW)[0];
        BaseView baseView = element.GetComponentInChildren<BaseView>();
        if (baseView == null) Debug.LogError("NULL BASE VIEW");
        baseView.Setup(null);
        return baseView;

    }

    

    public static void HideAllElementInCategory(string category)
    {
        foreach(var e in UIManager.ElementDatabase)
        {
            foreach(var cl in e.Value)
            {
                if (cl.elementCategory == category) UIManager.HideUiElement(cl.elementName, cl.elementCategory);
            }
        }

    }

    public static void ShowElementInCategoryByIndex(string category, int index)
    {
        UIManager.ShowUiElement((UIManager.ElementDatabase[category])[index].elementName,category);
    }
}

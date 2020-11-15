using DG.Tweening;
using DoozyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameView : BaseView
{
    public Image imgProgress;
    public Text txtProgress;
    public Text txtGold;
    public RectTransform buffDebuffPanel;
    public BuffDebuffItem buffDebuffItemPrefab;
    public CanvasGroup resultPanel;
    public Text txtGoldEarned;
    public LevelPanel levelPanel;


    private List<BuffDebuffItem> buffDebuffItems = new List<BuffDebuffItem>();
    private BuffDebuffControl buffDebuffControl;

    private bool isSetupOneTime = true;

    

    public override void Setup(ViewParam param)
    {
        base.Setup(param);

        int currentLevel = MissionManager.instance.currentMission;
        levelPanel.Setup(currentLevel);
        UpdateProgress(0, 1);
        txtGold.text = 0 + "";
        resultPanel.blocksRaycasts = false;


        buffDebuffControl = GameObject.FindGameObjectWithTag("Player").GetComponent<BuffDebuffControl>();
        buffDebuffControl.AddBuffDebuffEvent += AddBuffDebuff;
        buffDebuffControl.BuffDebuffProgressEvent += BuffDebuffProgress;
        buffDebuffControl.RemoveBuffDebuffEvent += RemoveBuffDebuff;
        MissionManager.instance.OnEnemyDeadEvent += UpdateProgress;
        MissionManager.instance.OnGoldEarnedIncrease += UpdateGold;
        MissionManager.instance.OnGameEnd += OnGameEnd;

        resultPanel.alpha = 0;

        if (isSetupOneTime)
        {
            
            isSetupOneTime = false;
     
        }

        foreach(var bd in buffDebuffItems)
        {
            bd.Reset();
            bd.gameObject.SetActive(false);
        }

    }

    private void OnGameEnd(bool isWin)
    {
        resultPanel.DOFade(1, 0.5f);
        resultPanel.blocksRaycasts = true;
        txtGoldEarned.text = MissionManager.instance.goldEarned.NumberNormalize();
    }

    private void RemoveBuffDebuff(int id)
    {
        foreach(var i in buffDebuffItems)
        {
            
            if (i.id == id)
            {
                i.Reset();
                i.gameObject.SetActive(false);
            }
        }
    }

    private void BuffDebuffProgress(int id, float remainTime)
    {
        foreach(var i in buffDebuffItems)
        {
            if (i.id == id) i.imgProgress.DOFillAmount(remainTime / i.fullTime, 0.2f).SetEase(Ease.Linear);
        }
    }

    private void AddBuffDebuff(string sprite, int id, float remainTime)
    {
        foreach(var i in buffDebuffItems)
        {
            if (i.id == id)
            {
                i.imgProgress.fillAmount = 1;
                return;
            }
        }

        BuffDebuffItem item = GetInActiveItem();
        if(item == null)
        {
            item = Instantiate(buffDebuffItemPrefab);
            item.transform.SetParent(buffDebuffPanel);
            item.transform.localScale = Vector3.one;
            buffDebuffItems.Add(item);
        }
        else
        {
            item.gameObject.SetActive(true);
        }
        item.GetComponent<BuffDebuffItem>().Setup(sprite, id,remainTime);
    }

    private BuffDebuffItem GetInActiveItem()
    {
        foreach(var item in buffDebuffItems)
        {
            if (!item.gameObject.activeSelf) return item;
        }
        return null;
    }

    private void UpdateGold(int gold)
    {
        txtGold.text = gold.NumberNormalize();
    }

    private void UpdateProgress(int totalDead,int total)
    {
        float percent =1 - (float)totalDead / total;
        percent = Mathf.Clamp(percent, 0, 1);

        imgProgress.DOFillAmount(percent, 0.2f);
        txtProgress.text = Mathf.RoundToInt(percent * 100) + "%";
    }


    public void Confirm()
    {

        LoadScenceManager.instance.LoadScenceByNameInstant("Buffer", () =>
        {
            MyDoozyUIHelper.HideAllElementInCategory(DooName.MASTER_VIEW);
            MyDoozyUIHelper.ShowView(DooName.HOME_VIEW);
        });
    }
}

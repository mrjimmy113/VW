using DG.Tweening;
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
    public Text txtPreviousLevel;
    public Text txtCurrentLevel;
    public Text txtNextLevel;
    public RectTransform buffDebuffPanel;
    public BuffDebuffItem buffDebuffItemPrefab;
    public CanvasGroup resultPanel;
    public Text txtGoldEarned;


    private List<BuffDebuffItem> buffDebuffItems = new List<BuffDebuffItem>();
    private PlayerControl playerControl;

    private bool isSetupOneTime = true;

    

    public override void Setup(ViewParam param)
    {
        base.Setup(param);

        int currentLevel = MissionManager.instance.currentMission;
        txtPreviousLevel.text = currentLevel - 1 + "";
        txtCurrentLevel.text = currentLevel + "";
        txtNextLevel.text = currentLevel + 1 + "";
        UpdateProgress(0, 1);
        txtGold.text = 0 + "";
        resultPanel.blocksRaycasts = false;


        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        playerControl.AddBuffDebuffEvent += AddBuffDebuff;
        playerControl.BuffDebuffProgressEvent += BuffDebuffProgress;
        playerControl.RemoveBuffDebuffEvent += RemoveBuffDebuff;
        MissionManager.instance.OnEnemyDeadEvent += UpdateProgress;
        MissionManager.instance.OnGoldEarnedIncrease += UpdateGold;
        MissionManager.instance.OnGameEnd += OnGameEnd;

        resultPanel.alpha = 0;

        if (isSetupOneTime)
        {
            
            isSetupOneTime = false;
     
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
            if (i.id == id) i.gameObject.SetActive(false);
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
        ViewManager.instance.OnSwitchView(ViewIndex.EmptyView,null,(v) => {

            LoadScenceManager.instance.LoadScenceByNameInstant("Buffer", () =>
            {

            });
        });
      
    }
}

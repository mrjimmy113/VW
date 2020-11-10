using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldEarnedPanel : MonoBehaviour
{
    public Image imgProgress;
    public Text txtGoldEarned;
    private int currentGoldEarned;
    private bool isSetup = false;
    private bool isFirstTime = true;
    public void Setup()
    {
        if(!isSetup)
        {
            currentGoldEarned = DataAPIController.instance.GetGoldSaved();
            txtGoldEarned.text = currentGoldEarned.NumberNormalize();
            ResourcesGainHandler.instance.GoldEarnedHandler += UpdateProgress;
            DataAPIController.instance.RegisterEvent(DataPath.GOLDDAILYSAVED, UpdateGoldEarned);
            imgProgress.fillAmount = 0;
            isSetup = true;
        }
    }

    private void UpdateProgress(long remainTime)
    {
        float tweenTime = 0.95f;
        float percent = (float)remainTime / ResoucesConfig.GOLD_GAIN_TIME;

        percent += 0.1f;
        if(isFirstTime)
        {
            imgProgress.fillAmount = percent - 0.1f;
            imgProgress.DOFillAmount(percent, tweenTime).SetEase(Ease.Linear);
            isFirstTime = false;
            return;
        }

        if(percent < 0.2f && percent > 0f)
        {
            
            imgProgress.fillAmount = 0;
            imgProgress.DOFillAmount(0.1f, tweenTime).SetEase(Ease.Linear);
            return;
        }
        
        imgProgress.DOFillAmount(percent, tweenTime).SetEase(Ease.Linear);
    }

    private void UpdateGoldEarned(object obj)
    {
        int goldEarned = (int)obj;
        currentGoldEarned = goldEarned;
        txtGoldEarned.text = currentGoldEarned.NumberNormalize();
    }

    public void ConsumeEarnedGold()
    {
        
        DataAPIController.instance.AddGold(DataAPIController.instance.GetGoldSaved());
        DataAPIController.instance.ResetGoldSaved();
    }


}

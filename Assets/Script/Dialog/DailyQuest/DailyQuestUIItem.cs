using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyQuestUIItem : MonoBehaviour
{
    public Text txtReward;
    public Text txtQuestTitle;
    public Text txtQuestProgress;
    public Image imgQuestProgress;
    public Button btnCollect;
    public RectTransform clearPanel;

    private DailyQuest dq;

    private bool isOneTimeSetup = true;

    public void Setup(DailyQuest dq)
    {
        this.dq = dq;
        txtReward.text = dq.detail.reward.NumberNormalize();
        txtQuestTitle.text = dq.cf.Description;
        txtQuestProgress.text = dq.detail.currentProgress.NumberNormalize() + " / " + dq.detail.require.NumberNormalize();
        imgQuestProgress.fillAmount = (float)dq.detail.currentProgress / dq.detail.require;
        btnCollect.interactable = dq.detail.currentProgress >= dq.detail.require && !dq.detail.isRewarded;
        clearPanel.gameObject.SetActive(dq.detail.isRewarded);
        if(isOneTimeSetup)
        {
            btnCollect.onClick.AddListener(() =>
            {
                Collect();
            });
            isOneTimeSetup = false;
        }
        

    }

    private void Collect()
    {
        dq.CollectReward();
        btnCollect.interactable = false;
        clearPanel.gameObject.SetActive(true);
    }
}

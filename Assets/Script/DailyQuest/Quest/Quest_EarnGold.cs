using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_EarnGold : DailyQuest
{
    public override void SetupProgress()
    {
        MissionManager.instance.OnGoldEarnedIncrease += UpdateProgress;
    }

    protected override QuestDetail CreateNewQuestExtra(QuestDetail detail)
    {
        ConfigPlayerCoinValueRecord coinCf =
            ConfigurationManager.instance.playerCoinValue.GetRecordByKeySearch(
                
                DataAPIController.instance.GetCurrentGoldValueLevel()
                );

        detail.require = detail.require * coinCf.Value;

        return base.CreateNewQuestExtra(detail);
    }

    private void UpdateProgress(int data)
    {
        if(!detail.isRewarded)
        {
            detail.currentProgress += data;
            if (detail.currentProgress >= detail.require)
            {
                detail.currentProgress = detail.require;
                MissionManager.instance.OnGoldEarnedIncrease -= UpdateProgress;
            }
            DataAPIController.instance.UpdateQuestDetail(detail.id, detail);
        }
    }
}

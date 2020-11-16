using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_ClearBossMission : DailyQuest
{
    public override void SetupProgress()
    {
        MissionManager.instance.OnMissionClearEvent += UpdateProgress;
    }

    private void UpdateProgress()
    {
        ConfigMissionRecord record = 
            ConfigurationManager.instance.mission.GetRecordByKeySearch(
                DataAPIController.instance.GetCurrentMission()
                );
        if (!detail.isRewarded && record.IsBossMission)
        {
            detail.currentProgress++;
            if (detail.currentProgress >= detail.require)
            {
                detail.currentProgress = detail.require;
                MissionManager.instance.OnMissionClearEvent -= UpdateProgress;
            }
            DataAPIController.instance.UpdateQuestDetail(detail.id, detail);
        }
    }
}


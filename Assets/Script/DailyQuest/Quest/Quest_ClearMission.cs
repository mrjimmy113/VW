using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_ClearMission : DailyQuest
{
    public override void SetupProgress()
    {
        MissionManager.instance.OnMissionClearEvent += UpdateProgress;
    }

    private void UpdateProgress()
    {
        if (!detail.isRewarded)
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

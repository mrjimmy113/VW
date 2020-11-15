using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_KillEnemies : DailyQuest
{
    public override void SetupProgress()
    {
        MissionManager.instance.OnEnemyDeadEventQuest += UpdateProgress;
    }

    protected  void UpdateProgress(object data)
    {
        if(!detail.isRewarded)
        {
            detail.currentProgress++;
            if(detail.currentProgress >= detail.require)
            {
                MissionManager.instance.OnEnemyDeadEventQuest -= UpdateProgress;
            }
            DataAPIController.instance.UpdateQuestDetail(detail.id, detail);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_KillEnemies : DailyQuest
{
    public override void SetupProgress()
    {
        EnemyFactory.instance.OnEnemyDeadEvent += UpdateProgress;
    }

    protected  void UpdateProgress(OnEnemyDeadParam data)
    {
        if(!detail.isRewarded && data.isCountDead)
        {
            detail.currentProgress++;
            if(detail.currentProgress >= detail.require)
            {
                detail.currentProgress = detail.require;
                EnemyFactory.instance.OnEnemyDeadEvent -= UpdateProgress;
            }
            DataAPIController.instance.UpdateQuestDetail(detail.id, detail);
        }
        
    }
}

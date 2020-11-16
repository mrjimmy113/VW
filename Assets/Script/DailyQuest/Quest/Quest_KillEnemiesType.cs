using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_KillEnemiesType : DailyQuest
{

    protected override QuestDetail CreateNewQuestExtra(QuestDetail detail)
    {
        KillSpecificEnemyDetail extraDetail = new KillSpecificEnemyDetail();
        extraDetail.enemyId = ConfigurationManager.instance.enemyType.GetRandom().Id;

        detail.other = extraDetail;


        return base.CreateNewQuestExtra(detail);
    }


    public override void SetupProgress()
    {
        //MissionManager.instance.OnEnemyDeadEventQuest += UpdateProgress;
    }

    protected  void UpdateProgress(object data)
    {
        /*OnEnemyDeadParam param = (OnEnemyDeadParam)data;
        KillSpecificEnemyDetail d = (KillSpecificEnemyDetail)detail.other;
        if(d.enemyId == param.enemyType && !detail.isRewarded)
        {
            detail.currentProgress++;
            if (detail.currentProgress >= detail.require)
            {
                detail.isRewarded = true;
            }
            DataAPIController.instance.UpdateQuestDetail(detail.id,detail);
        }*/
    }
}

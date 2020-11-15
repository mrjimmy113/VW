using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DailyQuest : MonoBehaviour
{
    public ConfigDailyQuestRecord cf;
    public QuestDetail detail;


    public QuestDetail CreateNewQuest(ConfigDailyQuestRecord record)
    {
        cf = record;
        QuestDetail questDetail = new QuestDetail();
        questDetail.id = record.Id;
        questDetail.isRewarded = false;
        questDetail.reward = UnityEngine.Random.Range(record.MinReward, record.MaxReward + 1);
        questDetail.require = UnityEngine.Random.Range(record.MinRequire, record.MaxRequire + 1);
        questDetail.isRewarded = false;
        questDetail.currentProgress = 0;
        detail = questDetail;
        return CreateNewQuestExtra(questDetail);

    }

    public void LoadFromQuestDetail(QuestDetail questDetail)
    {
        detail = questDetail;
        cf = ConfigurationManager.instance.dailyQuest.GetRecordByKeySearch(detail.id);
    }

    protected virtual QuestDetail CreateNewQuestExtra(QuestDetail detail)
    {
        return detail;
    }

    public abstract void SetupProgress();

    public void CollectReward()
    {
        detail.isRewarded = true;
        DataAPIController.instance.UpdateQuestDetail(detail.id, detail);
    }

    
}

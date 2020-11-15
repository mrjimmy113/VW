using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyQuestControl : Singleton<DailyQuestControl>
{

    List<DailyQuest> quests = new List<DailyQuest>();

    public void InitQuest()
    {
        QuestData questData = DataAPIController.instance.GetQuestData();
        if(questData == null)
        {
            CreateNewQuestDetail();
        }else if(questData.time != DateTime.Now.DayOfYear)
        {
            CreateNewQuestDetail();
        }else
        {
            foreach(var d in questData.detail)
            {
                ConfigDailyQuestRecord cf = ConfigurationManager.instance.dailyQuest.GetRecordByKeySearch(d.Value.id);
                GameObject obj = Instantiate(Resources.Load(cf.Prefab) as GameObject, this.transform);
                DailyQuest dq = obj.GetComponent<DailyQuest>();
                dq.LoadFromQuestDetail(d.Value);
                quests.Add(dq);
            }
        }




     
    }

    public void Setup()
    {
        foreach (var q in quests)
        {
            if (!q.detail.isRewarded) q.SetupProgress();
        }
    }

    private void CreateNewQuestDetail()
    {
        List<ConfigDailyQuestRecord> cfs = ConfigurationManager.instance.dailyQuest.GetRandomAmount(3);
        QuestData data = new QuestData();
        Dictionary<string, QuestDetail> dic = new Dictionary<string, QuestDetail>();
        foreach(var cf in cfs)
        {
            GameObject obj = Instantiate(Resources.Load(cf.Prefab) as GameObject, this.transform);
            DailyQuest dq = obj.GetComponent<DailyQuest>();
            dic.Add(cf.Id.ToKey(), dq.CreateNewQuest(cf));
            quests.Add(dq);
        }

        data.detail = dic;
        data.time = DateTime.Now.DayOfYear;
        DataAPIController.instance.SetQuestData(data);
    }
}

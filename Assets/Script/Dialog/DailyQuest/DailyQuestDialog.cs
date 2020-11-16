using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyQuestDialog : DooBaseDialog
{
    public List<DailyQuestUIItem> uIItems;

    public override void Setup(DialogParam param)
    {
        List<DailyQuest> dqs = DailyQuestControl.instance.quests;

        for (int i = 0; i < uIItems.Count; i++)
        {
            uIItems[i].Setup(dqs[i]);
        }
    }

    public void HideDialog()
    {
        DooDialogManager.instance.HideDialog(dialogIndex);
    }
}

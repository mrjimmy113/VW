using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotivationPanel : MonoBehaviour
{
    public void OpenDailyQuestDialog()
    {
        DooDialogManager.instance.OnShowDialog(DooDialogIndex.DailyQuestDialog);
    }
}

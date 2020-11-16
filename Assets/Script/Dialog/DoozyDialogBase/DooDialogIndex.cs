using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DooDialogIndex
{
   None,
   DailyQuestDialog
}

public class DooDialogConfig
{
    public static DooDialogIndex[] DialogIndices =
    {
        DooDialogIndex.DailyQuestDialog
    };
}

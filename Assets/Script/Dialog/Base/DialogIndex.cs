using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    None,
    MissionResultDialog,
}
public class DialogConfig
{
    public static DialogIndex[] DialogIndices = { 
       DialogIndex.MissionResultDialog

    };
}
public class DialogParam
{

}

public class MissionResultDialogParam :DialogParam
{
    public int goldEarned;
    public bool isWin;
}


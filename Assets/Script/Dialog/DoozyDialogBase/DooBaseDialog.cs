using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DooBaseDialog : MonoBehaviour
{
    public DooDialogIndex dialogIndex;
    


    public void ShowDialog(DialogParam param, Action callback)
    {
        Setup(param);


    }

    public virtual void OnShowDialog()
    {

    }
    public virtual void Setup(DialogParam param)
    {

    }
    public void HideDialog(Action callback)
    {


    }
    public virtual void OnHideDialog()
    {

    }
}
public class DialogParam
{

}

public class DailyQuestDialogParam : DialogParam
{
    public DailyQuest dq;
}

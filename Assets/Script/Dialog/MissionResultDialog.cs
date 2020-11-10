using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionResultDialog : BaseDialog
{
    public Text txtGoldEarned;
    public Text txtResult;


    public override void Setup(DialogParam param)
    {
        MissionResultDialogParam p = (MissionResultDialogParam)param;
        txtGoldEarned.text = p.goldEarned.NumberNormalize() + " Collect" ;
        if (p.isWin) txtResult.text = "WIN";
        else txtResult.text = "LOSE";
    }

    
}

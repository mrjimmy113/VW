using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class DooDialogManagerConfig
{
    public static readonly string DIALOG_CATE = "Dialog";
}

public class DooDialogManager : Singleton<DooDialogManager>
{
    private Dictionary<DooDialogIndex, DooBaseDialog> dicDialog =
        new Dictionary<DooDialogIndex, DooBaseDialog>();

    private List<DooBaseDialog> lsDialogShow = new List<DooBaseDialog>();

    private void OnEnable()
    {
        DooBaseDialog[] dialogs;
        dialogs = FindObjectsOfType<DooBaseDialog>();

        foreach(var d in dialogs)
        {
            dicDialog.Add(d.dialogIndex, d);
        }
    }


    public int ShowedDialogCount { get { return lsDialogShow.Count; } }

    public void OnShowDialog(DooDialogIndex index)
    {
        
        DooBaseDialog dialog = dicDialog[index];
        dialog.Setup(null);
        UIManager.ShowUiElement(index.EnumToString(), DooDialogManagerConfig.DIALOG_CATE);
        dialog.OnShowDialog();
        
    }

    public void HideDialog(DooDialogIndex index)
    {
        DooBaseDialog dialog = dicDialog[index];
        
        UIManager.HideUiElement(index.EnumToString(), DooDialogManagerConfig.DIALOG_CATE);

        dialog.OnHideDialog();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{

    public Transform anchorDialog;
    private Dictionary<DialogIndex, BaseDialog> dicDialog = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> lsdialogShow = new List<BaseDialog>();
    // Start is called before the first frame update



    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public override void OnAwake()
    {
        foreach (DialogIndex e in DialogConfig.DialogIndices)
        {
            GameObject dialogObject = Instantiate(Resources.Load("Dialog/" + e.ToString(), typeof(GameObject))) as GameObject;
            dialogObject.transform.SetParent(anchorDialog, false);
            BaseDialog dialog = dialogObject.GetComponent<BaseDialog>();
            dicDialog.Add(dialog.dialogIndex, dialog);
            dialogObject.SetActive(false);
        }
    }

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    public bool IsHitUIOnCanvas()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);


        return results.Count > 0;
    }

    public int ShowedDialogCount { get { return lsdialogShow.Count; } }
    public void OnShowDialog(DialogIndex index, DialogParam param = null, Action<BaseDialog> callback = null)
    {

        BaseDialog dialog = dicDialog[index];
        dialog.gameObject.SetActive(true);
        dialog.transform.SetAsLastSibling();
        dialog.ShowDialog(param, () => {
            callback?.Invoke(dialog);
            
        });
        lsdialogShow.Add(dialog);

    }
    public void HideDialog(DialogIndex index)
    {
        BaseDialog dialog = dicDialog[index];
        dialog.HideDialog(()=> {
            dialog.gameObject.SetActive(false);
            lsdialogShow.Remove(dialog);
        });
        
    }
    public void HideAllDialog()
    {
         foreach (BaseDialog dialog in lsdialogShow)
        {
            dialog.HideDialog(() => {
                dialog.gameObject.SetActive(false);
            });
        }
        lsdialogShow.Clear();
    }

    

}

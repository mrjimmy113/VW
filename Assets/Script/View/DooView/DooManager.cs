using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;
public class DooManager : MonoBehaviour
{
    public void ShowView(string name)
    {
        UIManager.ShowUiElementAndHideAllTheOthers(name, "", false);
        UIElement element = UIManager.GetVisibleUIElements()[0];
        element.GetComponentInChildren<BaseView>();

    }
}

using DoozyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPanelExtendControl : MonoBehaviour
{
    public List<DownPanelExtendItemControl> itemControls;
    public List<DownPanelButton> buttons;

    public void Setup()
    {
        for (int i = 0; i < itemControls.Count; i++)
        {
            itemControls[i].Setup();
            
            buttons[i].self.sprite = buttons[i].normal;
        }
    }

    public void Open(int index, string viewName)
    {
        
        HideAll();
        UIManager.ShowUiElement(viewName, DooName.HOME_VIEW_DOWN_PANEL);
        buttons[index].self.sprite = buttons[index].selected;
    }

    public void HideAll()
    {
        MyDoozyUIHelper.HideAllElementInCategory(DooName.HOME_VIEW_DOWN_PANEL);
        for(int i = 0; i< itemControls.Count;i++)
        {
            buttons[i].self.sprite = buttons[i].normal;
        }
    }
}

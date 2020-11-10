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
            itemControls[i].gameObject.SetActive(false);
            buttons[i].self.sprite = buttons[i].normal;
        }
    }

    public void Open(int index)
    {
        HideAll();
        itemControls[index].gameObject.SetActive(true);
        buttons[index].self.sprite = buttons[index].selected;
    }

    public void HideAll()
    {
        for (int i = 0; i < itemControls.Count; i++)
        {
            itemControls[i].gameObject.SetActive(false);
            buttons[i].self.sprite = buttons[i].normal;
        }
 
    }
}

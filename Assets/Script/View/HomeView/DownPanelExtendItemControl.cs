using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DownPanelExtendItemControl : MonoBehaviour
{
    protected DownPanelExtendItem root;

    private void OnEnable()
    {
        root = GetComponent<DownPanelExtendItem>();
    }

    public abstract void Setup();
}

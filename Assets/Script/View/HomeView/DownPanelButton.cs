using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownPanelButton : MonoBehaviour
{
    public Sprite normal;
    public Sprite selected;
    [HideInInspector]
    public Image self;

    private void Awake()
    {
        self = GetComponent<Image>();
    }
}

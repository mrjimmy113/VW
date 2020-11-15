using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDebuffItem : MonoBehaviour
{
    public Image imgProgress;
    public Image imgIcon;
    public int id;
    public float fullTime;

    public void Setup(string sprite, int id, float fullTime)
    {
        imgIcon.sprite = SpriteLiblary.instance.GetSpriteByName(sprite);
        this.id = id;
        imgProgress.fillAmount = 1;
        this.fullTime = fullTime;
    }

    public void Reset()
    {
        id = 0;
        fullTime = 0;
    }
}

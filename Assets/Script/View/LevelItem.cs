using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public Image imgBoss;
    public Text txtLevel;

    public void Setup(int lvl, bool isBoss)
    {
        
        txtLevel.text = lvl.ToString();
        imgBoss.gameObject.SetActive(isBoss);
        gameObject.SetActive(lvl != 0);
    }
}

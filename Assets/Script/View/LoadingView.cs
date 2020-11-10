using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : BaseView
{
    [SerializeField]
    private Text txtTip = null;
    [SerializeField]
    private RectTransform tipBg = null;


    [SerializeField]
    private Text txtProgress = null;
    [SerializeField]
    private Image imgProgress = null;


    void OnEnable()
    {
        txtTip.text = "";
        txtProgress.text = "";
        imgProgress.fillAmount = 0;
    }


    public override void Setup(ViewParam param)
    {
        StartCoroutine(Tip());
        
        imgProgress.fillAmount = 0;
        txtProgress.text = "0%";
    }

    IEnumerator Tip()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        while(true)
        {
            ConfigTipRecord tip = ConfigurationManager.instance.tip.GetRandom();
            Vector2 tipBgSize = tipBg.sizeDelta;
            tipBgSize.x = Mathf.RoundToInt(((float)txtTip.fontSize / 2f) * tip.Tip.Length);
            tipBgSize.x = Mathf.Clamp(tipBgSize.x, 0, 1800);
            tipBg.sizeDelta = tipBgSize;
            txtTip.text = tip.Tip;
            yield return wait;
        }
    }

    public void UpdateProgress(float val)
    {
        val = Mathf.Clamp(val, 0, 1f);
        imgProgress.fillAmount = val;
        txtProgress.text = Mathf.RoundToInt(val * 100f).ToString() + "%";
    }

    public override void OnHideView()
    {
        StopCoroutine(Tip());
    }
}

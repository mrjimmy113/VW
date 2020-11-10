public class DownPanelExtendItem_Coin : DownPanelExtendItemControl
{

    private ConfigPlayerDailyCoinRecord dailyCf;
    private ConfigPlayerDailyCoinRecord dailyCfNext;

    private ConfigPlayerCoinValueRecord valueCf;
    private ConfigPlayerCoinValueRecord valueCfNext;

    private int currentGold;

    private bool isSetupOneTime = false;

    public override void Setup()
    {
        currentGold = DataAPIController.instance.GetCurrentGold();

        

        if(root == null) root = GetComponent<DownPanelExtendItem>();

        if(!isSetupOneTime)
        {
            isSetupOneTime = true;

            int dailyLevel = DataAPIController.instance.GetCurrentDamageLevel();
            int valueLevel = DataAPIController.instance.GetCurrentFireRateLevel();
            dailyCf = ConfigurationManager.instance.playerDailyCoin.GetRecordByKeySearch(dailyLevel);
            dailyCfNext = ConfigurationManager.instance.playerDailyCoin.GetRecordByKeySearch(dailyLevel + 1);
            valueCf = ConfigurationManager.instance.playerCoinValue.GetRecordByKeySearch(valueLevel);
            valueCfNext = ConfigurationManager.instance.playerCoinValue.GetRecordByKeySearch(valueLevel + 1);

            root.btnDownBuy.onClick.AddListener(() =>
            {
                LevelUpDailyCoin();
            });
            root.btnMidBuy.onClick.AddListener(() =>
            {
                LevelUpCoinValue();
            });

            DataAPIController.instance.RegisterEvent(DataPath.GOLD, OnGoldChangeEvent);
        }


        UpdateDownUI();
        UpdateMidUI();
       

        

    }

    private void UpdateDownUI()
    {
        if (dailyCfNext != null)
        {
            root.txtDownFieldName.text = "Gold daily [Lv" + dailyCf.Level + "]";
            root.txtDownCoin.text = dailyCfNext.UnlockFee.NumberNormalize();
            root.btnDownBuy.interactable = currentGold >= dailyCfNext.UnlockFee;
        }
        else
        {
            root.txtDownFieldName.text = "Gold daily [Lv MAX]";
            root.txtDownCoin.text = "MAX";
            root.btnDownBuy.interactable = false;
        }

        root.txtDownValue.text = dailyCf.Value.ToString();


    }

    private void UpdateMidUI()
    {
        if (valueCfNext != null)
        {
            root.txtMidFieldName.text = "Gold value [Lv" + valueCf.Level + "]";
            root.txtMidCoin.text = valueCfNext.UnlockFee.NumberNormalize();
            root.btnMidBuy.interactable = currentGold >= valueCfNext.UnlockFee;
        }
        else
        {
            root.txtMidFieldName.text = "Gold value [Lv MAX]";
            root.txtMidCoin.text = "MAX";
            root.btnMidBuy.interactable = false;
        }

        root.txtMidValue.text = valueCf.Value.ToString();


    }

    private void OnGoldChangeEvent(object obj)
    {
        currentGold = (int)obj;
        if (dailyCfNext != null) root.btnDownBuy.interactable = currentGold >= dailyCfNext.UnlockFee;
        else root.btnDownBuy.interactable = false;

        if (valueCfNext != null) root.btnMidBuy.interactable = currentGold >= valueCfNext.UnlockFee && valueCfNext != null;
        else root.btnMidBuy.interactable = false;


    }

    private void LevelUpDailyCoin()
    {
        if (DataAPIController.instance.LevelUpDailyCoin())
        {
            dailyCf = dailyCfNext;
            dailyCfNext = ConfigurationManager.instance.playerDailyCoin.GetRecordByKeySearch(dailyCf.Level + 1);
            UpdateDownUI();
        }
    }

    private void LevelUpCoinValue()
    {
        if (DataAPIController.instance.LevelUpCoinValue())
        {
            valueCf = valueCfNext;
            valueCfNext = ConfigurationManager.instance.playerCoinValue.GetRecordByKeySearch(valueCfNext.Level + 1);
            UpdateMidUI();
        }
    }


}

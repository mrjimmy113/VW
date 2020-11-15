using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    public List<LevelItem> levelItems;
    public Image imgPre;
    public Image imgNext;

    public void Setup(int currentLevel)
    {
        int level = currentLevel - 1;
        foreach (var i in levelItems)
        {
            ConfigMissionRecord mis =
                ConfigurationManager.instance.mission.GetRecordByKeySearch(level);
            if (mis == null)
            {
                imgPre.gameObject.SetActive(!(level == currentLevel - 1));
                imgNext.gameObject.SetActive(!(level == currentLevel + 1));
                i.Setup(0, false);
            }
            else
            {
                i.Setup(level, mis.IsBossMission);
            }

            level++;
        }
    }
}

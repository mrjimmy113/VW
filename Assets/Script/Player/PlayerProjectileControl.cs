using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerProjectileControl : MonoBehaviour
{
    public Dictionary<string,string> projectileTable;

    private void Awake()
    {
        
    }

    public Sprite GetCurrentProjectileSprite(List<int> input)
    {
        string id = input.ListIntToString();
        return null;
    }
}

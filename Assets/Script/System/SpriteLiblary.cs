using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLiblary : Singleton<SpriteLiblary>
{
    [SerializeField]
    private Sprite[] sprites = null;
    private Dictionary<string, Sprite> dicSprite = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
           foreach(Sprite e in sprites)
        {
            
            dicSprite.Add(e.name, e);
        }
    }
    public Sprite GetSpriteByName(string name_)
    {
        return dicSprite[name_];
    }
  
}

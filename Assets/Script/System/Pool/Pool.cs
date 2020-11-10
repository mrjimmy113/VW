using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool 
{
    public string namePool;
    public Transform prefab;
    public int total;
    public List<Transform> elements = new List<Transform>();
    private int index = -1;
    public Pool(string name,int total,Transform prefab)
    {
        this.namePool = name;
        this.total = total;
        this.prefab = prefab;
    }
    public Pool()
    {
     
    }
    public Transform Spwan()
    {
        index++;
        if(index>=elements.Count)
        {
            index = 0;
        }
        Transform trans = elements[index];
        trans.gameObject.SetActive(true);
        trans.gameObject.SendMessage("OnSpwan", SendMessageOptions.DontRequireReceiver);
        return trans;
    }
    public void Despwan(Transform trans)
    {
        if(elements.Contains(trans))
        {
            trans.gameObject.SendMessage("OnDespwan", SendMessageOptions.DontRequireReceiver);
            trans.gameObject.SetActive(false);
        }
    }

}

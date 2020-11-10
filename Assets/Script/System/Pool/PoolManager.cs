using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
  
    public  Dictionary<string, Pool> dicPool = new Dictionary<string, Pool>();
    // Start is called before the first frame update
    void Start()
    {
        
  
    }
    public  void AddNewPool(Pool pool)
    {
        if(!dicPool.ContainsKey(pool.namePool))
        {
            CreatePool(pool);
            dicPool.Add(pool.namePool, pool);
        }else
        {
            AddPool(pool);
            
        }
    }
    private void CreatePool(Pool pool)
    {
        for(int i=0;i<pool.total;i++)
        {
            Transform trans = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
            pool.elements.Add(trans);
            trans.gameObject.SetActive(false);
        }
    }

    private void AddPool(Pool pool)
    {
        Pool oldPool = dicPool[pool.namePool];
        for (int i = 0; i < pool.total; i++)
        {
            Transform trans = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
            oldPool.elements.Add(trans);
            trans.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        dicPool.Clear();
    }
}

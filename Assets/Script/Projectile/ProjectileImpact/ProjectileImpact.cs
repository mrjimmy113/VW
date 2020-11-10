using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileImpact : MonoBehaviour
{
    [SerializeField]
    protected ProjectileControl control;



    public abstract void OnImpact(Collider2D[] cols);
    

    
}

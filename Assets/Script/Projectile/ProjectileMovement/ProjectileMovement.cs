using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1f;
    protected ProjectileControl control;
    protected Transform trans;

    private void Awake()
    {
        trans = transform;
        
        
    }

    private void OnEnable()
    {
        control = GetComponent<ProjectileControl>();
    }

    public abstract void Setup();



    public abstract void Move();
}

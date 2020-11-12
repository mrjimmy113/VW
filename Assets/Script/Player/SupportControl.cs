using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportControl : MonoBehaviour
{
    private PlayerControl playerControl;
    [SerializeField]
    private Transform projectile;
    [SerializeField]
    private Transform impact;

    [SerializeField]
    private float distanceToPlayer = 1f;
    [SerializeField]
    private float speed = 2f;

    private void Awake()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,playerControl.transform.position) > distanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerControl.transform.position,
                Time.deltaTime * speed

                ) ;
        }
    }

    public void Summon()
    {

    }

    public void BeGone()
    {

    }
}

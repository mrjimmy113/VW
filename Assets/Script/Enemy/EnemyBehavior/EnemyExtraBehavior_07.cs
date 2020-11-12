using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_07 : EnemyExtraBehavior
{
    [SerializeField]
    private float explodeRange = 0.5f;

    public override void Setup()
    {
        
    }

    public override void OnDead(OnEnemyDeadParam param)
    {
        control.enabled = false;
        Explode();
        base.OnDead(param);
    }

    private void Explode()
    {
        Destroy(gameObject, 1f);
        control.model.gameObject.SetActive(false);
        control.deadAnim.gameObject.SetActive(true);
        Collider2D col = Physics2D.OverlapCircle(
            transform.position, explodeRange * transform.localScale.y,
            LayerConfig.PLAYER_LAYER
            );
        if(col != null)
        {
            
            col.GetComponent<PlayerControl>().Dead();
        }
        
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRange * transform.localScale.y);
    }
}

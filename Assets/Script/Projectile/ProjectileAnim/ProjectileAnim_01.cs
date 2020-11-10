using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileAnim_01 : MonoBehaviour
{
    public Transform model;

    private void Start()
    {
        model.DOLocalRotate(new Vector3(0, 0, 90), 0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}

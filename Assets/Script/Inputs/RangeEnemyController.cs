using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeEnemyController : EnemyController
{
    //[SerializeField][Range(0f, 100f)] private float followRange = 15f;
    //[SerializeField][Range(0f, 100f)] private float ShootRange = 10f;


    private int layerMaskTarget;

    protected override void Start()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
       
        Vector2 directionToTarget = DirectionToTarget();
        PrerformAttackAction(directionToTarget);
    }

    // 공격
    private void PrerformAttackAction(Vector2 directionToTarget)
    {
        CallTargetEvent(directionToTarget);
        CallMoveEvent(Vector2.zero);
        IsAttacking = true;
    }
}

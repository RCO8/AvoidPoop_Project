using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeEnemyController : EnemyController
{
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
        IsAttacking = true;
    }
}

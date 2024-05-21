
using UnityEngine;
using UnityEngine.UIElements;

public class RangeEnemyController : EnemyController
{
    private int layerMaskTarget;
    private bool IsMove = false;
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

    private void RandomMove(Vector2 directionToTarget)
    {
        //움직이는 것을 의미합니다.
        if (IsMove)
        {
            // 지금 게임 오브젝트에 대한 x,y축을 가져온다.
            float xnum = gameObject.transform.position.x;
            float ynum = gameObject.transform.position.y;

            float randomx = UnityEngine.Random.Range(-3.0f, 3.0f);
            float randomy = UnityEngine.Random.Range(-3.0f, 3.0f);

            Vector2 vector2 = new Vector2(xnum  + randomx, ynum + randomy);

            // 이게 맞을까나?
            CallMoveEvent(vector2);
            
            // 추가후에 
            IsMove = false;
        }

        // 아닐시 공격
        else
        {
            PrerformAttackAction(directionToTarget);
        }
    }
    // 공격
    private void PrerformAttackAction(Vector2 directionToTarget)
    {
        CallTargetEvent(directionToTarget);
        CallMoveEvent(Vector2.one);
        IsAttacking = true;
        IsMove = true;
    }
}

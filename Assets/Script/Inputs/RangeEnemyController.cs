
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeEnemyController : EnemyController
{
    private int layerMaskTarget;
    private bool IsMove = false;
    Vector2 drection = Vector2.zero;
    Vector2 directionToTarget;

    float time = 0;
    protected override void Start()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
        drection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        // 코루틴은 비활성화때에는 작동안한다.
        //StartCoroutine(IRandomMove());
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        time += Time.fixedDeltaTime;
        directionToTarget = DirectionToTarget();

        if (time >= stats.CurrentStat.attackSO.delay && IsMove == true)
        {
            RandomMove();
            time = 0;
        }
        else if (time < stats.CurrentStat.attackSO.delay && IsMove == false)
        {
            PrerformAttackAction(directionToTarget);
        }
    }


    // 코루틴은 비활성화 되면 작동을 안합니다.
    //IEnumerator IRandomMove()
    //{
    //    while(true)
    //         {
    //             RandomMove();
    // 
    //             yield return new WaitForSeconds(10f);
    //         }
    //    
    //픽스업데이트는 0초
    private void RandomMove()
    {
        //움직이는 것을 의미합니다.
        Vector2 TestVector = Vector2.one;
        TestVector = TestVector * drection;
        // 움직이게 구독하는 부분을
        CallMoveEvent(TestVector);
        CallTargetEvent(TestVector);
        // 추가후에 
        IsMove = true;
    }

    // 공격
    private void PrerformAttackAction(Vector2 directionToTarget)
    {

        CallTargetEvent(directionToTarget);
        CallMoveEvent(Vector2.zero);
        IsAttacking = true;
        IsMove = true;
    }
}

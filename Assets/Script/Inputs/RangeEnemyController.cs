
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeEnemyController : EnemyController
{
    private LayerMask layerMaskTarget;
    private bool IsMove = false;

    Vector2 directionToTarget;
    Vector2 drection;
    float time = 0;
    float movetime = 0;
    int count = 0;
    IEnumerator Cor;
    protected override void Start()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
        Cor = IRandomMove();
        StartCoroutine(Cor);
    }

    private void OnEnable()
    {
        if (Cor != null)
        {
            StartCoroutine(Cor);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        time += Time.fixedDeltaTime;

        directionToTarget = DirectionToTarget();

        if (IsMove == false)
        {
            PrerformAttackAction(directionToTarget);
            Debug.Log("FixedUpdate에 46줄");
        }

        //if (time >= stats.CurrentStat.attackSO.delay && IsMove == true)
        //{
        //    RandomMove();

        //    if (time >= 8f)
        //    {
        //        Debug.Log(time);
        //        IsMove = false;
        //        time = 0;
        //    }
        //}
        //else if (time < stats.CurrentStat.attackSO.delay && IsMove == false)
        //{
        //    PrerformAttackAction(directionToTarget);
        //}
    }


    // 코루틴은 비활성화 되면 작동을 안합니다.
    IEnumerator IRandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(stats.CurrentStat.attackSO.delay);
            
            IsMove = true;
            yield return new WaitForFixedUpdate();

            Debug.Log("아무거나");
            RandomMove();
            yield return new WaitForSeconds(3f);
            IsMove = false;
        }

    }
    //픽스업데이트는 0초
    private void RandomMove()
    {
        //움직이는 것을 의미합니다.
        while (true)
        {
            drection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized;
            if (drection != Vector2.zero)
            {
                break;
            }
        }
        Vector2 TestVector = Vector2.one;
        TestVector = TestVector * drection;
        // 움직이게 구독하는 부분을
        CallMoveEvent(TestVector);
        CallTargetEvent(TestVector);
    }

    // 공격
    private void PrerformAttackAction(Vector2 directionToTarget)
    {
        CallTargetEvent(directionToTarget);
        CallMoveEvent(Vector2.zero);
        IsAttacking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(layerMaskTarget.value, collision.gameObject.layer))
        {
            CharacterStatsHandler stats = collision.gameObject.GetComponent<CharacterStatsHandler>();

            if (!stats.canAttacked)
            {
                HealthSystem myHealthSystem = gameObject.GetComponent<HealthSystem>();

                myHealthSystem.ChangeHealth(-stats.CurrentStat.power);

                return;
            }

            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();

            if (null != healthSystem)
                healthSystem.ChangeHealth(-gameObject.GetComponent<CharacterStatsHandler>().CurrentStat.power);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}

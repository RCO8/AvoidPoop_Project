
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.XR.OpenVR;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Enemy : Character
{
    RangeEnemyController rangeEnemyController;

    [SerializeField] Transform targetPivot;
    [SerializeField] List<string> itemTags;

    private Vector2 aimDirection = Vector2.right;

    protected override void Awake()
    {
        base.Awake();
        rangeEnemyController = GetComponent<RangeEnemyController>();
    }


    protected override void Start()
    {
        base.Start();
        rangeEnemyController.OnMoveEvent += EnemyMove;
        rangeEnemyController.OnTargetEvent += ViewTarget;
        rangeEnemyController.OnTargetEvent += EnemyTaget;
        rangeEnemyController.OnShootEvent += Shooting;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void EnemyMove(Vector2 direction)
    {
        rgb2D.velocity = direction * statsHandler.CurrentStat.speed;
    }

    private void EnemyTaget(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //스프라이트에서 문제가 있어서 안쪽에서 90+더한 값으로 이동하게끔 만들었습니다.
        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    protected override void Shooting(AttackSO attackSO)
    {
        //AudioManager.Instance.PlayEffect(shootClip);

        // 디버그세팅
        // Debug.Log("Emeny 슈팅");
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;

        if (null == rangedAttackSO)
            return;

        float BulletsAngleSpace = rangedAttackSO.multipleBulletsAngle;
        int numberOfBulletsPerShot = rangedAttackSO.numberOfBulletsPerShot;

        float minAngle = (BulletsAngleSpace * 0.5f) - ((numberOfBulletsPerShot / 2f) * BulletsAngleSpace);

        for (int i = 0; i < numberOfBulletsPerShot; ++i)
        {
            float angle = minAngle + (i * BulletsAngleSpace);

            CreateBullet(rangedAttackSO, angle + rangedAttackSO.spread);
        }

    }
    private void CreateBullet(RangedAttackSO rangedAttackSO, float angle)
    {
        ObjectPool pool = GameManager.Instance.GetComponent<ObjectPool>();

        GameObject obj = pool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        obj.transform.position = targetPivot.position;

        BulletController bulletController = obj.GetComponent<BulletController>();

        bulletController.InitailizeAttack(RotateVector2(targetRotation, angle), rangedAttackSO);
    }
    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
    protected override void Dead()
    {
        // 체력이 0이하일때 게임 오브젝트 비활성화하고 오브젝트풀에서 없애는 것을 목표로 하면될 거 같습니다.
        if (healthSystem.CurrentHP <= 0)
        {
            //AudioManager.Instance.PlayEffect(deathClip);

            ObjectPool currentObjectPool = GameManager.Instance.GetComponent<ObjectPool>();

            int percent = Random.Range(0, 10);

            if (5 > percent)
            {
                // 아이템 생성
                GameObject item = currentObjectPool.SpawnFromPool(itemTags[Random.Range(0, 3)]);
                item.transform.position = transform.position;
            }

            // 그리고 비활성화
            //gameObject.SetActive(false);

            // 체력을 원상복구
            healthSystem.ReSetHp();

            //게임 매니저를 통해서 그 오브젝트 플이 있으면 다시 집어 넣는 다.
            currentObjectPool.RetrieveObject(gameObject.tag, gameObject);
        }
    }
}
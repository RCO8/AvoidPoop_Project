﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character
{
    //System.Numerics.Vector2 createPos = System.Numerics.Vector2.Zero;   //적이 랜덤으로 생성할 위치
    RangeEnemyController rangeEnemyController;
    [SerializeField] Transform targetPivot;
   
    private ObjectPool pool;

    protected override void Awake()
    {
        base.Awake();
        pool = GameManager.Instance.GetComponent<ObjectPool>();
        rangeEnemyController = GetComponent<RangeEnemyController>();
    }

    private void Start()
    {
        rangeEnemyController.OnShootEvent += Shooting;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // 나중에 플레이어 다시 맞춰야함 지금 플레이어 맞게 만드는 중..
    // 타켓이랑 샷은 스크립트를 뺐으면 좋았을텐데

    protected override void Shooting(AttackSO attackSO)
    {
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
        GameObject obj = pool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        obj.transform.position = targetPivot.position;

        BulletController bulletController = obj.GetComponent<BulletController>();
        bulletController.InitailizeAttack(RotateVector2(targetRotation, angle), rangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}
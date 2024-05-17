using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    PlayerController controller;
    [SerializeField] Transform targetPivot;

    // 속성 업그레이드 하면 일정 시간의 유효
    private float speedTime = 5f;
    private float powerTime = 5f;
        
    private ObjectPool pool;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();

        pool = GameManager.Instance.GetComponent<ObjectPool>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Moving;
        controller.OnTargetEvent += ViewTarget;
        controller.OnShootEvent += Shooting;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        PlayerMoving();
        PlayerTarget();
    }

    private void PlayerMoving() //플레이어만의 이동기능
    {
        rgb2D.velocity = characterMovement.normalized * statsHandler.CurrentStat.speed;
    }
    private void PlayerTarget() //마우스 방향 조준
    {
        float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        targetPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }


    // 이 메서드들은 업그레이드 하면 일정기간동안 적용되다가 원래로 리셋된다.
    private void PowerUp()
    {
        // 일정시간동안 Power를 2로
        statsHandler.CurrentStat.power = 2f;
    }
    private void SpeedUp()
    {
        //일정시간동안 Speed를 5로
        statsHandler.CurrentStat.speed = 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Dead();
        }

        //만약 아이템을 먹으면 종류에 따라 업그레이드 함수로 이동
    }

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

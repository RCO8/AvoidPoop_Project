using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    PlayerController controller;
    [SerializeField] Transform targetPivot;

    // �Ӽ� ���׷��̵� �ϸ� ���� �ð��� ��ȿ
    private float speedTime = 5f;
    private float powerTime = 5f;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask itemLayer;

    protected override void Awake()
    {
        base.Awake();
        
        controller = GetComponent<PlayerController>();
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

    private void PlayerMoving() //�÷��̾�� �̵����
    {
        rgb2D.velocity = characterMovement.normalized * statsHandler.CurrentStat.speed;
    }
    private void PlayerTarget() 
    {
        //���콺 ���� ����
        //float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        //targetPivot.rotation = Quaternion.Euler(0, 0, rotZ);

        // TODO : �ϸ��� ȸ���� ������
        float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        Quaternion from = transform.localRotation;
        Quaternion to = Quaternion.Euler(0, 0, rotZ);
        transform.localRotation = Quaternion.Slerp(from, to, 1f);

    }


    // �� �޼������ ���׷��̵� �ϸ� �����Ⱓ���� ����Ǵٰ� ������ ���µȴ�.
    private void PowerUp()
    {
        // �����ð����� Power�� 2��
        statsHandler.CurrentStat.power = 2f;
    }
    private void SpeedUp()
    {
        //�����ð����� Speed�� 5��
        statsHandler.CurrentStat.speed = 5f;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 8) //EnemyBullet
        {
            Debug.Log("�÷��̾� ����");
            Dead();
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
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
}

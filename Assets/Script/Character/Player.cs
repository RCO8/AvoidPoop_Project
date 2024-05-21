using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    PlayerController controller;
    [SerializeField] Transform targetPivot;

    [SerializeField] private GameObject LeftBoost;
    [SerializeField] private GameObject RightBoost;

    // �Ӽ� ���׷��̵� �ϸ� ���� �ð��� ��ȿ
    private float speedTime = 5f;
    private float powerTime = 5f;

    protected override void Awake()
    {
        base.Awake();
        
        controller = GetComponent<PlayerController>();
    }

    protected override void Start()
    {
        base.Start();

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
        if (Vector2.zero == characterMovement)
        {
            LeftBoost.SetActive(false);
            RightBoost.SetActive(false);
        }
        else
        {
            LeftBoost.SetActive(true);
            RightBoost.SetActive(true);
        }

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Dead();
    //    }

    //    //���� �������� ������ ������ ���� ���׷��̵� �Լ��� �̵�
    //}

    protected override void Shooting(AttackSO attackSO)
    {
        //AudioManager.Instance.PlayEffect(shootClip);

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
        //AudioManager.Instance.PlayEffect(deathClip);

        StartCoroutine(OnDead());
    }

    IEnumerator OnDead()
    {
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.EndGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    PlayerController controller;
    [SerializeField] Transform targetPivot;

    [SerializeField] private GameObject LeftBoost;
    [SerializeField] private GameObject RightBoost;

    private float buffTime = 15f;
    private float countdownTime = 0f;

    PlayerAnimationController animationController;

    protected override void Awake()
    {
        base.Awake();
        
        controller = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    protected override void Start()
    {
        base.Start();

        controller.OnMoveEvent += Moving;
        controller.OnTargetEvent += ViewTarget;
        controller.OnShootEvent += Shooting;

        countdownTime = 0f;
}

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        PlayerMoving();
        PlayerTarget();
    }

    private void Update()
    {
        if (0f < countdownTime)
            countdownTime -= Time.deltaTime;
    }

    private void PlayerMoving()
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
        //float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        //targetPivot.rotation = Quaternion.Euler(0, 0, rotZ);

        float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        Quaternion from = transform.localRotation;
        Quaternion to = Quaternion.Euler(0, 0, rotZ);
        transform.localRotation = Quaternion.Slerp(from, to, 1f);
    }

    public void RootItem(ItemSO itemData)
    {
        StartCoroutine(StatsUPCor(itemData));
    }

    private IEnumerator StatsUPCor(ItemSO itemData)
    {
        switch (itemData.type)
        {
            case ItemType.SPEEDUP:
                statsHandler.CurrentStat.speed += (int)itemData.increase;
                break;
            case ItemType.POWERUP:
                statsHandler.currentBulletsPerShot += (int)itemData.increase;
                break;
            case ItemType.Invincibillity:
                OnInvincibillity();
                break;
        }

        GameManager.Instance.SetPlayerUI(statsHandler);
        GameManager.Instance.TurnItem(itemData.type, true);

        yield return new WaitForSeconds(buffTime);

        switch (itemData.type)
        {
            case ItemType.SPEEDUP:
                statsHandler.CurrentStat.speed -= (int)itemData.increase;
                break;
            case ItemType.POWERUP:
                statsHandler.currentBulletsPerShot -= (int)itemData.increase;
                break;
            case ItemType.Invincibillity:
                OffInvincibillity();
                break;
        }

        GameManager.Instance.SetPlayerUI(statsHandler);
        GameManager.Instance.TurnItem(itemData.type, false);
    }

    private void OnInvincibillity()
    {
        if (statsHandler.canAttacked)
        {
            statsHandler.canAttacked = false;
            animationController.OnInvincibillity();
            GameManager.Instance.TurnInvincibillity(true);
        }

        countdownTime += buffTime;
    }

    private void OffInvincibillity()
    {
        if (0f >= countdownTime)
        {
            statsHandler.canAttacked = true;
            animationController.OffInvincibillity();
            GameManager.Instance.TurnInvincibillity(false);
            countdownTime = 0f;
        }
    }

    protected override void Shooting(AttackSO attackSO)
    {
        //AudioManager.Instance.PlayEffect(shootClip);

        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;

        if (null == rangedAttackSO)
            return;

        float BulletsAngleSpace = rangedAttackSO.multipleBulletsAngle;
        int numberOfBulletsPerShot = statsHandler.currentBulletsPerShot;

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
        yield return new WaitForSeconds(1f);

        GameManager.Instance.EndGame();
    }
}

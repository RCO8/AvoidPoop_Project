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

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Moving;
        controller.OnTargetEvent += ViewTarget;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        PlayerMoving();
        PlayerTarget();
    }

    private void PlayerMoving() //�÷��̾�� �̵����
    {
        rgb2D.velocity = characterMovement.normalized * Speed;
    }
    private void PlayerTarget()
    {
        float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        targetPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }


    // �� �޼������ ���׷��̵� �ϸ� �����Ⱓ���� ����Ǵٰ� ������ ���µȴ�.
    private void PowerUp()
    {
        // �����ð����� Power�� 2��
        Power = 2f;
    }
    private void SpeedUp()
    {
        //�����ð����� Speed�� 5��
        Speed = 5f;
    }
}

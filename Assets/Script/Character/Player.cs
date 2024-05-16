using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    PlayerController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Moving;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        PlayerMoving();
    }

    private void PlayerMoving() //�÷��̾�� �̵����
    {
        rgb2D.velocity = characterMovement.normalized * Speed;
    }

    // �� �޼������ ���׷��̵� �ϸ� �����Ⱓ���� ����Ǵٰ� ������ ���µȴ�.
    void PowerUp()
    {

    }
    void SpeedUp()
    {
        //���ǵ��
        Speed = 5f;
    }
}

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

    private void PlayerMoving() //플레이어만의 이동기능
    {
        rgb2D.velocity = characterMovement.normalized * Speed;
    }

    // 이 메서드들은 업그레이드 하면 일정기간동안 적용되다가 원래로 리셋된다.
    void PowerUp()
    {

    }
    void SpeedUp()
    {
        //스피드업
        Speed = 5f;
    }
}

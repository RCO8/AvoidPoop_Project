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

    private void PlayerMoving() //플레이어만의 이동기능
    {
        rgb2D.velocity = characterMovement.normalized * Speed;
    }
    private void PlayerTarget()
    {
        float rotZ = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        targetPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }


    // 이 메서드들은 업그레이드 하면 일정기간동안 적용되다가 원래로 리셋된다.
    private void PowerUp()
    {
        // 일정시간동안 Power를 2로
        Power = 2f;
    }
    private void SpeedUp()
    {
        //일정시간동안 Speed를 5로
        Speed = 5f;
    }
}

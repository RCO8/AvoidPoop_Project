using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : InputController
{
    [SerializeField] Camera cam;
    public void OnMove(InputValue value)
    {
        CallMoveEvent(value.Get<Vector2>().normalized);
    }

    public void OnTarget(InputValue value)
    {
        //마우스로 조작한 타겟
        Vector2 targetPos = value.Get<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(targetPos);
        targetPos = (worldPos - (Vector2)transform.position).normalized;
        //CallTargetEvent(targetPos);

    }

    public void OnTargetKey(InputValue value)   //키보드 조준 (I,J,K,L)
    {
        //2인용 컨트롤러 문제로 키보드 방향키대로 바라보게 해보기
        // 예를 들어 방향키를 아래 오른쪽으로 누르면 그 방향대로 쏘게 하기
        Vector2 targetPos = value.Get<Vector2>().normalized;
        if(targetPos.x != 0 || targetPos.y != 0)
            CallTargetEvent(targetPos);
    }

    public void OnShoot(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}

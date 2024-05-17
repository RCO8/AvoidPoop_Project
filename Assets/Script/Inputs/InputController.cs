using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnTargetEvent; //마우스 좌표
    public event Action<AttackSO> OnShootEvent;

    protected bool IsAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;

    private void Update()
    {
        
    }

    private void HandleAttackDelay()
    {
        if (true == IsAttacking)
            CallShootEvent();
    }

    //입력 시스템 Invoke
    public void CallMoveEvent(Vector2 move)
    {
        OnMoveEvent?.Invoke(move);
    }

    public void CallTargetEvent(Vector2 target)
    {
        OnTargetEvent?.Invoke(target);
    }

    public void CallShootEvent(AttackSO attackSO)
    {
        OnShootEvent?.Invoke(attackSO);
    }
}

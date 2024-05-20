using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnTargetEvent; //���콺 ��ǥ
    public event Action<AttackSO> OnShootEvent;

    protected bool IsAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;

    protected CharacterStatsHandler stats { get; private set; }

    private void Awake()
    {
        stats = GetComponent<CharacterStatsHandler>();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (stats.CurrentStat.attackSO.delay > timeSinceLastAttack)
            timeSinceLastAttack += Time.deltaTime;
        else if ((true == IsAttacking) && (stats.CurrentStat.attackSO.delay <= timeSinceLastAttack))
        {
            timeSinceLastAttack = 0f;
            CallShootEvent(stats.CurrentStat.attackSO);
        }
    }

    //�Է� �ý��� Invoke
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

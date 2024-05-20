using UnityEngine;

public class WindowOutEnemyController : InputController
{
    private Transform ClosestTarget { get; set; }

    private void Start()
    {
        ClosestTarget = GameManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (ClosestTarget.position - transform.position).normalized;

        CallTargetEvent(direction);
        IsAttacking = true;
    }
}
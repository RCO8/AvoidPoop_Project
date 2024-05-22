using UnityEngine;

public class PlayerBulletController : BulletController
{
    private TrailRenderer trailRenderer;

    protected override void Awake()
    {
        base.Awake();

        //trailRenderer = GetComponent<TrailRenderer>();
    }

    public override void InitailizeAttack(Vector2 direction, AttackSO attackData)
    {
        base.InitailizeAttack(direction, attackData);

        //trailRenderer.Clear();
    }
}

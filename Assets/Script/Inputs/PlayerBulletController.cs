using UnityEngine;

public class PlayerBulletController : BulletController
{

    protected override void Awake()
    {
        base.Awake();

      
    }

    public override void InitailizeAttack(Vector2 direction, AttackSO attackData)
    {
        base.InitailizeAttack(direction, attackData);

  
    }
}

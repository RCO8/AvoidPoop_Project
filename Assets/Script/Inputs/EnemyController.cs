using UnityEngine;



public partial class EnemyController : InputController
{

    protected Transform ClosestTarget { get; private set; }
    protected override void Awake()
    {
        base.Awake();

    }

    protected virtual void Start()
    {
        ClosestTarget = GameManager.Instance.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected Vector3 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }

    public override void DestroyObject()
    {

    }
}

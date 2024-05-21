using UnityEngine;

public class DodgeShooting : MonoBehaviour
{
    private InputController controller;

    protected Vector2 aimDirection = Vector2.right;

    private void Awake()
    {
        controller = GetComponent<InputController>();
    }

    private void Start()
    {
        controller.OnShootEvent += OnShoot;
        controller.OnTargetEvent += OnAim;
    }

    protected virtual void OnShoot(AttackSO attackSO)
    {
        GameObject obj = GameManager.Instance.CurrentObjectPool.SpawnFromPool(attackSO.bulletNameTag);

        obj.transform.position = transform.position;

        BulletController attackController = obj.GetComponent<BulletController>();
        attackController.InitailizeAttack(aimDirection, attackSO);

        ++GameManager.Instance.BulletCount;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }
}

using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private AttackSO attackData;
    private Vector2 direction;

    private bool isReady = false;
    private bool fxOnDestroy = true;

    private ObjectPool obPool;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        obPool = GameManager.Instance.GetComponent<ObjectPool>();
    }

    private void FixedUpdate()
    {
        if (!isReady)
            return;

        rigidBody.velocity = direction * attackData.speed;
    }

    public void InitailizeAttack(Vector2 direction, AttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        UpdateBulletSprite();

        trailRenderer.Clear();

        transform.right = this.direction;

        isReady = true;
    }

    private void DestroyBullet(bool createFX)
    {
        if (createFX)
        {

        }

        gameObject.SetActive(false);

        obPool.RetrieveObject(attackData.bulletNameTag, this.gameObject);
    }

    private void UpdateBulletSprite()
    {
        transform.localScale = Vector2.one * attackData.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            // 데미지
            DestroyBullet(fxOnDestroy);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}

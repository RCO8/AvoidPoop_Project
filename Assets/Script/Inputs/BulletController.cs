using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    protected AttackSO attackData;
    protected Vector2 direction;

    protected bool isReady = false;
    protected bool fxOnDestroy = true;

    private bool isInWindow = false;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isReady)
            return;

        rigidBody.velocity = direction * attackData.speed;
    }

    public virtual void InitailizeAttack(Vector2 direction, AttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        UpdateBulletSprite();

        transform.right = this.direction;

        spriteRenderer.color = attackData.bulletColor;

        isReady = true;
    }

    private void DestroyBullet(bool createFX)
    {
        if (createFX)
        {

        }

        gameObject.SetActive(false);
        isInWindow = false;

        GameManager.Instance.GetComponent<ObjectPool>().RetrieveObject(attackData.bulletNameTag, this.gameObject);
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

    private void OnBecameVisible()
    {
        isInWindow = true;
    }

    private void OnBecameInvisible()
    {
        if (isInWindow)
            StartCoroutine(DestroyAfterWait());
    }

    IEnumerator DestroyAfterWait()
    {
        yield return new WaitForSeconds(1f);

        DestroyBullet(false);
    }
}

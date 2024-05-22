using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public enum ItemType
{
    POWERUP,
    SPEEDUP,
    Invincibillity
}

public class Item : MonoBehaviour
{
    public float speed;
    
    private Rigidbody2D rigidbody;
    public ItemSO itemSO;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        LandomSpawn();
        Launch();
    }

    private void LandomSpawn()
    {
        float x = Random.Range(-9f, 9f);
        float y = Random.Range(-5f, 5f);
        transform.position = new Vector3(x, y, 0);
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody.velocity = new Vector2(x * speed, y * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision) // 아이템이 플레이어에 충돌 시 삭제
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            Destroy(gameObject);
        }
    }
}

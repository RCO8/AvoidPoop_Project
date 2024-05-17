using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public enum ItemType
{
    POWERUP,
    SPEEDUP
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType type;

    public float speed;
    public Rigidbody2D rigidbody;

    public float increase { get; }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody.velocity = new Vector2(x * speed, y * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public enum ItemType
{
    POWERUP,
    SPEEDUP
}

[System.Serializable]

public class Item : MonoBehaviour
{
    public float speed;
    public float increase { get; }
    
    public Rigidbody2D rigidbody;

    public ItemType Type;

    public ItemSO itemSO;

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

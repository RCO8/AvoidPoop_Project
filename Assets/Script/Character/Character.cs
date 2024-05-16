using UnityEngine;

public class Character : MonoBehaviour
{
    int speed;
    int power;
    Bullet bullet;  //오브젝트 풀링 필요

    protected Rigidbody2D rgb2D;

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }

    void Shooting()
    {

    }
    void Moving()
    {
        //이동방향 * speed;
    }
    void Dead()
    {

    }
}

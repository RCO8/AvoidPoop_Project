using UnityEngine;

public class Character : MonoBehaviour
{
    int speed;
    int power;
    Bullet bullet;  //������Ʈ Ǯ�� �ʿ�

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
        //�̵����� * speed;
    }
    void Dead()
    {

    }
}

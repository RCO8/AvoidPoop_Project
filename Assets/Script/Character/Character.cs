using UnityEngine;

public class Character : MonoBehaviour
{
    protected float Speed { get; set; }
    int power;
    Bullet bullet;  //������Ʈ Ǯ�� �ʿ�

    protected Rigidbody2D rgb2D;
    protected Vector2 characterMovement;

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();

        //�⺻ �Ӽ��� ����
        Speed = 3f;
    }

    protected virtual void FixedUpdate()
    {
        Moving(characterMovement);
    }

    void Shooting()
    {

    }
    protected void Moving(Vector2 move)
    {
        //����Ŭ������ move���� charcterMovement�� ��⸸ �ϰ�
        //���� �̵��� ���� Ŭ��������.
        //�̵����� * speed;
        characterMovement = move;
    }
    void Dead()
    {

    }
}

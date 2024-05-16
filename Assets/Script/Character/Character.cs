using UnityEngine;

public class Character : MonoBehaviour
{
    protected float Speed { get; set; }
    int power;
    Bullet bullet;  //오브젝트 풀링 필요

    protected Rigidbody2D rgb2D;
    protected Vector2 characterMovement;

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();

        //기본 속성값 설정
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
        //상위클래스는 move값을 charcterMovement에 담기만 하고
        //실제 이동은 하위 클래스에서.
        //이동방향 * speed;
        characterMovement = move;
    }
    void Dead()
    {

    }
}

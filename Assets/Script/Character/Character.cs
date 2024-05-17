using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    //기본 필드 : 속도나 파워가 바뀔때 필요
    protected float Speed { get; set; } = 3f;
    protected float Power { get; set; } = 1f;

    [SerializeField] protected GameObject bullet;  //오브젝트 풀링 필요

    protected Rigidbody2D rgb2D;
    protected Vector2 characterMovement;
    protected Vector2 targetRotation;
    public bool isDead { get; protected set; }

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();

    }

    protected virtual void FixedUpdate()
    {
        Moving(characterMovement);
        ViewTarget(targetRotation);
    }

    void Shooting()
    {
        //발사하는 총알 생성 (오브젝트 풀링)
    }
    protected void Moving(Vector2 move)
    {
        //상위클래스는 move값을 charcterMovement에 담기만 하고
        //실제 이동은 하위 클래스에서.
        //이동방향 * speed;
        characterMovement = move;
    }
    protected void ViewTarget(Vector2 target)
    {
        targetRotation = target;
    }


    public virtual void Dead()
    {

    }


}

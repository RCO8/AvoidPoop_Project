using UnityEngine;

public class Character : MonoBehaviour
{
    //�⺻ �ʵ� : �ӵ��� �Ŀ��� �ٲ� �ʿ�
    protected float Speed { get; set; } = 3f;
    protected float Power { get; set; } = 1f;



    protected Rigidbody2D rgb2D;
    protected Vector2 characterMovement;
    protected Vector2 targetRotation;

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();

    }

    protected virtual void FixedUpdate()
    {
        Moving(characterMovement);
        ViewTarget(targetRotation);
    }

    protected void Shooting(bool isShot)
    {
        //�߻��ϴ� �Ѿ� ���� (������Ʈ Ǯ��)
        //if(isShot) �Ѿ˹߻�
    }
    protected void Moving(Vector2 move)
    {
        //����Ŭ������ move���� charcterMovement�� ��⸸ �ϰ�
        //���� �̵��� ���� Ŭ��������.
        //�̵����� * speed;
        characterMovement = move;
    }
    protected void ViewTarget(Vector2 target)
    {
        targetRotation = target;
    }


    protected virtual void Dead()
    {
        //���������� �ı��� �ϸ� ���� �� ������
        //�������̷�?
    }
}

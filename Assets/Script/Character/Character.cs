using UnityEngine;

public class Character : MonoBehaviour
{
    protected Rigidbody2D rgb2D;
    protected Vector2 characterMovement;
    protected Vector2 targetRotation;

    protected CharacterStatsHandler statsHandler;

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        statsHandler = GetComponent<CharacterStatsHandler>();
    }

    protected virtual void FixedUpdate()
    {
        Moving(characterMovement);
        ViewTarget(targetRotation);
    }

    protected virtual void Shooting(AttackSO attackSO)
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

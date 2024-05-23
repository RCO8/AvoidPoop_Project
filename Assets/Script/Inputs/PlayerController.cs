using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : InputController
{
    [SerializeField] Camera cam;
    public void OnMove(InputValue value)
    {
        CallMoveEvent(value.Get<Vector2>().normalized);
    }

    public void OnTarget(InputValue value)
    {
        //���콺�� ������ Ÿ��
        Vector2 targetPos = value.Get<Vector2>();
        Vector2 worldPos = cam.ScreenToWorldPoint(targetPos);
        targetPos = (worldPos - (Vector2)transform.position).normalized;
        //CallTargetEvent(targetPos);

    }

    public void OnTargetKey(InputValue value)   //Ű���� ���� (I,J,K,L)
    {
        //2�ο� ��Ʈ�ѷ� ������ Ű���� ����Ű��� �ٶ󺸰� �غ���
        // ���� ��� ����Ű�� �Ʒ� ���������� ������ �� ������ ��� �ϱ�
        Vector2 targetPos = value.Get<Vector2>().normalized;
        if(targetPos.x != 0 || targetPos.y != 0)
            CallTargetEvent(targetPos);
    }

    public void OnShoot(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}

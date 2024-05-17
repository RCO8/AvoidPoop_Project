using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnTargetEvent; //���콺 ��ǥ
    public event Action<bool> OnShootEvent;

    //�Է� �ý��� Invoke
    public void CallMoveEvent(Vector2 move)
    {
        OnMoveEvent?.Invoke(move);
    }

    public void CallTargetEvent(Vector2 target)
    {
        OnTargetEvent?.Invoke(target);
    }

    public void CallShootEvent(bool isShoot)
    {
        OnShootEvent?.Invoke(isShoot);
    }
}

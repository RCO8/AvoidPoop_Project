using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : InputController
{


    public void OnMove(InputValue value)
    {
        CallMoveEvent(value.Get<Vector2>());
    }

    public void OnTarget(InputValue value)
    {
        CallTargetEvent(value.Get<Vector2>());
    }
}

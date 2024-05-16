using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    protected override void Awake()
    {
        base.Awake();
    }

    // 이 메서드들은 업그레이드 하면 일정기간동안 적용되다가 원래로 리셋된다.
    void PowerUp()
    {

    }
    void SpeedUp()
    {

    }
}

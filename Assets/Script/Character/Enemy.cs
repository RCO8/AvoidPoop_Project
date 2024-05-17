using System.Numerics;

public class Enemy : Character
{
    Vector2 createPos = Vector2.Zero;   //적이 랜덤으로 생성할 위치

    protected override void Awake()
    {
        base.Awake();
    }

    void RandomCreate()
    {

    }
}
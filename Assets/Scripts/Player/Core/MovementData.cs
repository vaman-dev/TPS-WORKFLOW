using UnityEngine;

public struct MovementData
{
    public float moveX;
    public float moveY;
    public float speed;

    public MovementData(float x, float y, float s)
    {
        moveX = x;
        moveY = y;
        speed = s;
    }
}

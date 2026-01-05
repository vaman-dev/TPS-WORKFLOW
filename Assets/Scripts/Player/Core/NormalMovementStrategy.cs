using UnityEngine;

public class NormalMovementStrategy : IMovementStrategy
{
    // Normalized blend values for Animator
    private readonly float walkSpeed = 0.5f;
    private readonly float runSpeed = 1.0f;

    public MovementData CalculateMovement(Vector2 moveInput, bool isRunning)
    {
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        float speed = 0f;

        if (moveInput.sqrMagnitude > 0.01f)
        {
            speed = isRunning ? runSpeed : walkSpeed;
        }

        return new MovementData(moveX, moveY, speed);
    }
}

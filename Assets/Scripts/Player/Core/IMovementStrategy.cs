using UnityEngine;
public interface IMovementStrategy
{
    MovementData CalculateMovement(Vector2 moveInput, bool isRunning);
}
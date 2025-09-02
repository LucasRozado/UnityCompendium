using UnityEngine;

public class TransformMovementSystem : MovementSystem.RunsOnUpdate
{
    protected override void Move(Vector3 velocity)
    { transform.position += velocity; }
}

using UnityEngine;

public class TransformMovementSubject : MovementSubject
{
    private Vector3 velocity;


    public override Vector3 Velocity => velocity;
    protected override void SetVelocity(Vector3 velocity)
    { this.velocity = velocity; }


    protected override void Move(Vector3 velocity)
    { transform.position += velocity; }
}

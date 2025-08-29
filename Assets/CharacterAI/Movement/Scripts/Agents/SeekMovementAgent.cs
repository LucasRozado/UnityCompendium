using UnityEngine;

public class SeekMovementAgent : MovementAgent
{
    public override Vector3 GetNextVelocity(MovementSubject subject, Vector3 targetPosition, float deltaTime)
    {
        Vector3 targetOffset = targetPosition - subject.Position;
        Vector3 targetDirection = targetOffset.normalized;
        Vector3 desiredVelocity = targetDirection * subject.MaximumSpeed;

        Vector3 velocity = subject.Velocity;

        Vector3 desiredSteering = desiredVelocity - velocity;
        Vector3 steering = Vector3.ClampMagnitude(desiredSteering, subject.MaximumAcceleration * deltaTime);

        Vector3 acceleration = steering / subject.Mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, subject.MaximumSpeed);

        return velocity;
    }
}

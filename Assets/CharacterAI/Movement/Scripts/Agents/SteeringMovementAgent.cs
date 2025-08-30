using UnityEngine;

public abstract class SteeringMovementAgent : MovementAgent
{
    public abstract Vector3 CalculateVelocityTarget(MovementSubject subject, Vector3 positionTarget);

    public sealed override Vector3 CalculateNextVelocity(MovementSubject subject, Vector3 positionTarget, float deltaTime)
    {
        Vector3 velocityTarget = CalculateVelocityTarget(subject, positionTarget);

        Vector3 velocity = subject.Velocity;

        Vector3 steeringTarget = velocityTarget - velocity;
        Vector3 steering = Vector3.ClampMagnitude(steeringTarget, subject.MaximumAcceleration * deltaTime);

        Vector3 acceleration = steering / subject.Mass;

        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, subject.MaximumSpeed);

        return velocity;
    }
}

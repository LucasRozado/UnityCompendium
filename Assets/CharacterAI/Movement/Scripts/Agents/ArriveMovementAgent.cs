using UnityEngine;

public class ArriveMovementAgent : MovementAgent
{
    public override Vector3 GetNextVelocity(MovementSubject subject, Vector3 targetPosition, float deltaTime)
    {
        Vector3 velocity = subject.Velocity;

        Vector3 targetOffset = targetPosition - subject.Position;
        float targetDistance = targetOffset.magnitude;
        float rampedSpeed = subject.MaximumSpeed * (targetDistance / subject.ArrivalDistance);
        float clippedSpeed = Mathf.Min(rampedSpeed, subject.MaximumSpeed);
        Vector3 desiredVelocity = (clippedSpeed / targetDistance) * targetOffset;

        Vector3 steerVelocity = desiredVelocity - velocity;
        steerVelocity = Vector3.ClampMagnitude(steerVelocity, subject.MaximumAcceleration * deltaTime);

        Vector3 acceleration = steerVelocity / subject.Mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, subject.MaximumSpeed);

        return velocity;
    }
}

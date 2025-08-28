using UnityEngine;

public class ArriveMovementAgent : MovementAgent
{
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float maximumAcceleration = 5f;
    [SerializeField] private float arrivalRadius = 5f;

    [SerializeField] private Transform target;

    public override Vector3 GetNextVelocity(MovementSubject subject, float deltaTime)
    {
        Vector3 velocity = subject.Velocity;

        Vector3 targetOffset = target.position - transform.position;
        float targetDistance = targetOffset.magnitude;
        float rampedSpeed = maximumSpeed * (targetDistance / arrivalRadius);
        float clippedSpeed = Mathf.Min(rampedSpeed, maximumSpeed);
        Vector3 desiredVelocity = (clippedSpeed / targetDistance) * targetOffset;

        Vector3 steerVelocity = desiredVelocity - velocity;
        steerVelocity = Vector3.ClampMagnitude(steerVelocity, maximumAcceleration * deltaTime);

        Vector3 acceleration = steerVelocity / subject.Mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

        return velocity;
    }
}

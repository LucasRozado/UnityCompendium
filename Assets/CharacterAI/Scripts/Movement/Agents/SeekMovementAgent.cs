using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
public class SeekMovementAgent : MovementAgent
{
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float maximumAcceleration = .1f;

    [SerializeField] private Transform target;

    public override Vector3 GetNextVelocity(MovementSubject subject)
    {
        Vector3 velocity = subject.Velocity;

        Vector3 targetOffset = target.position - transform.position;
        Vector3 targetDirection = targetOffset.normalized;
        Vector3 desiredVelocity = targetDirection * maximumSpeed;

        Vector3 steerVelocity = desiredVelocity - velocity;
        steerVelocity = Vector3.ClampMagnitude(steerVelocity, maximumAcceleration);

        Vector3 acceleration = steerVelocity / subject.Mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

        return velocity;
    }
}

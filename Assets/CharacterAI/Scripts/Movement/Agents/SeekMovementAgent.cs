using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
public class SeekMovementAgent : MovementAgent
{
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float maximumAcceleration = 5f;

    [SerializeField] private Transform target;

    public override Vector3 GetNextVelocity(MovementSubject subject, float deltaTime)
    {
        Vector3 targetOffset = target.position - transform.position;
        Vector3 targetDirection = targetOffset.normalized;
        Vector3 desiredVelocity = targetDirection * maximumSpeed;

        Vector3 velocity = subject.Velocity;
        
        Vector3 desiredSteering = desiredVelocity - velocity;
        Vector3 steering = Vector3.ClampMagnitude(desiredSteering, maximumAcceleration * deltaTime);

        Vector3 acceleration = steering / subject.Mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

        return velocity;
    }
}

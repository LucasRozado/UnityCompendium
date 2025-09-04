using UnityEngine;

public class SteeringMovementAgent : MovementAgent
{
    private readonly ComponentSet<SteeringBehaviour> behaviours = new();

    private void Start()
    {
        behaviours.Initialize(gameObject);
    }

    public override Vector3 CalculateNextVelocity(MovementSubject subject, float deltaTime)
    {
        Vector3 velocityTarget = Vector3.zero;
        foreach (SteeringBehaviour behaviour in behaviours.Active)
        { velocityTarget += behaviour.CalculateVelocityTarget(subject); }

        Vector3 velocity = subject.Velocity;

        Vector3 steeringTarget = velocityTarget - velocity;
        Vector3 steering = Vector3.ClampMagnitude(steeringTarget, subject.MaximumAcceleration * deltaTime);

        Vector3 acceleration = steering / subject.Mass;

        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, subject.MaximumSpeed);

        return velocity;
    }
}
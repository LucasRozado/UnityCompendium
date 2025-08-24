using UnityEngine;

public class ArriveMovementActor : MovementActor.Using<ControllerColliderHit>
{
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float maximumAcceleration = .1f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float arrivalRadius = 5f;

    [SerializeField] private Transform target;

    private Vector3 velocity = Vector3.zero;

    public override void Move(MovementAgent.Using<ControllerColliderHit> agent)
    {
        Vector3 targetOffset = target.position - transform.position;
        float targetDistance = targetOffset.magnitude;
        float rampedSpeed = maximumSpeed * (targetDistance / arrivalRadius);
        float clippedSpeed = Mathf.Min(rampedSpeed, maximumSpeed);
        Vector3 desiredVelocity = (clippedSpeed / targetDistance) * targetOffset;

        Vector3 steerVelocity = desiredVelocity - velocity;
        steerVelocity = Vector3.ClampMagnitude(steerVelocity, maximumAcceleration);

        Vector3 acceleration = steerVelocity / mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

        agent.Move(velocity);
        transform.rotation = Quaternion.LookRotation(velocity);
    }
}

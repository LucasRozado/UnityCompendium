using UnityEngine;

public class SeekMovementActor : MovementActor.Using<ControllerColliderHit>
{
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float maximumSteerForce = .1f;
    [SerializeField] private float mass = 1f;

    [SerializeField] private Transform target;

    private Vector3 velocity = Vector3.zero;

    public override void Move(MovementAgent.Using<ControllerColliderHit> agent)
    {
        Vector3 targetOffset = target.position - transform.position;
        Vector3 targetDirection = targetOffset.normalized;
        Vector3 desiredVelocity = targetDirection * maximumSpeed;

        Vector3 steerVelocity = desiredVelocity - velocity;
        steerVelocity = Vector3.ClampMagnitude(steerVelocity, maximumSteerForce);

        Vector3 acceleration = steerVelocity / mass;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

        agent.Move(velocity);
        transform.rotation = Quaternion.LookRotation(velocity);
    }
}

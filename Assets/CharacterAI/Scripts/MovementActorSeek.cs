using UnityEngine;

public class MovementActorSeek : MovementActor.Using<ControllerColliderHit>
{
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float maximumForce = 3f;
    [SerializeField] private float mass = 1f;

    [SerializeField] private Transform target;

    private Vector3 velocity = Vector3.zero;

    public override void Move(MovementAgent.Using<ControllerColliderHit> agent)
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 desiredVelocity = targetDirection.normalized * maximumSpeed;
        Vector3 steerVelocity = desiredVelocity - velocity;

        float steerSpeed = steerVelocity.magnitude;
        Vector3 steerDirection = steerVelocity / steerSpeed;
        steerVelocity = steerDirection * Mathf.Min(steerVelocity.magnitude, maximumForce);

        Vector3 acceleration = steerVelocity / mass;
        velocity += acceleration;

        float speed = velocity.magnitude;
        Vector3 direction = velocity / speed;
        velocity = direction * Mathf.Min(velocity.magnitude, maximumSpeed);

        agent.Move(velocity);
        transform.rotation = Quaternion.LookRotation(velocity);
    }
}

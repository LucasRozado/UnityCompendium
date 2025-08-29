using UnityEngine;

public class SimpleMovementAgent : MovementAgent
{
    public override Vector3 GetNextVelocity(MovementSubject subject, Vector3 targetPosition, float deltaTime)
    {
        Vector3 offset = targetPosition - subject.transform.position;
        Vector3 direction = offset.normalized;
        Vector3 velocity = direction * subject.MaximumSpeed;

        if (velocity.magnitude * deltaTime > offset.magnitude)
        { return offset / deltaTime; }
        else
        { return velocity; }
    }
}

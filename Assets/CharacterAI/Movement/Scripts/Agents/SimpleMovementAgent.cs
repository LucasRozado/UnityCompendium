using UnityEngine;

public class SimpleMovementAgent : MovementAgent
{
    public override Vector3 CalculateNextVelocity(MovementSubject subject, float deltaTime)
    {
        Vector3 offset = subject.Position - subject.transform.position;
        Vector3 direction = offset.normalized;
        Vector3 velocity = direction * subject.MaximumSpeed;

        if (velocity.magnitude * deltaTime > offset.magnitude)
        { return offset / deltaTime; }
        else
        { return velocity; }
    }
}

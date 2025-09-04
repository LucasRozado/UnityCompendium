using UnityEngine;

public class WaypointMovementAgent : MovementAgent
{
    [SerializeField] private Waypoint currentTarget;

    public override Vector3 CalculateNextVelocity(MovementSubject subject, float deltaTime)
    {
        Vector3 offset = currentTarget.transform.position - subject.transform.position;
        Vector3 direction = offset.normalized;
        Vector3 velocity = direction * subject.MaximumSpeed;

        if (velocity.magnitude * deltaTime > offset.magnitude)
        {
            currentTarget = currentTarget.Next;
            return offset / deltaTime;
        }
        else
        { return velocity; }
    }
}

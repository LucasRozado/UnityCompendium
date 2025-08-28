using UnityEngine;

public class WaypointMovementAgent : MovementAgent
{
    [SerializeField] private float speedMovement = 5f;

    [SerializeField] private Waypoint currentTarget;

    public override Vector3 GetNextVelocity(MovementSubject subject, float deltaTime)
    {
        Vector3 offset = currentTarget.transform.position - subject.transform.position;
        Vector3 direction = offset.normalized;
        Vector3 velocity = direction * speedMovement;

        if (velocity.magnitude * deltaTime > offset.magnitude)
        {
            currentTarget = currentTarget.Next;
            return offset / deltaTime;
        }
        else
        { return velocity; }
    }
}

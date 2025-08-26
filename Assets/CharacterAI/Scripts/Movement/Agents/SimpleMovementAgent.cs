using UnityEngine;

public class SimpleMovementAgent : MovementAgent
{
    [SerializeField] private float speedMovement = 5f;

    [SerializeField] private Transform target;

    public override Vector3 GetNextVelocity(MovementSubject subject)
    {
        Vector3 offset = target.position - subject.transform.position;
        Vector3 direction = offset.normalized;
        Vector3 velocity = direction * speedMovement;

        return velocity;
    }
}

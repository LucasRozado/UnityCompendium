using UnityEngine;

public abstract class MovementAgent : SignalsEnable
{
    public abstract Vector3 CalculateNextVelocity(MovementSubject subject, Vector3 positionTarget, float deltaTime);
    public virtual Vector3 CalculateNextForward(MovementSubject subject, Vector3 positionTarget, float deltaTime)
    {
        return subject.Velocity.normalized;
    }
}

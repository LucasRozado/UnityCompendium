using UnityEngine;

public abstract class MovementAgent : SignalsEnable
{
    public abstract Vector3 CalculateNextVelocity(MovementSubject subject, float deltaTime);
    public virtual Vector3 CalculateNextForward(MovementSubject subject, float deltaTime)
    {
        return subject.Velocity.normalized;
    }
}

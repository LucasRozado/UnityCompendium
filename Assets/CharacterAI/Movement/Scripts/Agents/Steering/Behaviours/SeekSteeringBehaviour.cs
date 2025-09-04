using UnityEngine;

public class SeekSteeringBehaviour : SteeringBehaviour
{
    public override Vector3 CalculateVelocityTarget(MovementSubject subject)
    {
        Vector3 positionOffset = subject.Target.position - subject.Position;
        Vector3 directionTarget = positionOffset.normalized;
        Vector3 velocityTarget = directionTarget * subject.MaximumSpeed;

        return velocityTarget;
    }
}

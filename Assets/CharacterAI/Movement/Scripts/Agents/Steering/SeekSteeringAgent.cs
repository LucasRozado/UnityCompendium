using UnityEngine;

public class SeekSteeringAgent : SteeringMovementAgent
{
    public override Vector3 CalculateVelocityTarget(MovementSubject subject, Vector3 positionTarget)
    {
        Vector3 positionOffset = positionTarget - subject.Position;
        Vector3 directionTarget = positionOffset.normalized;
        Vector3 velocityTarget = directionTarget * subject.MaximumSpeed;

        return velocityTarget;
    }
}

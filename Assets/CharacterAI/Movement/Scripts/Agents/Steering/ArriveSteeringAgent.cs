using UnityEngine;

public class ArriveSteeringAgent : SteeringMovementAgent
{
    public override Vector3 CalculateVelocityTarget(MovementSubject subject, Vector3 positionTarget)
    {
        Vector3 positionOffset = positionTarget - subject.Position;
        float positionDistance = positionOffset.magnitude;
        float speedRamped = subject.MaximumSpeed * (positionDistance / subject.SightDistance);
        float speedClamped = Mathf.Min(speedRamped, subject.MaximumSpeed);
        Vector3 velocityTarget = (speedClamped / positionDistance) * positionOffset;

        return velocityTarget;
    }
}

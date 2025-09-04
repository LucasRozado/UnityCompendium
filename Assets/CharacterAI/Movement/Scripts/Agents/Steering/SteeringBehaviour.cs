using UnityEngine;

public abstract class SteeringBehaviour : SignalsEnable
{
    public abstract Vector3 CalculateVelocityTarget(MovementSubject subject);
}

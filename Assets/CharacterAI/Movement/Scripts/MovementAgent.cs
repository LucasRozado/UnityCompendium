using UnityEngine;

public abstract class MovementAgent : SwitchableBehaviour
{
    public abstract Vector3 CalculateNextVelocity(MovementSubject subject, Vector3 positionTarget, float deltaTime);
}

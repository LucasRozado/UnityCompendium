using UnityEngine;

public abstract class MovementAgent : SwitchableBehaviour
{
    public abstract Vector3 GetNextVelocity(MovementSubject subject, Vector3 targetPosition, float deltaTime);
}

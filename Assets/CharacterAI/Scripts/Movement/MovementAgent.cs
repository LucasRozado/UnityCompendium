using UnityEngine;

public abstract class MovementAgent : MonoBehaviour
{
    public abstract Vector3 GetNextVelocity(MovementSubject subject);
}

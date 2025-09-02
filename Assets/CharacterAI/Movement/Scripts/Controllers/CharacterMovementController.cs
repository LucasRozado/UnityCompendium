using UnityEngine;

public class CharacterMovementController : MovementController
{
    private void Update()
    {
        if (MovementSubject.Velocity != Vector3.zero)
        { transform.rotation = Quaternion.LookRotation(MovementSubject.Velocity); }
    }
}

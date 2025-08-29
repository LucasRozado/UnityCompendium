using UnityEngine;

public class CharacterMovementController : MovementController
{
    private void Update()
    {
        if (MovementSystem.Velocity != Vector3.zero)
        { transform.rotation = Quaternion.LookRotation(MovementSystem.Velocity); }
    }
}

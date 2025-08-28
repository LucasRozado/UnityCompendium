using UnityEngine;

public class CharacterMovementController : MovementController
{
    private void Update()
    {
        Subject.UpdateVelocity(Agent, Time.deltaTime);
        Subject.Move(Time.deltaTime);

        if (Subject.Velocity != Vector3.zero)
        { transform.rotation = Quaternion.LookRotation(Subject.Velocity); }
    }
}

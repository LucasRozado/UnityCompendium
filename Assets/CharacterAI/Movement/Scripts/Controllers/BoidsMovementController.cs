using UnityEngine;

public class BoidsMovementController : MovementController
{
    private void Update()
    {
        if (MovementSubject.Velocity != Vector3.zero)
        { transform.rotation = Quaternion.LookRotation(MovementSubject.Velocity); }
    }

    private void FixedUpdate()
    {
        Collider[] overlaps = Physics.OverlapSphere(MovementSubject.Position, MovementSubject.SightDistance);
    }
}

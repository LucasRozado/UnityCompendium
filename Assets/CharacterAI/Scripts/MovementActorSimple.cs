using UnityEngine;

public class MovementActorSimple : MovementActor
{
    [SerializeField] private float speedMovement = 5f;
    [SerializeField] private float speedRotation = 4f;
    [SerializeField] private Transform target;

    protected override void UpdateDirection()
    {
        if (target.hasChanged) // não atualiza se o target ficar parado!
        {
            direction = target.position - transform.position;
            direction = direction.normalized * speedMovement;
            target.hasChanged = false;
        }
    }

    protected override void UpdateRotation()
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedRotation);
    }
}

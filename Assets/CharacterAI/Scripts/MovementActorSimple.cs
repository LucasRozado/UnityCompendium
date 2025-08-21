using UnityEngine;

public class MovementActorSimple : MovementActor.Using<ControllerColliderHit>
{
    [SerializeField] private float speedMovement = 5f;
    [SerializeField] private float speedRotation = 4f;
    [SerializeField] private Transform target;

    protected Vector3 direction;
    protected Quaternion rotation;

    public override void Move(MovementAgent.Using<ControllerColliderHit> agent)
    {
        UpdateDirection();
        UpdateRotation();

        agent.Move(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedRotation);
    }

    protected virtual void UpdateDirection()
    {
        // if (target.hasChanged) // não atualiza se o target ficar parado!
        {
            direction = target.position - transform.position;
            direction = direction.normalized * speedMovement;
            target.hasChanged = false;
        }
    }

    protected virtual void UpdateRotation()
    {
        rotation = Quaternion.LookRotation(direction);
    }
}

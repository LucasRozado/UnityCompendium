using UnityEngine;

public class CharacterMovementController : MovementController
{
    private MovementActor.Using<ControllerColliderHit> actor;
    private MovementAgent.Using<ControllerColliderHit> agent;

    private new void Awake()
    {
        base.Awake();

        actor = GetComponent<MovementActor.Using<ControllerColliderHit>>();
        agent = GetAgent<CharacterMovementAgent>();
    }

    private void Update()
    {
        actor.Move(agent);
    }
}

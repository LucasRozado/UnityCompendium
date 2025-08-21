using UnityEngine;

public class MovementControllerCharacter : MovementController
{
    private MovementActor.Using<ControllerColliderHit> actor;
    private MovementAgent.Using<ControllerColliderHit> agent;

    private new void Awake()
    {
        base.Awake();

        actor = GetComponent<MovementActor.Using<ControllerColliderHit>>();
        agent = GetAgent<MovementAgentCharacter>();
    }

    private void Update()
    {
        actor.Move(agent);
    }
}

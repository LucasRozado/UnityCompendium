using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementAgentCharacter : MovementAgent.Using<ControllerColliderHit>
{
    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private ControllerColliderHit lastCollision;
    protected override void Move(Vector3 velocity, out CollisionFlags collisionFlags, out ControllerColliderHit colliderHit)
    {
        collisionFlags = characterController.Move(velocity * Time.deltaTime);
        /// <see cref="OnControllerColliderHit"/>: Called on the line above if there is a collision
        
        colliderHit =
            collisionFlags != CollisionFlags.None
            ? lastCollision
            : null;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    { lastCollision = hit; }

    protected override CollisionFlags LastCollisionFlags => characterController.collisionFlags;
}

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementSubject : MovementSubject.Collider<ControllerColliderHit>
{
    private CharacterController characterController;
    private void Awake()
    { characterController = GetComponent<CharacterController>(); }


    private Vector3 velocity;


    public override Vector3 Velocity => velocity;
    protected override void SetVelocity(Vector3 velocity)
    { this.velocity = velocity; }


    private ControllerColliderHit colliderHit;
    protected override void Move(Vector3 velocity)
    {
        CollisionFlags lastCollisionFlags = characterController.collisionFlags;
        ControllerColliderHit lastColliderHit = colliderHit;

        CollisionFlags collisionFlags = characterController.Move(velocity);
        /// <see cref="OnControllerColliderHit"/>: Called on the line above if there is a collision
        if (collisionFlags != CollisionFlags.None)
        { colliderHit = null; }

        bool hasUpdatedCollider = lastColliderHit.collider != colliderHit.collider;
        if (onCollisionUpdate != null)
        {
            bool hasUpdatedFlags = lastCollisionFlags != collisionFlags;
            if (hasUpdatedCollider || hasUpdatedFlags)
            { onCollisionUpdate.Invoke(collisionFlags, colliderHit); }
        }
        if (onCollisionStay != null)
        {
            if (!hasUpdatedCollider)
            { onCollisionStay.Invoke(collisionFlags, colliderHit); }
        }
        if (onCollisionEnter != null)
        {
            bool hasEnteredCollision = colliderHit != null && hasUpdatedCollider;
            if (hasEnteredCollision)
            { onCollisionEnter.Invoke(collisionFlags, colliderHit); }
        }
        if (onCollisionLeave != null)
        {
            bool hasLeftCollision = lastColliderHit != null && hasUpdatedCollider;
            if (hasLeftCollision)
            { onCollisionLeave.Invoke(collisionFlags, colliderHit); }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit colliderHit)
    { this.colliderHit = colliderHit; }
}

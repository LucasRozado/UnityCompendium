using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementSystem : MovementSystem.RunsOnUpdate
{
    public readonly CollisionSystem<ControllerColliderHit> collisions = new();

    private CharacterController characterController;
    private new void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        characterController.enabled = true;
    }
    protected new void OnDisable()
    {
        base.OnDisable();
        characterController.enabled = false;
    }

    private ControllerColliderHit colliderHit;
    protected override void Move(Vector3 velocity)
    {
        CollisionFlags lastCollisionFlags = characterController.collisionFlags;
        ControllerColliderHit lastColliderHit = colliderHit;

        CollisionFlags collisionFlags = characterController.Move(velocity);
        /// <see cref="OnControllerColliderHit"/>: Called on the line above if there is a collision
        if (collisionFlags != CollisionFlags.None)
        { colliderHit = null; }

        bool hasUpdatedCollider = lastColliderHit?.collider != colliderHit?.collider;
        if (collisions.OnCollisionUpdate != null)
        {
            bool hasUpdatedFlags = lastCollisionFlags != collisionFlags;
            if (hasUpdatedCollider || hasUpdatedFlags)
            { collisions.OnCollisionUpdate.Invoke(collisionFlags, colliderHit); }
        }
        if (collisions.OnCollisionStay != null)
        {
            if (!hasUpdatedCollider)
            { collisions.OnCollisionStay.Invoke(collisionFlags, colliderHit); }
        }
        if (collisions.OnCollisionEnter != null)
        {
            bool hasEnteredCollision = colliderHit != null && hasUpdatedCollider;
            if (hasEnteredCollision)
            { collisions.OnCollisionEnter.Invoke(collisionFlags, colliderHit); }
        }
        if (collisions.OnCollisionLeave != null)
        {
            bool hasLeftCollision = lastColliderHit != null && hasUpdatedCollider;
            if (hasLeftCollision)
            { collisions.OnCollisionLeave.Invoke(collisionFlags, colliderHit); }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit colliderHit)
    { this.colliderHit = colliderHit; }
}

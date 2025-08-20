using UnityEngine;

public abstract class MovementAgent : MonoBehaviour
{
    public abstract class Using<ColliderHit> : MovementAgent
    {
        public delegate void CollisionHandler(CollisionFlags flags, ColliderHit hit);
        public void Move(Vector3 velocity, CollisionHandler collisionHandler)
        {
            Move(velocity, out CollisionFlags collisionFlags, out ColliderHit colliderHit);
            collisionHandler?.Invoke(collisionFlags, colliderHit);
        }
        public void Move(Vector3 velocity, CollisionHandler onCollision = null, CollisionHandler onFlagsUpdate = null)
        {
            CollisionFlags lastCollisionFlags = LastCollisionFlags;
            Move(velocity, out CollisionFlags collisionFlags, out ColliderHit colliderHit);

            if (colliderHit != null)
            { onCollision?.Invoke(collisionFlags, colliderHit); }

            if (lastCollisionFlags != collisionFlags)
            { onFlagsUpdate?.Invoke(collisionFlags, colliderHit); }
        }

        protected abstract void Move(Vector3 velocity, out CollisionFlags collisionFlags, out ColliderHit colliderHit);
        protected abstract CollisionFlags LastCollisionFlags { get; }
    }
}
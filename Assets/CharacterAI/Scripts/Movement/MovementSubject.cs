using UnityEngine;

public abstract class MovementSubject : MonoBehaviour
{
    [SerializeField] private float mass = 1f;


    public float Mass => mass;
    public abstract Vector3 Velocity { get; }


    protected abstract void SetVelocity(Vector3 velocity);


    public void UpdateVelocity(MovementAgent agent)
    { SetVelocity(agent.GetNextVelocity(this)); }
    public void Move(float scale) => Move(Velocity * scale);


    protected abstract void Move(Vector3 velocity);


    public abstract class Collider<ColliderHit> : MovementSubject
    {
        public delegate void CollisionHandler(CollisionFlags flags, ColliderHit hit);

        protected CollisionHandler onCollisionUpdate;
        protected CollisionHandler onCollisionEnter;
        protected CollisionHandler onCollisionStay;
        protected CollisionHandler onCollisionLeave;


        private void Handle(ref CollisionHandler var, CollisionHandler handler)
        { var += handler; }

        public void HandleCollisionUpdate(CollisionHandler handler) => Handle(ref onCollisionUpdate, handler);
        public void HandleCollisionEnter(CollisionHandler handler) => Handle(ref onCollisionEnter, handler);
        public void HandleCollisionStay(CollisionHandler handler) => Handle(ref onCollisionStay, handler);
        public void HandleCollisionLeave(CollisionHandler handler) => Handle(ref onCollisionLeave, handler);
    }
}
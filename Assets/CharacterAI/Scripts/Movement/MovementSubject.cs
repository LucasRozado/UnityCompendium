using UnityEngine;

public abstract class MovementSubject : SwitchableBehaviour
{
    [SerializeField] private float mass = 1f;

    public float Mass => mass;
    public abstract Vector3 Velocity { get; }

    public void UpdateVelocity(MovementAgent agent)
    { SetVelocity(agent.GetNextVelocity(this)); }
    public void Move(float scale) => Move(Velocity * scale);


    protected abstract void Move(Vector3 velocity);
    protected abstract void SetVelocity(Vector3 velocity);


    public abstract class Collider<ColliderHit> : MovementSubject
    {
        public delegate void CollisionHandler(CollisionFlags flags, ColliderHit hit);

        protected CollisionHandler onCollisionUpdate;
        protected CollisionHandler onCollisionEnter;
        protected CollisionHandler onCollisionStay;
        protected CollisionHandler onCollisionLeave;


        private void AddHandler(ref CollisionHandler var, CollisionHandler handler)
        { var += handler; }

        public void RunOnCollisionUpdate(CollisionHandler callback) => AddHandler(ref onCollisionUpdate, callback);
        public void RunOnCollisionEnter(CollisionHandler callback) => AddHandler(ref onCollisionEnter, callback);
        public void RunOnCollisionStay(CollisionHandler callback) => AddHandler(ref onCollisionStay, callback);
        public void RunOnCollisionLeave(CollisionHandler callback) => AddHandler(ref onCollisionLeave, callback);
    }
}
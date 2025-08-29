using UnityEngine;

[RequireComponent(typeof(MovementSubject))]
public abstract class MovementSystem : SwitchableBehaviour
{
    private MovementSubject subject;
    private MovementController controller;
    protected void Awake()
    {
        subject = GetComponent<MovementSubject>();
        controller = GetComponent<MovementController>();
    }
    protected new void OnEnable()
    {
        base.OnEnable();
        SetVelocity(subject.Velocity);
    }

    public MovementAgent Agent => controller.Agent;
    public abstract Vector3 Velocity { get; }

    protected abstract void SetVelocity(Vector3 velocity);
    protected abstract void Move(Vector3 velocity);

    public abstract class RunOnUpdate : MovementSystem
    {
        protected void Update()
        { if (Agent != null) Move(Time.deltaTime); }
    }
    public abstract class RunOnFixedUpdate : MovementSystem
    {
        protected void FixedUpdate()
        { if (Agent != null) Move(Time.fixedDeltaTime); }
    }

    private void Move(float deltaTime)
    {
        UpdateVelocity(Agent, deltaTime);
        Move(Velocity * deltaTime);
    }
    private void UpdateVelocity(MovementAgent agent, float deltaTime)
    {
        Vector3 velocity = agent.GetNextVelocity(subject, subject.Target, deltaTime);
        subject.SetVelocity(velocity);
        SetVelocity(velocity);
    }
}

public interface IHoldsAgent
{
    MovementAgent Agent { get; }
}

public class CollisionSystem<ColliderHit>
{
    public delegate void CollisionHandler(CollisionFlags flags, ColliderHit hit);

    public CollisionHandler OnCollisionUpdate;
    public CollisionHandler OnCollisionEnter;
    public CollisionHandler OnCollisionStay;
    public CollisionHandler OnCollisionLeave;

    private void AddHandler(ref CollisionHandler var, CollisionHandler handler)
    { var += handler; }

    public void RunOnCollisionUpdate(CollisionHandler callback) => AddHandler(ref OnCollisionUpdate, callback);
    public void RunOnCollisionEnter(CollisionHandler callback) => AddHandler(ref OnCollisionEnter, callback);
    public void RunOnCollisionStay(CollisionHandler callback) => AddHandler(ref OnCollisionStay, callback);
    public void RunOnCollisionLeave(CollisionHandler callback) => AddHandler(ref OnCollisionLeave, callback);
}
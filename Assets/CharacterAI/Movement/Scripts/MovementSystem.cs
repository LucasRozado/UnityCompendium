using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementSubject))]
public abstract class MovementSystem : SignalsEnable
{
    private MovementController controller;
    protected void Awake()
    {
        controller = GetComponent<MovementController>();
    }

    public MovementSubject Subject => controller.MovementSubject;
    public IReadOnlyCollection<MovementAgent> Agents => controller.MovementAgents;

    public abstract class RunsOnUpdate : MovementSystem
    {
        protected void Update()
        { if (Agents != null) Move(Time.deltaTime); }
    }
    public abstract class RunsOnFixedUpdate : MovementSystem
    {
        protected void FixedUpdate()
        { if (Agents != null) Move(Time.fixedDeltaTime); }
    }

    protected abstract void Move(Vector3 velocity);

    private void Move(float deltaTime)
    {
        UpdateVelocity(Agents, deltaTime);
        UpdateRotation(Agents, deltaTime);
        Move(Subject.Velocity * deltaTime);
    }
    private void UpdateVelocity(IReadOnlyCollection<MovementAgent> agents, float deltaTime)
    {
        Vector3 velocity = Vector3.zero;
        foreach (MovementAgent agent in agents)
        { velocity += agent.CalculateNextVelocity(Subject, Subject.Target, deltaTime); }

        Subject.SetVelocity(velocity);
    }
    private void UpdateRotation(IReadOnlyCollection<MovementAgent> agents, float deltaTime)
    {
        Vector3 forward = Vector3.zero;
        foreach (MovementAgent agent in agents)
        { forward += agent.CalculateNextForward(Subject, Subject.Target, deltaTime); }

        Subject.SetForward(forward);
    }
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
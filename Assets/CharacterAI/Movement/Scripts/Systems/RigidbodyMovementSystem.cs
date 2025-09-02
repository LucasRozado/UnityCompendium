using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovementSystem : MovementSystem.RunsOnFixedUpdate
{
    private new Rigidbody rigidbody;
    protected new void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = Subject.Mass;
        rigidbody.maxLinearVelocity = Subject.MaximumSpeed;
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        rigidbody.isKinematic = true;
    }
    protected new void OnDisable()
    {
        base.OnDisable();
        rigidbody.isKinematic = false;
    }

    protected override void Move(Vector3 velocity)
    {
        rigidbody.AddForce(velocity, ForceMode.Force);
    }
}

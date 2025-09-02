using UnityEngine;

public class MovementSubject : MonoBehaviour
{
    [SerializeField] private MovementSubjectData data;

    [SerializeField] private Transform target;

    private Vector3 velocity;

    public float Mass => data.mass;
    public float MaximumSpeed  => data.maximumSpeed;
    public float MaximumAcceleration  => data.maximumAcceleration;
    public float SightDistance => data.sightDistance;

    public Vector3 Velocity => velocity;
    public Vector3 Forward => transform.forward;
    public Vector3 Position => transform.position;
    public Vector3 Target => target.position;

    public void SetVelocity(Vector3 velocity)
    { this.velocity = velocity; }
    public void SetForward(Vector3 forward)
    { transform.forward = forward; }
    public void SetTarget(Transform target)
    { this.target = target; }
}
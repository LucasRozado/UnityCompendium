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
    public Transform Target => target;

    public void SetVelocity(Vector3 velocity)
    { this.velocity = velocity; }
    public void SetForward(Vector3 forward)
    { transform.rotation = Quaternion.LookRotation(forward); }
    public void SetTarget(Transform target)
    { this.target = target; }
}
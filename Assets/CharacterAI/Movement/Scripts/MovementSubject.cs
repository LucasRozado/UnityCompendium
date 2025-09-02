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
    public float SightAngle => data.sightAngle;

    public Vector3 Velocity => velocity;
    public Vector3 Position => transform.position;
    public Vector3 Target => target.position;

    public void SetVelocity(Vector3 velocity)
    { this.velocity = velocity; }
    public void SetTarget(Transform target)
    { this.target = target; }
}
using System.Collections.Generic;
using UnityEngine;

public class BoidsSteeringBehaviour : SteeringBehaviour
{
    [SerializeField] private float separationIntensity = 1f;
    [SerializeField] private float cohesionIntensity = 1f;
    [SerializeField] private float alignmentIntensity = 0.5f;
    [SerializeField] private LayerMask targetLayer;

    HashSet<Collider> flock;

    public override Vector3 CalculateVelocityTarget(MovementSubject subject)
    {
        Vector3 positionOffset = subject.Target.position - subject.Position;
        Vector3 directionTarget = positionOffset.normalized;
        Vector3 velocityTarget = directionTarget * subject.MaximumSpeed;

        Debug.DrawRay(subject.Position, velocityTarget, Color.gray);

        Collider[] overlaps = Physics.OverlapSphere(subject.Position, subject.SightDistance);

        flock = new();
        foreach (Collider collider in overlaps)
        {
            bool isTargetLayer = IsLayerInMask(collider.gameObject.layer, targetLayer);
            bool isSelf = collider.gameObject == subject.gameObject;

            if (isTargetLayer && !isSelf)
            { flock.Add(collider); }
        }

        velocityTarget += CalculateSeparation(subject, flock) * separationIntensity;
        velocityTarget += CalculateCohesion(subject, flock) * cohesionIntensity;
        CalculateAlignment(subject, flock);

        return velocityTarget;
    }

    private Vector3 CalculateSeparation(MovementSubject subject, ICollection<Collider> others)
    {
        Vector3 positionsSum = Vector3.zero;
        foreach (Collider other in others)
        {
            Vector3 offset = subject.Position - other.transform.position;

            float distance = offset.magnitude;

            float strength = Mathf.Lerp(1, 0, distance / subject.SightDistance);

            Debug.DrawRay(subject.Position, strength * offset.normalized, Color.yellow);

            positionsSum += strength * offset.normalized;
            // TODO: avoidance variable, exponential curve when too close
            // avoidance distance, avoidance strength
        }


        Vector3 velocityTarget = positionsSum;
        Debug.DrawRay(subject.Position, velocityTarget, Color.red);
        return velocityTarget;
    }
    private Vector3 CalculateCohesion(MovementSubject subject, ICollection<Collider> others)
    {
        Vector3 positionsSum = Vector3.zero;
        foreach (Collider other in others)
        {
            positionsSum += other.transform.position;
        }
        Vector3 positionsAverage = positionsSum / others.Count;

        Vector3 velocityTarget = positionsAverage - subject.Position;
        Debug.DrawRay(subject.Position, velocityTarget, Color.green);
        return velocityTarget;
    }
    private Vector3 CalculateAlignment(MovementSubject subject, ICollection<Collider> others)
    {
        Vector3 forwardsSum = Vector3.zero;
        foreach (Collider other in others)
        {
            forwardsSum += other.transform.forward;
        }
        Vector3 forwardsAverage = forwardsSum / others.Count;

        Vector3 forwardTarget = forwardsAverage;
        return forwardTarget;
    }

    private bool IsLayerInMask(int layer, LayerMask mask)
    { return (mask & 1 << layer) == 1 << layer; }
}

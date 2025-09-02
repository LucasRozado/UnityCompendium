using System.Collections.Generic;
using UnityEngine;

public class BoidsSteeringAgent : SteeringMovementAgent
{
    [SerializeField] private LayerMask targetLayer;

    public override Vector3 CalculateVelocityTarget(MovementSubject subject, Vector3 positionTarget)
    {
        Collider[] overlaps = Physics.OverlapSphere(subject.Position, subject.SightDistance);

        HashSet<Collider> others = new();
        foreach (Collider collider in overlaps)
        {
            bool isTargetLayer = IsLayerInMask(collider.gameObject.layer, targetLayer);
            bool isSelf = collider.gameObject == subject.gameObject;

            if (isTargetLayer && !isSelf)
            { others.Add(collider); }
        }

        Vector3 velocityTarget = Vector3.zero;
        velocityTarget += CalculateSeparation(subject, others);
        velocityTarget += CalculateAlignment(subject, others);
        velocityTarget += CalculateCohesion(subject, others);

        return velocityTarget;
    }

    private Vector3 CalculateSeparation(MovementSubject subject, ICollection<Collider> others)
    {
        Vector3 positionsSum = Vector3.zero;
        foreach (Collider other in others)
        {
            positionsSum += other.transform.position;
            // TODO: avoidance variable, exponential curve when too close
            // avoidance distance, avoidance strength
        }

        Vector3 velocityTarget = positionsSum - subject.Position;
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

        Vector3 velocityTarget = positionsAverage;
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

        Vector3 velocityTarget = forwardsAverage;
        return velocityTarget;
    }

    private bool IsLayerInMask(int layer, LayerMask mask)
    { return (mask & 1 << layer) == 1 << layer; }
}

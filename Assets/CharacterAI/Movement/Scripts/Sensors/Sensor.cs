using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public void Sense(MovementSubject subject)
    {
        Collider[] overlaps = Physics.OverlapSphere(subject.Position, subject.SightDistance);
        
        Dictionary<int, Collider> result = new();

        foreach (Collider collider in overlaps)
        {
            bool isSelf = collider.gameObject == subject.gameObject;

            if (!isSelf)
            { result.Add(collider.gameObject.layer, collider); }
        }
    }
}

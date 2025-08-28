using UnityEngine;

public class Waypoint : Node<Waypoint>
{
    public virtual Waypoint Next => next[Random.Range(0, next.Length)];
}

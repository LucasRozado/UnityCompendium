using UnityEngine;

public abstract class MovementActor : MonoBehaviour
{
    public abstract class Using<ColliderHit> : MovementActor
    {
        public abstract void Move(MovementAgent.Using<ColliderHit> agent);
    }
}

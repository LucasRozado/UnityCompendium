using UnityEngine;

public abstract class Node<T> : MonoBehaviour
    where T : Node<T>
{
    [SerializeField] protected T[] next;

    private void OnDrawGizmos()
    {
        if (next != null)
        {
            Gizmos.color = Color.gray;
            foreach (T next in next)
            { Gizmos.DrawLine(transform.position, next.transform.position); }
        }
    }
}

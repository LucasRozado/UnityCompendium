using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementAgentCharacter))]
public abstract class MovementActor : MonoBehaviour
{
    protected Vector3 direction;
    protected Quaternion rotation;
    public UnityAction<Vector3> applyMove;

    private MovementAgentCharacter agent;
    private void Start()
    {
         agent = GetComponent<MovementAgentCharacter>();
    }

    private void Update()
    {
        UpdateDirection();
        UpdateRotation();

        applyMove(direction);
    }

    protected abstract void UpdateDirection();
    protected abstract void UpdateRotation();
}

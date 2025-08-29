using UnityEngine;

[CreateAssetMenu(fileName = nameof(MovementSubjectData), menuName = "Scriptable Objects/" + nameof(MovementSubjectData))]
public class MovementSubjectData : ScriptableObject
{
    public float mass = 1f;
    public float maximumSpeed = 5f;
    public float maximumAcceleration = 5f;
    public float arrivalRadius = 5f;
}
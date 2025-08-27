using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    private readonly ComponentSwitch<MovementSubject> subjects = new();
    private readonly ComponentSwitch<MovementAgent> agents = new();

    protected void Start()
    {
        subjects.Initialize(gameObject);
        agents.Initialize(gameObject);
    }

    public MovementAgent Agent => agents.Active;
    public MovementSubject Subject => subjects.Active;
}


public class SwitchableBehaviour : MonoBehaviour
{
    public delegate void Signal(SwitchableBehaviour self);

    public Signal RunOnEnable { get; set; }
    protected void OnEnable()
    { RunOnEnable?.Invoke(this); }

    public Signal RunOnDisable { get; set; }
    protected void OnDisable()
    { RunOnDisable?.Invoke(this); }
}

public class ComponentSwitch<T> where T : SwitchableBehaviour
{
    private readonly Dictionary<System.Type, T> components = new();

    private T active;

    public void Initialize(GameObject gameObject)
    {
        T initial = null;

        T[] components = gameObject.GetComponents<T>();
        foreach (T component in components)
        {
            this.components[component.GetType()] = component;

            if (component.enabled)
            {
                if (initial == null)
                { initial = component; }
                else
                { component.enabled = false; }
            }

            component.RunOnEnable += OnComponentEnable;
            component.RunOnDisable += OnActiveDisable;
        }

        active = initial;
    }

    public T Active => active;


    public void Switch<Component>() where Component : T
    { Switch(Get<Component>()); }
    public void Deactivate()
    { Switch(null); }

    private void Switch(T component)
    {
        if (active != null)
        { active.enabled = false; }

        if (component != null)
        { component.enabled = true; }
    }
    private void OnComponentEnable(SwitchableBehaviour component)
    {
        if (active != null)
        { active.enabled = false; }

        active = component as T;
    }
    private void OnActiveDisable(SwitchableBehaviour active)
    {
        this.active = null;
    }


    protected Component Get<Component>() where Component : T
    { return components[typeof(Component)] as Component; }
}
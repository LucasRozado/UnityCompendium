using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    private readonly ComponentSwitch<MovementSystem> systems = new();
    private readonly ComponentSwitch<MovementAgent> agents = new();

    protected void Start()
    {
        systems.Initialize(gameObject);
        agents.Initialize(gameObject);
    }

    public MovementAgent Agent => agents.Active;
    public MovementSystem MovementSystem => systems.Active;

    public delegate void Signal<T>(T param);
    protected Signal<MovementAgent> RunOnAgentChanged;
    protected Signal<MovementSystem> RunOnSystemChanged;
}


public class SwitchableBehaviour : MonoBehaviour
{
    public delegate void Signal<T>(T param);

    public Signal<SwitchableBehaviour> RunOnEnable;
    protected void OnEnable()
    { RunOnEnable?.Invoke(this); }

    public Signal<SwitchableBehaviour> RunOnDisable;
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
            component.RunOnDisable += OnComponentDisable;
        }

        active = initial;
        RunOnActiveChanged?.Invoke(active);
    }

    public T Active => active;

    public delegate void Signal<P>(P param);
    public Signal<T> RunOnActiveChanged;

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
        RunOnActiveChanged?.Invoke(active);
    }
    private void OnComponentDisable(SwitchableBehaviour component)
    {
        active = null;
        RunOnActiveChanged?.Invoke(null);
    }


    protected Component Get<Component>() where Component : T
    { return components[typeof(Component)] as Component; }
}
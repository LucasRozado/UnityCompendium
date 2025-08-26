using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    private readonly ComponentSwitch<MovementSubject> subjects = new();
    private readonly ComponentSwitch<MovementAgent> agents = new();

    protected void Awake()
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
    private bool isSwitching;

    public void Initialize(GameObject gameObject)
    {
        T[] components = gameObject.GetComponents<T>();
        foreach (T component in components)
        {
            if (active == null && component.enabled)
            { active = component; }

            component.enabled = false;
            component.RunOnEnable += OnComponentEnable;
            this.components[component.GetType()] = component;
        }
        if (active == null)
        { active = components?[0]; }

        active.enabled = true;
    }

    public T Active => active;


    public void Switch<Component>() where Component : T
    { Switch(Get<Component>()); }
    private void Switch(T component)
    {
        if (isSwitching) return;

        isSwitching = true;

        active.enabled = false;
        active = component;
        active.enabled = true;

        isSwitching = false;
    }
    public void Deactivate()
    { Switch(null); }

    protected Component Get<Component>() where Component : T
    { return components[typeof(Component)] as Component; }


    private void OnComponentEnable(SwitchableBehaviour component)
    {
        if (active == null)
        { Activate(component as T); }
        else
        { Switch(component as T); }
    }
    private void OnActiveDisable(SwitchableBehaviour active)
    {
        Deactivate(active as T);
    }

    private void Activate(T component)
    {
        component.RunOnEnable += OnComponentEnable;
        component.RunOnDisable -= OnActiveDisable;
        active = component;
    }
    private void Deactivate(T component)
    {
        component.RunOnEnable -= OnComponentEnable;
        component.RunOnDisable += OnActiveDisable;
        active = null;
    }
}
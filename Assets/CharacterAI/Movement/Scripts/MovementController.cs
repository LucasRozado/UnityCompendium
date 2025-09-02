using UnityEngine;
using System.Collections.Generic;

public abstract class MovementController : MonoBehaviour
{
    private MovementSubject subject;
    private readonly ComponentSwitch<MovementSystem> systems = new();
    private readonly ComponentSet<MovementAgent> agents = new();
    protected void Awake()
    {
        subject = GetComponent<MovementSubject>();
    }
    protected void Start()
    {
        systems.Initialize(gameObject);
        agents.Initialize(gameObject);
    }

    public MovementSubject MovementSubject => subject;
    public IReadOnlyCollection<MovementAgent> MovementAgents => agents.Active;
    public MovementSystem MovementSystem => systems.Active;
}


public class SignalsEnable : MonoBehaviour
{
    public delegate void Signal<T>(T param);

    public Signal<SignalsEnable> RunOnEnable;
    protected void OnEnable()
    { RunOnEnable?.Invoke(this); }

    public Signal<SignalsEnable> RunOnDisable;
    protected void OnDisable()
    { RunOnDisable?.Invoke(this); }
}

public abstract class ComponentCollection<T> where T : SignalsEnable
{
    private readonly Dictionary<System.Type, T> components = new();
    public void Initialize(GameObject gameObject)
    {
        T[] components = gameObject.GetComponents<T>();
        foreach (T component in components)
        {
            this.components[component.GetType()] = component;

            InitializeComponent(component);

            component.RunOnEnable += OnComponentEnable;
            component.RunOnDisable += OnComponentDisable;
        }
    }
    protected virtual void InitializeComponent(T component) { }

    protected void Toggle(T component)
    { component.enabled = !component.enabled; }
    protected abstract void OnComponentEnable(SignalsEnable component);
    protected abstract void OnComponentDisable(SignalsEnable component);

    protected Component Get<Component>() where Component : T
    { return components[typeof(Component)] as Component; }
}

public class ComponentSet<T> : ComponentCollection<T>
    where T : SignalsEnable
{
    private readonly HashSet<T> active = new();

    protected override void InitializeComponent(T component)
    {
        if (component.enabled)
        { active.Add(component); }
    }

    public IReadOnlyCollection<T> Active => active;

    public void Toggle<Component>() where Component : T
    { Toggle(Get<Component>()); }
    public void Deactivate()
    { foreach (T component in active) component.enabled = false; }

    protected override void OnComponentEnable(SignalsEnable component)
    { active.Add(component as T); }
    protected override void OnComponentDisable(SignalsEnable component)
    { active.Remove(component as T); }
}

public class ComponentSwitch<T> : ComponentCollection<T>
    where T : SignalsEnable
{
    private T active = null;

    protected override void InitializeComponent(T component)
    {
        if (component.enabled)
        {
            if (active == null)
            { active = component; }
            else
            { component.enabled = false; }
        }
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
    protected override void OnComponentEnable(SignalsEnable component)
    {
        if (active != null)
        { active.enabled = false; }

        active = component as T;
    }
    protected override void OnComponentDisable(SignalsEnable component)
    {
        active = null;
    }
}
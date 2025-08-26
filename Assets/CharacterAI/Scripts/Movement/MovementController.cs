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

    class ComponentSwitch<T> where T : MonoBehaviour
    {
        private readonly Dictionary<System.Type, T> components = new();

        private T active;

        public void Initialize(GameObject gameObject)
        {
            T[] components = gameObject.GetComponents<T>();
            foreach (T component in components)
            {
                this.components[component.GetType()] = component;

                if (active == null && component.enabled)
                { active = component; }
                else
                { component.enabled = false; }
            }
            if (active == null)
            { active = components?[0]; }
        }

        public T Active => active;

        protected Component Get<Component>() where Component : T
        { return components[typeof(Component)] as Component; }
    }
}

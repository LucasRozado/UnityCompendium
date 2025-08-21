using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    private readonly Dictionary<Type, MovementAgent> agents = new();
    protected void Awake()
    {
        MovementAgent[] agents = GetComponents<MovementAgent>();
        foreach (var agent in agents)
        { this.agents[agent.GetType()] = agent; }
    }

    protected T GetAgent<T>() where T : MovementAgent
    { return agents[typeof(T)] as T; }
}

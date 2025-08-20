using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementController: MonoBehaviour
{

    private readonly Dictionary<Type, MovementAgent> agents = new();
    private void Awake()
    {
        MovementAgent[] agents = GetComponents<MovementAgent>();
        foreach (var agent in agents)
        { this.agents[agent.GetType()] = agent; }
    }

    public void Start()
    {
        Debug.Log(GetAgent<MovementAgentCharacter>());
    }

    private T GetAgent<T>() where T : MovementAgent
    { return agents[typeof(T)] as T; }
}

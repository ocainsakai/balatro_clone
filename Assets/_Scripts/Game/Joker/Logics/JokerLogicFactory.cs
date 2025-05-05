using Game.Jokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class JokerLogicFactory
{
    private static readonly Dictionary<string, Type> logicTypeMap = new Dictionary<string, Type>();

    public static void Initialize()
    {
        logicTypeMap.Clear();

        var logicTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IJokerLogic).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in logicTypes)
        {
            var attribute = type.GetCustomAttribute<JokerLogicAttribute>();
            if (attribute != null)
            {
                logicTypeMap[attribute.ID] = type;
            }
        }

        Debug.Log($"Found {logicTypeMap.Count} joker logics.");
    }

    public static IJokerLogic CreateLogic(string logicID)
    {
        if (logicTypeMap.Count == 0) Initialize();
        if (string.IsNullOrEmpty(logicID))
        {
            Debug.LogError("Logic ID is null or empty!");
            return null;
        }

        if (logicTypeMap.TryGetValue(logicID, out Type logicType))
        {
            return Activator.CreateInstance(logicType) as IJokerLogic;
        }

        Debug.LogError($"No logic found for ID: {logicID}");
        return null; 
    }
}
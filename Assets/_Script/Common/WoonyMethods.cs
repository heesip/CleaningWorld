using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WoonyMethods
{
    public static void Assert<T>(T caller, params (UnityEngine.Object variable, string variableName)[] variables) where T : MonoBehaviour
    {
        foreach (var (variable, varizbleName) in variables)
        {
            Debug.Assert(variable != null, $"{varizbleName} is null", caller);
        }
    }

    public static void Assert(string caller, params (UnityEngine.Object variable, string variableName)[] variables)
    {
        foreach (var (variable, varizbleName) in variables)
        {
            Debug.Assert(variable != null, $"{caller} : {varizbleName} is null");
        }
    }
}

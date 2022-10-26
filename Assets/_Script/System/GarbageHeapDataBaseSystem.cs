using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageHeapDataBaseSystem
{
    static readonly string HEAP_BASE_KEY = "GarbageAmount";

    public string GarbageHeapBaseKey()
    {
        return HEAP_BASE_KEY;
    }

    public void GetGarbageHeapData(string key, ref int garbageCount, int initializeGarbageCount)
    {
        garbageCount = PlayerPrefs.GetInt(key, initializeGarbageCount);
    }

    public void SetGarbageHeapData(string key, int garbageCount)
    {
        PlayerPrefs.SetInt(key, garbageCount);
    }
}

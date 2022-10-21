using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataBaseSystem
{
    static readonly string COIN_KEY = "CoinAmount";
    static readonly string HEAP_BASE_KEY = "GarbageAmount";
    public void Initialize()
    {
    }

    public void GetCoinData(ref int value)
    {
        if (PlayerPrefs.HasKey(COIN_KEY))
        {
            value = PlayerPrefs.GetInt(COIN_KEY, value);
        }
    }

    public void SetCoinData(int intValue)
    {
        PlayerPrefs.SetInt(COIN_KEY, intValue);
    }

    public string BASE_KEY()
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



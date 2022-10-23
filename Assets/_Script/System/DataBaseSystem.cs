using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataBaseSystem
{
    static readonly string COIN_KEY = "CoinAmount";
    static readonly string HEAP_BASE_KEY = "GarbageAmount";

    StringBuilder stringBuilder;
    static readonly string GARBAGES_KEY = "PlayerGarbages";

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

    public string GetData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        return key;
    }

    public void SetData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

}



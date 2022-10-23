﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataBaseManager : Singleton<DataBaseManager>
{
    DataBaseSystem dataBaseSystem = new DataBaseSystem();


    public void Initialize()
    {
        dataBaseSystem.Initialize();
    }

    public void GetCoinData(ref int coin)
    {
        dataBaseSystem.GetCoinData(ref coin);
    }

    public void SetCoinData(int coin)
    {
        dataBaseSystem.SetCoinData(coin);
    }

    public string GarbageHeapBaseKey()
    {
        return dataBaseSystem.GarbageHeapBaseKey();
    }
    public void GetGarbageHeapData(string key, ref int garbageCount, int initializeGarbageCount)
    {
        dataBaseSystem.GetGarbageHeapData(key, ref garbageCount, initializeGarbageCount);
    }

    public void SetGarbageHeapData(string key, int garbageCount)
    {
        dataBaseSystem.SetGarbageHeapData(key, garbageCount);
    }

    public string GetData(string key)
    {
        return dataBaseSystem.GetData(key);
    }

    public void SetData(string key, string value)
    {
        dataBaseSystem.SetData(key, value);
    }
}

using System.Collections;
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

    public string BASE_KEY()
    {
        return dataBaseSystem.BASE_KEY();
    }
    public void GetGarbageHeapData(string key, ref int garbageCount, int initializeGarbageCount)
    {
        dataBaseSystem.GetGarbageHeapData(key, ref garbageCount, initializeGarbageCount);
    }

    public void SetGarbageHeapData(string key, int garbageCount)
    {
        dataBaseSystem.SetGarbageHeapData(key, garbageCount);
    }

}

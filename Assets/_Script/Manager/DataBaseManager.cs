using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataBaseManager : Singleton<DataBaseManager>
{
    CoinDataBaseSysytem coinDataBaseSysytem = new CoinDataBaseSysytem();
    GarbageHeapDataBaseSystem garbageHeapDataBaseSystem = new GarbageHeapDataBaseSystem();
    PlayerGarbagesDataBaseSystem playerGarbagesDataBaseSystem = new PlayerGarbagesDataBaseSystem();

    public void Initialize()
    {
        playerGarbagesDataBaseSystem.Initialize();
    }

    public void GetCoinData(ref int coin)
    {
        coinDataBaseSysytem.GetCoinData(ref coin);
    }

    public void SetCoinData(int coin)
    {
        coinDataBaseSysytem.SetCoinData(coin);
    }

    public string GarbageHeapBaseKey()
    {
        return garbageHeapDataBaseSystem.GarbageHeapBaseKey();
    }
    public void GetGarbageHeapData(string key, ref int garbageCount, int initializeGarbageCount)
    {
        garbageHeapDataBaseSystem.GetGarbageHeapData(key, ref garbageCount, initializeGarbageCount);
    }

    public void SetGarbageHeapData(string key, int garbageCount)
    {
        garbageHeapDataBaseSystem.SetGarbageHeapData(key, garbageCount);
    }

    public string GetData(string key)
    {
        return playerGarbagesDataBaseSystem.GetData(key);
    }

    public void SetData(string key, string value)
    {
        playerGarbagesDataBaseSystem.SetData(key, value);
    }
}

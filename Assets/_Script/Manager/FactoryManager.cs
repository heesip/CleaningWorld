using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FactoryManager : Singleton<FactoryManager>
{
    PrefabFactorySystem prefabFactorySystem = new PrefabFactorySystem();

    public void Initialize()
    {
        prefabFactorySystem.Initialize(transform);
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageDetailType, Vector3 spawnPoint)
    {
        return prefabFactorySystem.GetGarbageObject(garbageDetailType, spawnPoint);
    }

    public Coin GetCoin(Vector3 spawnPosition)
    {
        return prefabFactorySystem.GetCoin(spawnPosition);
    }
}

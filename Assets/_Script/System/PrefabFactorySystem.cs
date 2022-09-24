using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactorySystem
{ 
    Transform factoryManager;

    GarbageObject tempGarbageObject;
    Coin tempCoin;
    ObjectPoolSystem can1ObjectPool;
    ObjectPoolSystem can2ObjectPool;
    ObjectPoolSystem food1ObjectPool;
    ObjectPoolSystem food2ObjectPool;
    ObjectPoolSystem glass1ObjectPool;
    ObjectPoolSystem glass2ObjectPool;
    ObjectPoolSystem paper1ObjectPool;
    ObjectPoolSystem paper2ObjectPool;
    ObjectPoolSystem plastic1ObjectPool;
    ObjectPoolSystem plastic2ObjectPool;
    ObjectPoolSystem coinObjectPool;

    public void Initialize(Transform factoryManager)
    {
        this.factoryManager = factoryManager;
        InitializeGarbageObjectPool(ref can1ObjectPool, GarbageDetailType.Can1);
        InitializeGarbageObjectPool(ref can2ObjectPool, GarbageDetailType.Can2);
        InitializeGarbageObjectPool(ref food1ObjectPool, GarbageDetailType.Food1);
        InitializeGarbageObjectPool(ref food2ObjectPool, GarbageDetailType.Food2);
        InitializeGarbageObjectPool(ref glass1ObjectPool, GarbageDetailType.Glass1);
        InitializeGarbageObjectPool(ref glass2ObjectPool, GarbageDetailType.Glass2);
        InitializeGarbageObjectPool(ref paper1ObjectPool, GarbageDetailType.Paper1);
        InitializeGarbageObjectPool(ref paper2ObjectPool, GarbageDetailType.Paper2);
        InitializeGarbageObjectPool(ref plastic1ObjectPool, GarbageDetailType.Plastic1);
        InitializeGarbageObjectPool(ref plastic2ObjectPool, GarbageDetailType.Plastic2);
        coinObjectPool = new ObjectPoolSystem(GameResourcesManager.Instance.GetCoinObjectPrefab(), 1, factoryManager);

    }

    void InitializeGarbageObjectPool(ref ObjectPoolSystem objectPool, GarbageDetailType garbageDetailType)
    {
        objectPool = new ObjectPoolSystem(GetGarbageObjectPrefab(garbageDetailType),
                                          defaultPoolSize: 1,
                                          parent: factoryManager);
    }

    GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageDetailType)
    {
        return GameResourcesManager.Instance.GetGarbageObjectPrefab(garbageDetailType);
    }

    ObjectPoolSystem GetObjectPoolSystem(GarbageDetailType garbageType)
    {
        return garbageType switch
        {
            GarbageDetailType.Can1 => can1ObjectPool,
            GarbageDetailType.Can2 => can2ObjectPool,
            GarbageDetailType.Food1 => food1ObjectPool,
            GarbageDetailType.Food2 => food2ObjectPool,
            GarbageDetailType.Glass1 => glass1ObjectPool,
            GarbageDetailType.Glass2 => glass2ObjectPool,
            GarbageDetailType.Paper1 => paper1ObjectPool,
            GarbageDetailType.Paper2 => paper2ObjectPool,
            GarbageDetailType.Plastic1 => plastic1ObjectPool,
            GarbageDetailType.Plastic2 => plastic2ObjectPool,
            _ => null,
        };
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageType, Vector3 spawnPoint)
    {
        tempGarbageObject = GetObjectPoolSystem(garbageType).Get() as GarbageObject;
        tempGarbageObject.transform.position = spawnPoint;
        return tempGarbageObject;
    }

    public Coin GetCoin(Vector3 spawnPosition)
    {
        tempCoin = coinObjectPool.Get() as Coin;
        tempCoin.transform.position = spawnPosition;
        return tempCoin;
    }

}

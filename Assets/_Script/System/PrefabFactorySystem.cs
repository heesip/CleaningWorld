using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactorySystem
{
    Transform factoryManager;

    GarbageObject tempGarbageObject;
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

    public void Initialize(Transform factoryManager)
    {
        this.factoryManager = factoryManager;
        InutalizeIiObjectPool(ref can1ObjectPool, GarbageType.Can1);
        InutalizeIiObjectPool(ref can2ObjectPool, GarbageType.Can2);
        InutalizeIiObjectPool(ref food1ObjectPool, GarbageType.Food1);
        InutalizeIiObjectPool(ref food2ObjectPool, GarbageType.Food2);
        InutalizeIiObjectPool(ref glass1ObjectPool, GarbageType.Glass1);
        InutalizeIiObjectPool(ref glass2ObjectPool, GarbageType.Glass2);
        InutalizeIiObjectPool(ref paper1ObjectPool, GarbageType.Paper1);
        InutalizeIiObjectPool(ref paper2ObjectPool, GarbageType.Paper2);
        InutalizeIiObjectPool(ref plastic1ObjectPool, GarbageType.Plastic1);
        InutalizeIiObjectPool(ref plastic2ObjectPool, GarbageType.Plastic2);
    }

    void InutalizeIiObjectPool(ref ObjectPoolSystem objectPool, GarbageType garbageType)
    {
        objectPool = new ObjectPoolSystem(GetGarbageObjectPrefab(garbageType),
                                          defaultPoolSize: 1,
                                          parent: factoryManager);
    }

    GarbageObject GetGarbageObjectPrefab(GarbageType garbageType)
    {
        return GameResourcesManager.Instance.GetGarbageObjectPrefab(garbageType);
    }

    ObjectPoolSystem GetObjectPoolSystem(GarbageType garbageType)
    {
        return garbageType switch
        {
            GarbageType.Can1 => can1ObjectPool,
            GarbageType.Can2 => can2ObjectPool,
            GarbageType.Food1 => food1ObjectPool,
            GarbageType.Food2 => food2ObjectPool,
            GarbageType.Glass1 => glass1ObjectPool,
            GarbageType.Glass2 => glass2ObjectPool,
            GarbageType.Paper1 => paper1ObjectPool,
            GarbageType.Paper2 => paper2ObjectPool,
            GarbageType.Plastic1 => plastic1ObjectPool,
            GarbageType.Plastic2 => plastic2ObjectPool,
            _ => null,
        };
    }

    public GarbageObject GetGarbageObject(GarbageType garbageType, Vector3 spawnPoint)
    {
        tempGarbageObject = GetObjectPoolSystem(garbageType).Get() as GarbageObject;
        tempGarbageObject.transform.position = spawnPoint;
        return tempGarbageObject;
    }

}

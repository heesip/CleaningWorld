using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGarbageStackSystem
{

    Player player;
    GarbageStack<GarbageObject> myGarbages = new GarbageStack<GarbageObject>();

    [SerializeField] Transform pivotCenter;
    [SerializeField] int upgradeValue = 10;
    [SerializeField] int maxCount = 20;
    [SerializeField] float garbageGapUp = 0.5f;
    [SerializeField] float garbageGapBack = 0.75f;
    [SerializeField] int orderCount = 3;

    string GARBAGES_KEY = "PlayerGarbages";

    public void Initialize(Player player)
    {
        this.player = player;
        Debug.Assert(pivotCenter != null, "pivotCenter is null");
        myGarbages.Initialize(GARBAGES_KEY, GetPosition, pivotCenter);
        for (int i = 1; i < Enum.GetNames(typeof(GarbageType)).Length; i++)
        {
            UIManager.Instance.UpdateGarbageAmount((GarbageType)i,
                                                   myGarbages.GetCountOfGarbageType((GarbageType)i));
        }
    }

    Vector3 GetPosition(int index)
    {
        return Vector3.zero
            + ((index % orderCount) * garbageGapUp * Vector3.up)
            + ((index / orderCount) * garbageGapBack * Vector3.back);
    }

    public void OnUpgrade()
    {
        maxCount += upgradeValue;
    }

    void UpdateCount(GarbageType garbageType)
    {
        myGarbages.SaveGarbage(GARBAGES_KEY);
        UIManager.Instance.UpdateGarbageAmount(garbageType,
                                               myGarbages.GetCountOfGarbageType(garbageType));
    }

    public bool IsAbleToGetGarbage()
    {
        return myGarbages.Count() < maxCount;
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return myGarbages.IsAbleToPopGarbage(garbageType);
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float delay)
    {
        garbageObject.transform.SetParent(pivotCenter);
        garbageObject.transform.localRotation = Quaternion.identity;

        myGarbages.Push(garbageObject, delay);
        UpdateCount(garbageObject.GarbageType);
    }

    public GarbageObject OnWastebasket(GarbageType garbageType)
    {
        var result = myGarbages.Pop(garbageType);
        if (result.isContained)
        {
            result.garbageObject.transform.SetParent(null);
            UpdateCount(garbageType);
        }
        return result.garbageObject;
    }
}

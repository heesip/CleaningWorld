using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGarbageStackSystem
{
    class GarbageCountInfo
    {
        public GarbageType GarbageType;
        public int count;
    }

    Player player;
    GarbageStack<GarbageObject> myGarbages = new GarbageStack<GarbageObject>();

    [SerializeField] Transform pivotCenter;
    [SerializeField] int upgradeValue = 10;
    [SerializeField] int maxCount = 20;
    [SerializeField] float garbageGapUp = 0.5f;
    [SerializeField] float garbageGapBack = 0.75f;
    [SerializeField] int orderCount = 3;

    Dictionary<GarbageType, GarbageCountInfo> garbageCountInfoMap = new Dictionary<GarbageType, GarbageCountInfo>();

    int canCount;
    int foodCount;
    int glassCount;
    int paperCount;
    int plasticCount;

    public void Initialize(Player player)
    {
        this.player = player;
        Debug.Assert(pivotCenter != null, "pivotCenter is null");
        myGarbages.Initialize(GetPosition);
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

    GarbageCountInfo GetGarbageCountInfo(GarbageType garbageType)
    {
        if (garbageCountInfoMap.ContainsKey(garbageType) == false)
        {
            InitialzeGarbageCountMap(garbageType);
        }

        return garbageCountInfoMap[garbageType];

        void InitialzeGarbageCountMap(GarbageType garbageType)
        {
            garbageCountInfoMap[garbageType] = new GarbageCountInfo()
            {
                GarbageType = garbageType,
                count = 0,
            };
        }
    }

    void UpdateCount(GarbageType garbageType, int changeValue)
    {
        var garbageInfo = GetGarbageCountInfo(garbageType);
        garbageInfo.count += changeValue;
        UIManager.Instance.UpdateGarbageAmount(garbageType, garbageInfo.count);
    }

    void IncreaseCount(GarbageType garbageType)
    {
        UpdateCount(garbageType, +1);
    }

    void DecreaseCount(GarbageType garbageType)
    {
        UpdateCount(garbageType, -1);
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
        IncreaseCount(garbageType: garbageObject.GarbageType);
    }

    public GarbageObject OnWastebasket(GarbageType garbageType)
    {
        var result = myGarbages.Pop(garbageType);
        if (result.isContained)
        {
            result.garbageObject.transform.SetParent(null);
            DecreaseCount(garbageType);
        }
        return result.garbageObject;
    }
}

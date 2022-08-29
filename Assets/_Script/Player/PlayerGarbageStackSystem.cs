using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class GarbageStack<T> where T : GarbageObject
{
    readonly List<T> container = new List<T>();
    T tempItem;

    public int Count()
    {
        return container.Count;
    }

    public void Push(T item, Func<int, Vector3> getPosition, float delay)
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].transform.localPosition = getPosition(i);
        }

        item.transform.DOLocalMove(getPosition(container.Count), delay);

        container.Add(item);
    }

    public (bool isContained, T garbageObject) Pop(GarbageType garbageType)
    {
        tempItem = container.FirstOrDefault(x => x.GarbageType == garbageType);
        
        if (tempItem == null || tempItem.GarbageType == GarbageType.None)
        {
            Debug.LogError("존재하지 않음!");
            return (false, null);
        }

        container.Remove(tempItem);
        return (true, tempItem);
    }

}

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
    }

    GarbageType GetGarbageTypeFromDetilType(GarbageDetailType garbageDetailType)
    {
        switch (garbageDetailType)
        {
            case GarbageDetailType.Can1:
            case GarbageDetailType.Can2:
                return GarbageType.Can;
            case GarbageDetailType.Food1:
            case GarbageDetailType.Food2:
                return GarbageType.Food;
            case GarbageDetailType.Glass1:
            case GarbageDetailType.Glass2:
                return GarbageType.Glass;
            case GarbageDetailType.Paper1:
            case GarbageDetailType.Paper2:
                return GarbageType.Paper;
            case GarbageDetailType.Plastic1:
            case GarbageDetailType.Plastic2:
                return GarbageType.Plastic;
            default:
                return GarbageType.None;
        }
    }

    GarbageCountInfo GetGarbageCountInfo(GarbageType garbageType)
    {
        if(garbageCountInfoMap.ContainsKey(garbageType) == false)
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

    void UpdateCount(GarbageDetailType garbageDetailType, int changeValue)
    {
        var garbageType = GetGarbageTypeFromDetilType(garbageDetailType);
        var garbageInfo = GetGarbageCountInfo(garbageType);
        garbageInfo.count += changeValue;
        UIManager.Instance.UpdateGarbageAmount(garbageType, garbageInfo.count);
    }

    void IncreaseCount(GarbageDetailType garbageDetailType)
    {
        UpdateCount(garbageDetailType, +1);
    }
    
    void DecreaseCount(GarbageDetailType garbageDetailType)
    {
        UpdateCount(garbageDetailType, -1);
    }

    public bool IsAbleToGetGarbage()
    {
        return myGarbages.Count() < maxCount;
    }


    public void OnGarbageHeap(GarbageObject garbageObject, float delay)
    {
        garbageObject.transform.SetParent(pivotCenter);
        garbageObject.transform.localRotation = Quaternion.identity;

        myGarbages.Push(garbageObject, GetPosition, delay);
        IncreaseCount(garbageDetailType: garbageObject.GarbageDetailType);

        Vector3 GetPosition(int index)
        {
            return Vector3.zero
                + ((index % orderCount) * garbageGapUp * Vector3.up)
                + ((index / orderCount) * garbageGapBack * Vector3.back);
        }
    }

   

    public void OnWastebasket(GarbageType garbageType)
    {
        myGarbages.Pop(garbageType);
    }
}

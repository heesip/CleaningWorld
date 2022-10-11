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

    int canAmount;
    int foodAmount;
    int glassAmount;
    int paperAmount;
    int plasticAmount;

    static readonly string CAN_KEY = "CanAmount";
    static readonly string FOOD_KEY = "FoodAmount;";
    static readonly string GLASS_KEY = "GlassAmount;";
    static readonly string PAPER_KEY = "PaperAmount;";
    static readonly string PLASTIC_KEY = "PlasticAmount;";

    public void Initialize(Player player)
    {
        this.player = player;
        Debug.Assert(pivotCenter != null, "pivotCenter is null");
        myGarbages.Initialize(GetPosition);
        LoadData(CAN_KEY, ref canAmount);
        LoadData(FOOD_KEY, ref foodAmount);
        LoadData(GLASS_KEY, ref glassAmount);
        LoadData(PAPER_KEY, ref paperAmount);
        LoadData(PLASTIC_KEY, ref plasticAmount);
    }

    void LoadData(string key, ref int amount)
    {
        if (PlayerPrefs.HasKey(key))
        {
            amount = PlayerPrefs.GetInt(key, 0);
        }

    }
    void SaveData(string key, int amount)
    {
        PlayerPrefs.SetInt(key, amount);
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

        switch (garbageType)
        {
            case GarbageType.Can:
                SaveData(CAN_KEY, garbageInfo.count);
                break;
            case GarbageType.Food:
                SaveData(FOOD_KEY, garbageInfo.count);
                break;
            case GarbageType.Glass:
                SaveData(GLASS_KEY, garbageInfo.count);
                break;
            case GarbageType.Paper:
                SaveData(PAPER_KEY, garbageInfo.count);
                break;
            case GarbageType.Plastic:
                SaveData(PLASTIC_KEY, garbageInfo.count);
                break;
            default:
                Debug.Log("알수없는 쓰레기가 저장되었습니다.");
                break;
        }
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

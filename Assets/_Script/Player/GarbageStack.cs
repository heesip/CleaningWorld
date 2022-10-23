using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

class SaveSystem
{
    string KEY;
    Action<GarbageType> onLoadGarbage;
    StringBuilder stringBuilder;

    public SaveSystem(string KEY, Action<GarbageType> onLoadGarbage)
    {
        this.KEY = KEY;
        this.onLoadGarbage = onLoadGarbage;
    }

    public void ConvertToGarvageType()
    {
        var stringData = DataBaseManager.Instance.GetData(KEY);
        for (int i = 0; i < stringData.Length; i++)
        {
            var newGarbageType = (GarbageType)Convert.ToInt32(stringData[i].ToString());
            onLoadGarbage?.Invoke(newGarbageType);
        }
    }

    public void ConvertToString<T>(List<T> container) where T : GarbageObject
    {
        if (stringBuilder == null)
        {
            stringBuilder = new StringBuilder();
        }

        stringBuilder.Clear();
        foreach (var item in container)
        {
            stringBuilder.Append((int)item.GarbageType);
        }

        DataBaseManager.Instance.SetData(KEY, stringBuilder.ToString());
    }
}

class GarbageStack<T> where T : GarbageObject
{
    SaveSystem saveSystem;
    Transform pivotCenter;
    readonly List<T> container = new List<T>();
    Func<int, Vector3> getPosition;
    T tempItem;
    //containerMap은 각 쓰레기별 카운트를 저장하기 위해 사용되는 딕셔너리
    Dictionary<GarbageType, int> containerMap = new Dictionary<GarbageType, int>();

    public void Initialize(string key, Func<int, Vector3> getPosition, Transform pivotCenter)
    {
        this.getPosition = getPosition;
        this.pivotCenter = pivotCenter;
        saveSystem = new SaveSystem(key, OnLoadGarbage);
        LoadGarbages();
    }

    void OnLoadGarbage(GarbageType garbageType)
    {
        var garbageObject = GenerateGarbage(garbageType);
        garbageObject.transform.SetParent(pivotCenter);
        garbageObject.transform.localRotation = Quaternion.identity;

        Push((T)garbageObject, delay: 0);
    }

    GarbageObject GenerateGarbage(GarbageType garbageType)
    {
        GarbageDetailType randomType = GarbageDetailType.None;
        switch (garbageType)
        {
            case GarbageType.Can:
                randomType = GarbageDetailType.Can1;
                break;
            case GarbageType.Food:
                randomType = GarbageDetailType.Food1;
                break;
            case GarbageType.Glass:
                randomType = GarbageDetailType.Glass1;
                break;
            case GarbageType.Paper:
                randomType = GarbageDetailType.Paper1;
                break;
            case GarbageType.Plastic:
                randomType = GarbageDetailType.Plastic1;
                break;
        }
        randomType += Random.Range(0, 2);
        return FactoryManager.Instance.GetGarbageObject(randomType, Vector3.zero);
    }

    public void LoadGarbages()
    {
        saveSystem.ConvertToGarvageType();
    }

    public void SaveGarbage()
    {
        saveSystem.ConvertToString(container);
    }


    public int Count()
    {
        return container.Count;
    }

    public void Push(T garbageObject, float delay)
    {
        SortPosition();

        garbageObject.transform.DOLocalMove(getPosition(container.Count), delay);

        container.Add(garbageObject);

        if (containerMap.ContainsKey(garbageObject.GarbageType) == false)
        {
            containerMap[garbageObject.GarbageType] = 0;
        }
        containerMap[garbageObject.GarbageType]++;
    }

    private void SortPosition()
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].transform.localPosition = getPosition(i);
        }
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
        containerMap[tempItem.GarbageType]--;
        SortPosition();
        return (true, tempItem);
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return containerMap.ContainsKey(garbageType)
            && containerMap[garbageType] > 0;
    }

    public int GetCountOfGarbageType(GarbageType garbageType)
    {
        if (containerMap.ContainsKey(garbageType) == false)
        {
            containerMap[garbageType] = 0;
        }
        return containerMap[garbageType];
    }
}

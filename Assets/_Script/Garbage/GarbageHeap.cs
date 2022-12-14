using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeap : MonoBehaviour
{
    int garbageDetailTypesMaxNumber;
    [SerializeField] GarbageHeapPlayerDetector garbageHeapPlayerDetector;
    [SerializeField] GarbageAmountBox garbageAmountBox;
    [SerializeField] float delay = 0.1f;
    int garbageCount;
    [SerializeField] int initializeGarbageCount = 100;
    [SerializeField] Transform inner;
    int originGarbageCount;
    Vector3 originScale;

    string BASE_KEY;
    [SerializeField] int uid;
    string REAL_KEY;
        
    void Start()
    {
        REAL_KEY = GetRealKey();
        WoonyMethods.Assert(this, (garbageHeapPlayerDetector, nameof(garbageHeapPlayerDetector)),
                                  (garbageAmountBox, nameof(garbageAmountBox)),
                                  (inner, nameof(inner)));

        var garbageDetailTypes = Enum.GetNames(typeof(GarbageDetailType));
        garbageDetailTypesMaxNumber = garbageDetailTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        garbageAmountBox.Initialize();

        originGarbageCount = initializeGarbageCount;
        originScale = inner.localScale;
        DataBaseManager.Instance.GetGarbageHeapData(REAL_KEY, ref garbageCount, initializeGarbageCount);
        garbageAmountBox.UpdateAmount(garbageCount);
        HeapScaleUpdate();
    }
    
    string GetRealKey()
    {
        BASE_KEY = DataBaseManager.Instance.GarbageHeapBaseKey();
        return BASE_KEY + uid.ToString();
    }

    void SubGarbageCount(int value)
    {
        value = Math.Abs(value);
        garbageCount -= value;
        garbageAmountBox.UpdateAmount(garbageCount);
        DataBaseManager.Instance.SetGarbageHeapData(REAL_KEY, garbageCount);
    }

    void OnPlayerEnter()
    {
        StopGenerateGabageCo();
        generateGarbageCoHandle = StartCoroutine(GenerateGarbageCo());
    }
    void OnPlayerExit()
    {
        StopGenerateGabageCo();
    }

    void StopGenerateGabageCo()
    {
        if (generateGarbageCoHandle != null)
        {
            StopCoroutine(generateGarbageCoHandle);
        }
    }

    Coroutine generateGarbageCoHandle;
    IEnumerator GenerateGarbageCo()
    {
        var isTrue = true;
        while (isTrue)
        {
            OnGarbageHeap();
            yield return new WaitForSeconds(delay);
        }
    }

    void OnGarbageHeap()
    {
        if (Player.Instance.IsAbleToGetGarbage() == false || IsAbleToGetGarbage())
        {
            return;
        }

        Player.Instance.OnGarbageHeap(GenerateGarbage(), delay);

        bool IsAbleToGetGarbage()
        {
            return garbageCount <= 0;
        }
    }

    GarbageObject GenerateGarbage()
    {
        GarbageDetailType randomeType = (GarbageDetailType)Random.Range(1, garbageDetailTypesMaxNumber);

        var randomGarbage = FactoryManager.Instance.GetGarbageObject(randomeType,
                                                                     transform.position);
        OnGenerateGarbage();

        return randomGarbage;

        void OnGenerateGarbage()
        {
            SubGarbageCount(1);
            HeapScaleUpdate();
        }
    }

    void HeapScaleUpdate()
    {
        inner.localScale = garbageCount / (float)originGarbageCount * originScale;
    }


}


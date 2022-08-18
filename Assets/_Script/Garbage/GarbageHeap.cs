﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeap : MonoBehaviour
{
    int garbageTypesMaxNumber;
    [SerializeField] GarbageHeapPlayerDetector garbageHeapPlayerDetector;
    [SerializeField] float delay = 0.1f;
    [SerializeField] int garbageCount = 100;
    [SerializeField] Transform inner;
    int originGarbageCount;
    Vector3 originScale;

    void Start()
    {
        WoonyMethods.Assert(this, (garbageHeapPlayerDetector, nameof(garbageHeapPlayerDetector)));

        var garbageTypes = Enum.GetNames(typeof(GarbageType));
        garbageTypesMaxNumber = garbageTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);

        originGarbageCount = garbageCount;
        originScale = inner.localScale;
    }

    void OnPlayerEnter()
    {
        StopGenerateGabageCo();
        GenerateGarbageCoHandle = StartCoroutine(GenerateGarbageCo());
    }

    void StopGenerateGabageCo()
    {
        if (GenerateGarbageCoHandle != null)
        {
            StopCoroutine(GenerateGarbageCoHandle);
        }
    }

    void OnPlayerExit()
    {
        StopGenerateGabageCo();
    }

    Coroutine GenerateGarbageCoHandle;

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
        GarbageDetailType randomeType = (GarbageDetailType)Random.Range(1, garbageTypesMaxNumber);

        var randomGarbage = FactoryManager.Instance.GetGarbageObject(randomeType,
                                                                     transform.position);
        OnGenerateGarbage();

        return randomGarbage;

        void OnGenerateGarbage()
        {
            garbageCount--;
            inner.localScale = garbageCount / (float)originGarbageCount * originScale;
        }
    }

}


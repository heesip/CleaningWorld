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
    [SerializeField] int garbageCount = 100;
    [SerializeField] Transform inner;
    int originGarbageCount;
    Vector3 originScale;

    [SerializeField] string GARBAGE_LOAD_KEY = null;
    //string TESTKEY = "A";

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        WoonyMethods.Assert(this, (garbageHeapPlayerDetector, nameof(garbageHeapPlayerDetector)),
                                  (garbageAmountBox, nameof(garbageAmountBox)),
                                  (inner, nameof(inner)));

        var garbageDetailTypes = Enum.GetNames(typeof(GarbageDetailType));
        garbageDetailTypesMaxNumber = garbageDetailTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        garbageAmountBox.Initialize();
        LoadData();
        //garbageAmountBox.UpdateAmount(garbageCount);

        originGarbageCount = garbageCount;
        originScale = inner.localScale;
    }

    //void KeySave()
    //{
    //    PlayerPrefs.SetString(TESTKEY, GARBAGE_LOAD_KEY);
    //}

    void SaveData()
    {
        PlayerPrefs.SetInt(GARBAGE_LOAD_KEY, garbageCount);
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey(GARBAGE_LOAD_KEY))
        {
            garbageCount = PlayerPrefs.GetInt(GARBAGE_LOAD_KEY, garbageCount);
        }
        garbageAmountBox.UpdateAmount(garbageCount);

    }

    void SubGarbageCount(int value)
    {
        value = Math.Abs(value);
        garbageCount -= value;
        garbageAmountBox.UpdateAmount(garbageCount);
        SaveData();
    }

    void OnPlayerEnter()
    {
        StopGenerateGabageCo();
        generateGarbageCoHandle = StartCoroutine(GenerateGarbageCo());
    }

    void StopGenerateGabageCo()
    {
        if (generateGarbageCoHandle != null)
        {
            StopCoroutine(generateGarbageCoHandle);
        }
    }

    void OnPlayerExit()
    {
        StopGenerateGabageCo();
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
            inner.localScale = garbageCount / (float)originGarbageCount * originScale;
        }
    }
}


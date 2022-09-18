using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] float delay = 0.1f;

    private void Start()
    {
        WoonyMethods.Assert(this, (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
    }

    void OnPlayerEnter()
    {
        StopThrowGarbageCo();
        throwGarbageCoHandle = StartCoroutine(ThrowGarbage());
    }

    void OnPlayerExit()
    {
        StopThrowGarbageCo();
    }

    private void StopThrowGarbageCo()
    {
        if (throwGarbageCoHandle != null)
        {
            StopCoroutine(throwGarbageCoHandle);
        }
    }

    Coroutine throwGarbageCoHandle;

    IEnumerator ThrowGarbage()
    {
        var isTrue = true;
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType))
        {
            var result = Player.Instance.OnWastebasket(garbageType);
            yield return result.transform.DOMove(transform.position, delay)
                                         .WaitForCompletion();
        }
    }
}


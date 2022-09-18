using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] WastebasketLidController lidController;
    [SerializeField] float delay = 0.1f;

    private void Start()
    {
        WoonyMethods.Assert(this, (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        lidController.Initialize();
    }

    void OnPlayerEnter()
    {
        StopThrowGarbageCo();
        throwGarbageCoHandle = StartCoroutine(ThrowGarbage());
    }

    void OnPlayerExit()
    {
        StopThrowGarbageCo();
        lidController.Close();
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
        yield return lidController.Open()
                                  .WaitForCompletion();
        var isTrue = true;
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType))
        {
            var result = Player.Instance.OnWastebasket(garbageType);
            yield return result.transform.DOMove(transform.position, delay)
                                         .WaitForCompletion();
        }
    }
}


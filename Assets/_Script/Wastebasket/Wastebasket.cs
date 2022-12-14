using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] WastebasketLidController lidController;
    [SerializeField] Transform garbageArrivedPoint;
    [SerializeField] float addedYValue = 2;
    [SerializeField] float delay = 0.1f;
    bool isPlayerAttached = false;

    private void Start()
    {
        WoonyMethods.Assert(this, (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        lidController.Initialize();
    }

    void OnPlayerEnter()
    {
        isPlayerAttached = true;
        StopThrowGarbageCo();
        throwGarbageCoHandle = StartCoroutine(ThrowGarbage());
    }

    void OnPlayerExit()
    {
        isPlayerAttached = false;
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
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType) && isPlayerAttached)
        {
            GarbageObject garbageObject = Player.Instance.OnWastebasket(garbageType);
            yield return garbageObject.OnWastebasket(garbageArrivedPoint.position,
                                                     addedYValue,
                                                     delay)
                                    .WaitForCompletion();
            var newCoin = FactoryManager.Instance.GetCoin(garbageArrivedPoint.position);
            newCoin.FlyCoin();
        }
    }
}


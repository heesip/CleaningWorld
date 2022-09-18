using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    class LidOpenCloseInfo
    {
        public Vector3 rotation;
        public float duration;

        public LidOpenCloseInfo(Vector3 rotation, float duration)
        {
            this.rotation = rotation;
            this.duration = duration;
        }
    }

    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] Transform lid;
    LidOpenCloseInfo lidOpen = new LidOpenCloseInfo(new Vector3(0, 105, -90), 0.2f);
    LidOpenCloseInfo lidClose = new LidOpenCloseInfo(new Vector3(0, 0, -90), 1);
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
       
        lid.DOKill();
        lid.DOLocalRotate(lidOpen.rotation, lidOpen.duration);
        throwGarbageCoHandle = StartCoroutine(ThrowGarbage());
    }

    void OnPlayerExit()
    {
        StopThrowGarbageCo();

        lid.DOKill();
        lid.DOLocalRotate(lidClose.rotation, lidClose.duration).SetEase(Ease.OutBounce);
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
        yield return new WaitForSeconds(lidOpen.duration);
        var isTrue = true;
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType))
        {
            var result = Player.Instance.OnWastebasket(garbageType);
            yield return result.transform.DOMove(transform.position, delay)
                                         .WaitForCompletion();
        }
    }
}


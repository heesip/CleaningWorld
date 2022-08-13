using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageUI : Singleton<GarbageUI>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GarbageUIBox baseUIBox;

    private void Awake()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)),
                                    (baseUIBox, nameof(baseUIBox)));

        canvasGroup.alpha = 0;
    }
}

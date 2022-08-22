using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarbageCount : MonoBehaviour
{
    CanvasGroup canvasGroup;
    TextMeshProUGUI count;

    public void Initialize()
    {
        WoonyMethods.Assert(this,(canvasGroup, nameof(canvasGroup)),
                            (count, nameof(count)));
        canvasGroup.alpha = 0;
    }

    private void Disappear()
    {
        canvasGroup.alpha = 0;
    }

    public void UpdateAmount(int amount)
    {
        if(amount <= 0)
        {
            Disappear();
            return;
        }

        count.text = $"x{amount}";
    }
}

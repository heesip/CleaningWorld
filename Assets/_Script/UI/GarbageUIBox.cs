using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarbageUIBox : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI amount;

    public void Initialze(Sprite icon)
    {
        WoonyMethods.Assert(this, (icon, nameof(icon)),
                                    (image, nameof(image)),
                                    (amount, nameof(amount)));
        this.image.sprite = icon;

    }

    public void UpdateAmount(int value)
    {
        amount.text = value.ToString();

        if (value == 0)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

}

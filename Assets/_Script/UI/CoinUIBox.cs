using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amount;

    public void UpdateAmount(int value)
    {
        amount.text = value.ToString();
        if(value == 0)
        {
            gameObject.SetActive(false);
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }
}

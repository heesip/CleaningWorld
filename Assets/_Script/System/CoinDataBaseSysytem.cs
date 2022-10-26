using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDataBaseSysytem
{
    static readonly string COIN_KEY = "CoinAmount";

    public void GetCoinData(ref int coin)
    {
        if (PlayerPrefs.HasKey(COIN_KEY))
        {
            coin = PlayerPrefs.GetInt(COIN_KEY, coin);
        }
    }

    public void SetCoinData(int coin)
    {
        PlayerPrefs.SetInt(COIN_KEY, coin);
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinSystem
{
    public int coin;

    public void AddCoin(int value)
    {
        if (value < 0)
        {
            Debug.Log("AddCoin() : 음수가 들어왔습니다.");
            value = Math.Abs(value);
        }

        CoinUpdateCount(value);
    }

    public void SubCoin(int value)
    {
        if (value < 0)
        {
            Debug.Log("SubCoin() : 음수가 들어왔습니다.");
            value = Math.Abs(value);
        }
        CoinUpdateCount(-value);
    }

    void CoinUpdateCount(int value)
    {
        coin += value;
        UIManager.Instance.UpdateCoinAmount(coin);
    }

    public bool IsSubable(int value)
    {
        return coin >= value;
    }
}

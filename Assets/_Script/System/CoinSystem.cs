using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinSystem
{
    static readonly string LOAD_KEY = "CoinAmout";
    int coin;

    public void Initialize()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(LOAD_KEY))
        {
            coin = PlayerPrefs.GetInt(LOAD_KEY, 0);
        }

        UIManager.Instance.UpdateCoinAmount(coin);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(LOAD_KEY, coin);
    }

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
        SaveData()  ;
    }
   
    public bool IsSubable(int value)
    {
        return coin >= value;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerGarbagesDataBaseSystem
{
    static readonly string GARBAGES_KEY = "PlayerGarbages";

    public void Initialize()
    {

    }

    public string GetData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        return key;
    }

    public void SetData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

}



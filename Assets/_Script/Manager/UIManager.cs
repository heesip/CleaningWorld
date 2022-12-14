using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Joystick Joystick => joystick;

    [SerializeField] Joystick joystick;

    public void Initialize() { }

    public void UpdateGarbageAmount(GarbageType garbageType, int value)
    {
        GarbageUI.Instance.UpdateAmount(garbageType, value);
    }

    public void UpdateCoinAmount(int value)
    {
        CoinUI.Instance.UpdateAmount(value);        
    }

    public void ShowUpgradeUI()
    {
        UpgradeUI.Instance.Show();
    }
    public void CloseUpgradeUI()
    {
        UpgradeUI.Instance.Close();
    }
}

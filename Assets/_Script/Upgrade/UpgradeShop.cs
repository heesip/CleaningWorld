using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] UpgradeShopPlayerDetector UpgradeShopPlayerDetector;

    private void Start()
    {
        WoonyMethods.Assert(this, (UpgradeShopPlayerDetector, nameof(UpgradeShopPlayerDetector)));
        UpgradeShopPlayerDetector.Initialize(() => UIManager.Instance.ShowUpgradeUI(),
                                             () => UIManager.Instance.CloseUpgradeUI());
    }
}
    
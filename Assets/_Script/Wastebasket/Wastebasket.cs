using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    private void Start()
    {
        WoonyMethods.Assert(this, (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExist);
    }

    void OnPlayerEnter() 
    {
        if(Player.Instance.IsAbleToPopGarbage(garbageType) == false)
        {
            return;
        }
        Player.Instance.OnWastebasket(garbageType);
    }

    void OnPlayerExist()
    {

    }
}

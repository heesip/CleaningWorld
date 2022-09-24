using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PrefabResourcesInfoSO), menuName = "CleaningWorld/Create PrefabResourcesInfoSO")]
public class PrefabResourcesInfoSO : ScriptableObject
{
    [SerializeField] GarbageObject canPrefab1;
    [SerializeField] GarbageObject canPrefab2;
    [SerializeField] GarbageObject foodPrefab1;
    [SerializeField] GarbageObject foodPrefab2;
    [SerializeField] GarbageObject glassPrefab1;
    [SerializeField] GarbageObject glassPrefab2;
    [SerializeField] GarbageObject paperPrefab1;
    [SerializeField] GarbageObject paperPrefab2;
    [SerializeField] GarbageObject plasticPrefab1;
    [SerializeField] GarbageObject plasticPrefab2;
    [SerializeField] Coin coinPrefab;

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        switch (garbageType)
        {
            case GarbageDetailType.Can1:
                return canPrefab1;
            case GarbageDetailType.Can2:
                return canPrefab2;
            case GarbageDetailType.Food1:
                return foodPrefab1;
            case GarbageDetailType.Food2:
                return foodPrefab2;
            case GarbageDetailType.Glass1:
                return glassPrefab1;
            case GarbageDetailType.Glass2:
                return glassPrefab2;
            case GarbageDetailType.Paper1:
                return paperPrefab1;
            case GarbageDetailType.Paper2:
                return paperPrefab2;
            case GarbageDetailType.Plastic1:
                return plasticPrefab1;
            case GarbageDetailType.Plastic2:
                return plasticPrefab2;
            default:
                Debug.Log($"PrefabeResourcesInfoSO : 이게 호출되면 안됨!, garbageType = {garbageType}");
                return null;
        }
    }

    public Coin GetCoinPrefab()
    {
        return coinPrefab;
    }
}

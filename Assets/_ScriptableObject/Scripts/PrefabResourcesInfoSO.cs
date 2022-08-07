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

    public GarbageObject GetGarbageObjectPrefab(GarbageType garbageType)
    {
        switch (garbageType)
        {
            case GarbageType.Can1:
                return canPrefab1;
            case GarbageType.Can2:
                return canPrefab2;
            case GarbageType.Food1:
                return foodPrefab1;
            case GarbageType.Food2:
                return foodPrefab2;
            case GarbageType.Glass1:
                return glassPrefab1;
            case GarbageType.Glass2:
                return glassPrefab2;
            case GarbageType.Paper1:
                return paperPrefab1;
            case GarbageType.Paper2:
                return paperPrefab2;
            case GarbageType.Plastic1:
                return plasticPrefab1;
            case GarbageType.Plastic2:
                return plasticPrefab2;
            default:
                Debug.Log($"PrefabeResourcesInfoSO : 이게 호출되면 안됨!, garbageType = {garbageType}")
                return null;
        }
    }
}

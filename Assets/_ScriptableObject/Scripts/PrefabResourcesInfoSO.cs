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


}

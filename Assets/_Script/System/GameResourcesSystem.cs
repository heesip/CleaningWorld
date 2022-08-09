using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesSystem
{
    PrefabResourcesInfoSO prefabResourcesInfoSO;

    public void Initialize()
    {
        prefabResourcesInfoSO = Resources.Load<PrefabResourcesInfoSO>(nameof(PrefabResourcesInfoSO));
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageType garbageType) => prefabResourcesInfoSO.GetGarbageObjectPrefab(garbageType);
}

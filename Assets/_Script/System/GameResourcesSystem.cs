using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesSystem
{
    PrefabResourcesInfoSO prefabResourcesInfoSO;
    ImageResourcesInfoSO imageResourcesInfoSO;


    public void Initialize()
    {
        prefabResourcesInfoSO = Resources.Load<PrefabResourcesInfoSO>(nameof(PrefabResourcesInfoSO));
        imageResourcesInfoSO = Resources.Load<ImageResourcesInfoSO>(nameof(ImageResourcesInfoSO));

    }

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType) 
    {
        return prefabResourcesInfoSO.GetGarbageObjectPrefab(garbageType);
    }  

    public Sprite GetIcon(IconType icontype)
    {
        return imageResourcesInfoSO.GetIcon(icontype);
    }

    public Coin GetCoinObjectPrefab()
    {
        return prefabResourcesInfoSO.GetCoinPrefab();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesManager : Singleton<GameResourcesManager>
{
    GameResourcesSystem gameResourcesSystem = new GameResourcesSystem();

    public void Initialize()
    {
        gameResourcesSystem.Initialize();
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageDetailType) 
    {
        return gameResourcesSystem.GetGarbageObjectPrefab(garbageDetailType);
    }  

    public Sprite GetIcon(IconType icontype)
    {
        return gameResourcesSystem.GetIcon(icontype);
    }


}

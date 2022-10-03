using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();
    [SerializeField] PlayerGarbageStackSystem playerGarbageStackSystem = new PlayerGarbageStackSystem();
    [SerializeField] CoinSystem coinSystem = new CoinSystem();
    private void Start()
    {
        playerMoveSystem.Initialize(this);
        playerGarbageStackSystem.Initialize(this);
    }

    private void FixedUpdate()
    {
        playerMoveSystem.Move();
    }

    public bool IsAbleToGetGarbage()
    {
        return playerGarbageStackSystem.IsAbleToGetGarbage();
    }

    public GarbageObject OnWastebasket(GarbageType garbageType)
    {
        return playerGarbageStackSystem.OnWastebasket(garbageType);
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return playerGarbageStackSystem.IsAbleToPopGarbage(garbageType);
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float delay)
    {
        playerGarbageStackSystem.OnGarbageHeap(garbageObject, delay);
    }

    public void AddCoin(int value)
    {
        coinSystem.AddCoin(value);
    }
    public void SubCoin(int value)
    {
        coinSystem.SubCoin(value);
    }

    public bool IsSubable(int value)
    {
        return coinSystem.IsSubable(value);
    }
}

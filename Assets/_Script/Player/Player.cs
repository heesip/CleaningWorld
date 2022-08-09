using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();
    [SerializeField] PlayerGarbageStackSystem playerGarbageStackSystem = new PlayerGarbageStackSystem();

    private void Start()
    {
        playerMoveSystem.Initalize(this);
        playerGarbageStackSystem.Initialize(this);
    }

    private void FixedUpdate()
    {
        playerMoveSystem.Move();
    }
}

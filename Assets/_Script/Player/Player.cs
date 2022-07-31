﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();

    private void Start()
    {
        playerMoveSystem.Initalize(this);
    }
    private void FixedUpdate()
    {
        playerMoveSystem.Move();
    }
}
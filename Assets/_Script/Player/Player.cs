using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerMoveSystem.Initalize(this);
    }

    private void FixedUpdate()
    {
        playerMoveSystem.Move();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}

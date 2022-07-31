using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] Transform playerFollower;

    void Start()
    {
        playerFollower.SetParent(Player.Instance.transform);
    }
}

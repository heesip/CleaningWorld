using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageHeapPlayerDetector : PlayerDetector
{
    Action onPlayerEnter;
    Action onPlayerExit;

    protected override void OnStart() { }

    public void Initialize(Action onPlayerEnter, Action onPlayerExit)
    {
        this.onPlayerEnter = onPlayerEnter;
        this.onPlayerExit = onPlayerExit;
    }

    protected override void _onTriggerEnter(Collider other)
    {
        onPlayerEnter();
    }

    protected override void _onTriggerExit(Collider other)
    {
        onPlayerExit();
    }
}

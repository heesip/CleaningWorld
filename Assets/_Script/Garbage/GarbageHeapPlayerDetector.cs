﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeapPlayerDetector : PlayerDetector
{
    int garbageTypesMaxNumber;

    protected override void OnStart()
    {
        var garbageTypes = Enum.GetNames(typeof(GarbageType));
        garbageTypesMaxNumber = garbageTypes.Length;
    }

    protected override void _onTriggerEnter()
    {
        GarbageType randomType = (GarbageType)Random.Range(1, garbageTypesMaxNumber);

        FactoryManager.Instance.GetGarbageObject(randomType, transform.position);
    }
    

    protected override void _onTriggerExit() { }

}
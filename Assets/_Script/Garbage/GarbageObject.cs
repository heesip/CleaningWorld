using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObject : RecycleObject
{
    public GarbageDetailType GarbageDetailType => garbageDetailType;
    public GarbageType GarbageType => garbageType;

    [SerializeField] GarbageType garbageType;
    [SerializeField] GarbageDetailType garbageDetailType;
    
}

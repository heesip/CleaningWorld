using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageHeapPlayerDetector : PlayerDetector
{
    protected override void _onTriggerEnter()
    {
        Debug.Log("미치겠다~"); 
    }

    protected override void _onTriggerExit() { }

}

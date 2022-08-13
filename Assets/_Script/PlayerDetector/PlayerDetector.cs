using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDetector : MonoBehaviour
{
    [SerializeField]Collider mainCollider;

    private void Start()
    {
        Debug.Assert(mainCollider != null, $"mainCollider is null", transform);
        OnStart();
    }

    protected virtual void OnStart() { }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(AllTagAndLayer.Player) == false)
        {
            return; 
        }
        _onTriggerEnter(other); 
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(AllTagAndLayer.Player) == false)
        {
            return;
        }
        _onTriggerExit(other);
    }

    protected abstract void _onTriggerEnter(Collider other);

    protected abstract void _onTriggerExit(Collider other);
    
}

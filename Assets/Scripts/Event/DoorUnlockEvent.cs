using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockEvent : MonoBehaviour
{
    public event EventHandler OnDoorUnlock;
    
    void OnTriggerEnter(Collider collider)
    {
        // Invoke event OnDoorUnlock as Publisher
        OnDoorUnlock?.Invoke(this, EventArgs.Empty);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickedEvent : MonoBehaviour
{
    public event EventHandler OnKeyPickedUp;
    
    void OnTriggerEnter(Collider collider)
    {
        // Invoke event OnKeyPickedUp as Publisher
        OnKeyPickedUp?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}

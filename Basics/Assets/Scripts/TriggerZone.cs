using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerZone : MonoBehaviour
{
    //Classic delegate exemple
    public delegate void OnTriggerDelg();

    public OnTriggerDelg onEnterEvent;

    //Exemple with Action
    public Action<Rigidbody> onTriggerEnter;

    private void Awake()
    {
        onEnterEvent += Sample;

        onTriggerEnter += Sample;
    }

    public void Sample()
    {
        Debug.Log("Enter the trigger zone");
    }

    public void Sample(Rigidbody rb)
    {
        Debug.Log($"Hello {rb.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        onEnterEvent.Invoke();
        onTriggerEnter.Invoke(other.attachedRigidbody);
    }
}

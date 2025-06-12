using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    public event System.Action<Collider2D> TriggerEnter;
    public event System.Action<Collider2D> TriggerExit;
    public event System.Action<Collider2D> TriggerStay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit?.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerStay?.Invoke(collision);
    }
}

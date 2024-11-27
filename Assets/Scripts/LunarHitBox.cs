using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LunarHitBox : MonoBehaviour
{
    public UnityEvent OnHitEvent;
    public UnityEvent ExitHitEvent;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            OnHitEvent?.Invoke();
            Debug.Log("Hit");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            ExitHitEvent?.Invoke();
            Debug.Log("Exit");
        }
    }
}

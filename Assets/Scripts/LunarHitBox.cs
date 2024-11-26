using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LunarHitBox : MonoBehaviour
{
    public UnityEvent OnHitEvent;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            OnHitEvent?.Invoke();
            Debug.Log("123123132132");
        }
    }
}

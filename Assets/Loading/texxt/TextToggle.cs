using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextToggle : MonoBehaviour
{
    public KeyCode toggleKey;
    public UnityEvent ToggleEvent = new UnityEvent();
    void Update()
    {
        if(Input.GetKeyDown(toggleKey))
        {
            ToggleEvent?.Invoke();
        }
    }
}

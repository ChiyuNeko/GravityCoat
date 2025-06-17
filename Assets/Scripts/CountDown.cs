using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public int CountdownTime;
    float StartTime;
    public TextMeshProUGUI time;
    public bool StartCounting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Three)))
        {
            StartTime = Time.time;
            StartCounting = true;
        }
        if (StartCounting)
        {
            TimeCountDown();
        }
    }
    public void TimeCountDown()
    {
        float a = CountdownTime - Time.time + StartTime;
        if (a < 0)
        {
            a = 0;
        }
        time.text = $"{(int)a}";

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fofulab;

public class WaterController : MonoBehaviour
{
    public HapticMaker hm;
    private bool hasSetInitialValues = false;
    private bool hasSet90sValues = false;
    private bool hasSet60sMotor = false;
    private bool hasSet70sMotor = false;
    private bool hasSet90sMotor = false;
    private bool hasSet110sMotor = false;
    private bool hasSet140sMotor = false;
    private bool hasSet170sMotor = false;
    private bool hasSet200sMotor = false;
    private float StartTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        // hm.SetPWM(OutputPin.P7, 250); //右手臂
        // hm.SetPWM(OutputPin.P8, 250); //左手臂
        // hm.SetPWM(OutputPin.P9, 250); //水袋灌水 ; 水袋吸水是P11
        // Debug.Log("Open motor");
        StartTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        float time = Time.timeSinceLevelLoad - StartTime;
        if (!hasSetInitialValues)
        {
            hm.SetPWM(OutputPin.P7, 250); 
            hm.SetPWM(OutputPin.P9, 250); 
            hasSetInitialValues = true;
        }

        // 10.6秒時水袋馬達停止
        if (time >= 10.6 && !hasSet60sMotor)
        {
            Debug.Log("水袋停止");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet60sMotor = true;
        }

        // 60.6秒時全部開始
        if (time >= 50.6f && !hasSet70sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P7, 250);
            hm.SetPWM(OutputPin.P9, 250);
            hasSet70sMotor = true;
        }

        // 120.8秒時停止
        if (time >= 90.8f && !hasSet90sMotor)
        {
            Debug.Log("Open mototr");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet90sMotor = true;
        }

        // 170.8秒時灌水
        if (time >= 130.8f && !hasSet110sMotor)
        {
            hm.SetPWM(OutputPin.P8, 250);
            hm.SetPWM(OutputPin.P11, 250);
            hasSet110sMotor = true;
        }

        // 213.3秒時停止洩水
        if (time >= 163.3f && !hasSet140sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P11, 0);
            hm.SetPWM(OutputPin.P8, 0);
            hasSet140sMotor = true;
        }


        // if (time >= 145 && !hasSet140sMotor)
        // {
        //     Debug.Log("F水袋停止吸水");
        //     hm.SetPWM(OutputPin.P11, 0);
        //     hasSet140sMotor = true;
        // }
    }
}

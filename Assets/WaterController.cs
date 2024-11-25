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

    // Start is called before the first frame update
    void Start()
    {
        // hm.SetPWM(OutputPin.P7, 250); //右手臂
        // hm.SetPWM(OutputPin.P8, 250); //左手臂
        // hm.SetPWM(OutputPin.P9, 250); //水袋灌水 ; 水袋吸水是P11
        // Debug.Log("Open motor");
    }

    void Update()
    {
        float time = Time.timeSinceLevelLoad;
        if (!hasSetInitialValues)
        {
            hm.SetPWM(OutputPin.P7, 250); 
            hm.SetPWM(OutputPin.P9, 250); 
            hasSetInitialValues = true;
        }

        // 26秒時水袋馬達停止
        if (time >= 26 && !hasSet60sMotor)
        {
            Debug.Log("水袋停止");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet60sMotor = true;
        }

        // 36秒時全部開始
        if (time >= 36 && !hasSet70sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P7, 250);
            hm.SetPWM(OutputPin.P9, 250);
            hasSet70sMotor = true;
        }

        // 102秒時停止
        if (time >= 102 && !hasSet90sMotor)
        {
            Debug.Log("Open mototr");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet90sMotor = true;
        }

        // 112秒時灌水
        if (time >= 112 && !hasSet90sValues)
        {
            hm.SetPWM(OutputPin.P8, 250);
            hm.SetPWM(OutputPin.P11, 250);
            hasSet90sValues = true;
        }

        // 167秒時停止洩水
        if (time >= 167 && !hasSet110sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P11, 0);
            hm.SetPWM(OutputPin.P8, 0);
            hasSet110sMotor = true;
        }

        // 177秒時洩水
        if (time >= 177 && !hasSet140sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P11, 250);
            hm.SetPWM(OutputPin.P8, 250);
            hasSet140sMotor = true;
        }

        // 237秒時w停止洩水
        if (time >= 237 && !hasSet170sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P11, 0);
            hm.SetPWM(OutputPin.P8, 0);
            hasSet170sMotor = true;
        }

        // if (time >= 145 && !hasSet140sMotor)
        // {
        //     Debug.Log("F水袋停止吸水");
        //     hm.SetPWM(OutputPin.P11, 0);
        //     hasSet140sMotor = true;
        // }
    }
}

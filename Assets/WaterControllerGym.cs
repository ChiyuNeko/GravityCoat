using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fofulab;

public class WaterControllerGym : MonoBehaviour
{
    public HapticMaker hm;
    private bool hasSetInitialValues = false;
    private bool hasSet90sValues = false;
    private bool hasSet60sMotor = false;
    private bool hasSet70sMotor = false;
    private bool hasSet90sMotor = false;
    private bool hasSet110sMotor = false;
    private bool hasSet140sMotor = false;
    private bool hasSet239sMotor = false;
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
        float time = Time.timeSinceLevelLoad  - StartTime;
        if (!hasSetInitialValues)
        {
            hm.SetPWM(OutputPin.P8, 250); 
            hm.SetPWM(OutputPin.P11, 250); 
            hasSetInitialValues = true;
        }

        // 7秒時抽水馬達停止
        if (time >= 7 && !hasSet60sMotor)
        {
            Debug.Log("水袋停止");
            hm.SetPWM(OutputPin.P8, 0);
            hm.SetPWM(OutputPin.P11, 0);
            hasSet60sMotor = true;
        }

        // 52秒時灌水開始
        if (time >= 52 && !hasSet70sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P7, 250);
            hm.SetPWM(OutputPin.P9, 250);
            hasSet70sMotor = true;
        }

        // 73秒時灌水停止
        if (time >= 73 && !hasSet90sMotor)
        {
            Debug.Log("Open mototr");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet90sMotor = true;
        }

        // 88秒時灌水
        if (time >= 88 && !hasSet90sValues)
        {
            hm.SetPWM(OutputPin.P7, 250);
            hm.SetPWM(OutputPin.P9, 250);
            hasSet90sValues = true;
        }

        // 109秒時停止灌水
        if (time >= 109 && !hasSet110sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet110sMotor = true;
        }


    }
}

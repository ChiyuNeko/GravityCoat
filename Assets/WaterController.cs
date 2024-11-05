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

    // Start is called before the first frame update
    void Start()
    {
        hm.SetPWM(OutputPin.P4, 255); //���
        hm.SetPWM(OutputPin.P5, 0); //���
        hm.SetPWM(OutputPin.P7, 250); //�k���u
        //hm.SetPWM(OutputPin.P8, 250); //�����u
        //hm.SetPWM(OutputPin.P9, 250); //���U��� ; ���U�l���OP11
        Debug.Log("Open motor");
    }

   /* void Update()
    {
        float time = Time.timeSinceLevelLoad;
        if (!hasSetInitialValues)
        {
            hm.SetPWM(OutputPin.P4, 0); // �����
            hm.SetPWM(OutputPin.P5, 0);
            hasSetInitialValues = true;
        }
        if (time >= 60 && !hasSet60sMotor)
        {
            Debug.Log("Close mototr Bag.");
            hm.SetPWM(OutputPin.P9, 0);
            hasSet60sMotor = true;
        }
        // 70��ɥ�������
        if (time >= 70 && !hasSet70sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P7, 0);
            hm.SetPWM(OutputPin.P8, 0);
            hm.SetPWM(OutputPin.P9, 0);
            hasSet70sMotor = true;
        }

        // 90��ɡA���U�l���A���u����
        if (time >= 90 && !hasSet90sMotor)
        {
            Debug.Log("Open mototr");
            hm.SetPWM(OutputPin.P7, 250);
            hm.SetPWM(OutputPin.P8, 250);
            hm.SetPWM(OutputPin.P11, 250);
            hasSet90sMotor = true;
        }

        // 90��ɮ�o�}�A���u����
        if (time >= 90 && !hasSet90sValues)
        {
            hm.SetPWM(OutputPin.P4, 255);
            hm.SetPWM(OutputPin.P5, 255);
            hasSet90sValues = true;
        }

        // 110��ɮ�o�}�A���u����A���U�~��l��
        if (time >= 110 && !hasSet110sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P11, 250);
            hm.SetPWM(OutputPin.P8, 0);
            hm.SetPWM(OutputPin.P7, 0);
            hasSet110sMotor = true;
        }

        if (time >= 145 && !hasSet140sMotor)
        {
            Debug.Log("Close mototr");
            hm.SetPWM(OutputPin.P11, 0);
            hasSet140sMotor = true;
        }
    }*/
}

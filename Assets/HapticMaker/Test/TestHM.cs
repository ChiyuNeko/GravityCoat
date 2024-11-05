using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fofulab;

public class TestHM : MonoBehaviour
{
    public HapticMaker hm;

    [Range(0, 255)]
    public uint pwmValue;
    [Range(0, 255)]
    public uint motorValue;

    [Range(0, 180)]
    public uint servoValue;

    // Start is called before the first frame update
    void Start()
    {
        // use set servo to set the output pin connect to servo
        // only need to call once
        hm.SetServo(OutputPin.P4, true);
    }

    // Update is called once per frame
    void Update()
    {
        // use set pwm to set servo angle
        hm.SetPWM(OutputPin.P4, servoValue);

        // use set pwm to set pwm value
        hm.SetPWM(OutputPin.P2, pwmValue);
        // use set motor to set motor value
        hm.SetMotor(MotorPin.P10, motorValue);
    }
}

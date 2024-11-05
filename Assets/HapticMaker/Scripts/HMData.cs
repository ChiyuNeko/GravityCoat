using System;
using System.Collections.Generic;
using UnityEngine;

namespace fofulab
{
    [Serializable]
    public class HMData {
        public string name;
        public bool[] isservo;
        public uint[] pwm;
        public uint[] motor;

        public HMData() {
            isservo = new bool[8];
            pwm = new uint[8];
            motor = new uint[4];
        }

        public void reset() {
            for (int i = 0; i < 8; i++) {
                pwm[i] = 0;
            }
            for (int i = 0; i < 4; i++) {
                motor[i] = 0;
            }
        }
    }

    [Serializable]
    public class PinData {
        public bool isservo;
        [Range(0, 255)]
        public uint pwm;
    }

    [Serializable]
    public class MotorData
    {
        [Range(0, 255)]
        public uint pwm;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.VFX;
using Unity.Collections;

namespace fofulab{
    public class HapticMaker : MonoBehaviour
    {

        [SerializeField]
        private string hmName;
        
        [SerializeField]
        private HMData data = new HMData();
        public HMData Data { get { return data; } }
        
        [Header("Inputs")]
        [MyReadOnly, SerializeField]
        private uint inputP15 = 0;
        [MyReadOnly, SerializeField]
        private uint inputP16 = 0;

        private HapticMakerManager _mgr;

        // Start is called before the first frame update
        void Start()
        {
            data.name = hmName;
            _mgr = HapticMakerManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            checkSize();
        }

        void checkSize() {
            if (data == null)
                return;

            if (data.isservo.Length != 8) {
                Debug.LogWarning("Don't change the 'outputs' field's array size!");
                Array.Resize(ref data.isservo, 8);
            }

            if (data.pwm.Length != 8) {
                Debug.LogWarning("Don't change the 'outputs' field's array size!");
                Array.Resize(ref data.pwm, 8);
            }

            if (data.motor.Length != 4) {
                Debug.LogWarning("Don't change the 'motors' field's array size!");
                Array.Resize(ref data.motor, 4);
            }
        }

        public string GetName() {
            return hmName;
        }
        public void SetServo(OutputPin pin, bool isservo) {
            _setServo((uint)pin, isservo);
        }
        private void _setServo(uint pin, bool isservo) {
            data.isservo[pin] = isservo;
        }

        public void SetPWM(OutputPin pin, uint val) {
            _setPWM((uint)pin, val);
        }

        private void _setPWM(uint pin, uint val) {
            data.pwm[pin] = val;
        }

        public void SetMotor(MotorPin pin, uint val) {
            _setMotor((uint)pin, val);
        }
        private void _setMotor(uint pin, uint val) {
            data.motor[pin] = val;
        }

        public uint GetInput(InputPin pin) {
            return _getInput((uint)pin);
        }

        private uint _getInput(uint pin) {
            switch (pin) {
                case 0:
                    return inputP15;
                case 1:
                    return inputP16;
                default:
                    Debug.Log("get input pin out of range.");
                    return 0;
            }
        }

        public void updateInput(UInt32 v) {
            inputP15 = (uint)(v & 0xFF);
            inputP16 = (uint)((v >> 8) & 0xFF);
        }

        public void Reset() {
            data.reset();
        }

#if UNITY_EDITOR
        private void OnValidate() {
            checkSize();
        }
#endif
    }
}
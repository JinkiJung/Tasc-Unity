using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class InputOTouch : MonoBehaviour
    {
        private Quaternion prevControllerRotation;
        //public Vector3 veloDirection;
        private float velocityMagnitute;

        public bool isGrabing
        {
            get; set;
        }

        public Valve.VR.InteractionSystem.GrabTypes grabType
        {
            get; set;
        }

        public float GetSwingMagnitude()
        {
            return (isGrabing) ? velocityMagnitute : 0;
        }

        private void Start()
        {
            isGrabing = false;
        }

        private void Update()
        {
            float rotDiff = Quaternion.Angle(this.transform.localRotation, prevControllerRotation);
            prevControllerRotation = this.transform.localRotation;
            velocityMagnitute = rotDiff * rotDiff * 0.05f;
        }
    }
}

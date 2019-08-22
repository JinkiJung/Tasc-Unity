using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;

namespace Tasc
{
    public class PlatformSelector : MonoBehaviour
    {
        public static Tasc.Platform chosenPlatform;

        public static PlatformSelector platformSelector = null;
        public Tasc.Platform currentPlatform = Platform.Desktop;
        public static Tasc.Platform GetCurrentPlatform() { return chosenPlatform; }

        private void OnValidate()
        {
            PlatformSelector.chosenPlatform = currentPlatform;
            if(chosenPlatform == Platform.Desktop){
                Debug.Log("VR setting is off.");
                XRSettings.enabled = false;
                transform.Find("Player").gameObject.SetActive(false);
                transform.Find("RigidBodyFPSController").gameObject.SetActive(true);
            }
            else{
                Debug.Log("VR setting is on.");
                XRSettings.enabled = true;
                transform.Find("Player").gameObject.SetActive(true);
                transform.Find("RigidBodyFPSController").gameObject.SetActive(false);
            }
        }
    }

    
}

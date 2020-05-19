using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;

namespace TascUnity
{
    public class PlatformSelector : MonoBehaviour
    {
        public static Platform chosenPlatform;

        public static PlatformSelector platformSelector = null;
        public Platform currentPlatform = Platform.Desktop;
        public static Platform GetCurrentPlatform() { return chosenPlatform; }
        private ScenarioController scenarioController;

        private void OnValidate()
        {
            PlatformSelector.chosenPlatform = currentPlatform;

            if (FindObjectsOfType<ScenarioController>() != null && FindObjectsOfType<ScenarioController>()[0] != null)
            {
                scenarioController = FindObjectsOfType<ScenarioController>()[0];

                if (chosenPlatform == Platform.Desktop)
                {
                    Debug.Log("VR setting is off.");
                    XRSettings.enabled = false;
                    transform.Find("Player").gameObject.SetActive(false);
                    transform.Find("RigidBodyFPSController").gameObject.SetActive(true);
                    scenarioController.actor = transform.Find("RigidBodyFPSController").gameObject.GetComponent<DesktopActor>();
                }
                else
                {
                    Debug.Log("VR setting is on.");
                    XRSettings.enabled = true;
                    transform.Find("Player").gameObject.SetActive(true);
                    transform.Find("RigidBodyFPSController").gameObject.SetActive(false);
                    scenarioController.actor = transform.Find("Player").gameObject.GetComponent<OculusActor>();
                }       
            }            
        }
    }

    
}

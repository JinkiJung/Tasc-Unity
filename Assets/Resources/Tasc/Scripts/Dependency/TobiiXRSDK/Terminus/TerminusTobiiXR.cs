using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Tobii.XR;

namespace TascUnity
{
    public class TerminusTobiiXR : Terminus
    {

        void Start()
        {
            var settings = new TobiiXR_Settings();
            TobiiXR.Start(settings);
        }

        // Update is called once per frame
        void Update()
        {
            var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

            if (eyeTrackingData.GazeRay.IsValid)
            {
                var rayOrigin = eyeTrackingData.GazeRay.Origin;
                SingleConditionPublisher.Instance.Send(new VectorVariableState(this, "GazeOrigin", rayOrigin));
                var rayDirection = eyeTrackingData.GazeRay.Direction;
                Quaternion rayDirectionQuat = Quaternion.Euler(rayDirection);
                SingleConditionPublisher.Instance.Send(new QuaternionVariableState(this, "GazeDirection", rayDirectionQuat));
            }
        }
    }
}

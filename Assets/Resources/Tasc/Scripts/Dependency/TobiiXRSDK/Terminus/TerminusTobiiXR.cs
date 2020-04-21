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
            
        }

        // Update is called once per frame
        void Update()
        {
            var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

            if (eyeTrackingData.GazeRay.IsValid)
            {
                var rayOrigin = eyeTrackingData.GazeRay.Origin;
                ConditionPublisher.Instance.Send(new VectorVariableState(this, "GazeOrigin", rayOrigin));
                var rayDirection = eyeTrackingData.GazeRay.Direction;
                ConditionPublisher.Instance.Send(new VectorVariableState(this, "GazeDirection", rayDirection));
                //Quaternion rayDirectionQuat = Quaternion.Euler(rayDirection);
                //ConditionPublisher.Instance.Send(new QuaternionVariableState(this, "GazeDirection", rayDirectionQuat));
            }
        }
    }
}

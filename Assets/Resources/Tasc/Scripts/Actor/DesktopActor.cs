using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class DesktopActor : Actor
    {
        protected override void Update()
        {
            base.Update();
            HandleKeyInput();
        }

        // Update is called once per frame
        void HandleKeyInput()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Z))
            {
                //SingleCondition.Instance.Evaluate(new InputUpState(this, (int)UnityEngine.KeyCode.A));
                SingleConditionPublisher.Instance.Send(new InputDownState(this, (int)UnityEngine.KeyCode.Z));
            }
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.X))
            {
                SingleConditionPublisher.Instance.Send(new InputDownState(this, (int)UnityEngine.KeyCode.X));
            }
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.C))
            {
                SingleConditionPublisher.Instance.Send(new InputDownState(this, (int)UnityEngine.KeyCode.C));
            }
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.Z))
            {
                //SingleCondition.Instance.Evaluate(new InputUpState(this, (int)UnityEngine.KeyCode.A));
                SingleConditionPublisher.Instance.Send(new InputUpState(this, (int)UnityEngine.KeyCode.Z));
            }
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.X))
            {
                SingleConditionPublisher.Instance.Send(new InputUpState(this, (int)UnityEngine.KeyCode.X));
            }
            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.C))
            {
                SingleConditionPublisher.Instance.Send(new InputUpState(this, (int)UnityEngine.KeyCode.C));
            }
        }
    }
}
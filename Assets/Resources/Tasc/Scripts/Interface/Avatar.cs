using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Avatar : VisualInterface
    {
        public bool isVisible;
        ModelPoser model;
        private int currentModelPose = -1;

        public new void Start()
        {
            modality = Information.Modality.AvatarPose;
        }

        public void SetModel(ModelPoser _model)
        {
            model = _model;
        }

        public override void SetVisibility(bool visible)
        {
            isVisible = visible;
            if (isVisible)
                model.show();
            else
                model.hide();
        }

        public override void Send(Information information)
        {
            TakePose(information.GetContent(Information.Modality.AvatarPose));
        }

        protected virtual void TakePose(string poseName)
        {
            if (poseName != null)
            {
                if (poseName.Contains("forward bend pose"))
                    SetModelPose(0);
                else if (poseName.Contains("triangle pose"))
                    SetModelPose(1);
                else if (poseName.Contains("side bend stretch"))
                    SetModelPose(2);
                else if (poseName.Contains("mountain pose"))
                    SetModelPose(3);
                else if (poseName.Contains("neck relaxing pose"))
                    SetModelPose(4);
                else
                    SetModelPose(-1);
            }
            else
            {
                SetModelPose(-1);
            }
        }

        protected void SetModelPose(int index)
        {
            currentModelPose = index;
            if (index >= 0)
                model.takePose(index);
            else
                model.hide();
        }
    }
}

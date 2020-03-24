using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Annotation: Instruction
    {
        ModelPoser model;
        bool isVisible;

        public Annotation(List<Interface> givenInterfaces): base(givenInterfaces)
        {
        
        }

        public Annotation(string name, List<Interface> givenInterfaces) : base(name, givenInterfaces)
        {

        }

        public Annotation(Annotation another): base(another)
        {
            model = another.model;
            isVisible = another.isVisible;
        }

        public void SetModel(ModelPoser _model)
        {
            model = _model;
        }

        public void SetVisibility(bool visible)
        {
            isVisible = visible;
            if (isVisible)
                model.show();
            else
                model.hide();
            for (int i = 0; i < interfaces.Count; i++)
            {
                interfaces[i].Activate();
            }
        }

        public override void Proceed(bool isAudioEnabled = true)
        {
            if (!isVisible)
                SetVisibility(true);
            
            for(int i=0; i<interfaces.Count; i++)
            {
                if (library.GetInfo("title").content.Contains("forward bend pose"))
                    model.takePose(0);
                else if (library.GetInfo("title").content.Contains("triangle pose"))
                    model.takePose(1);
                else if (library.GetInfo("title").content.Contains("side bend stretch"))
                    model.takePose(2);
                else if (library.GetInfo("title").content.Contains("mountain pose"))
                    model.takePose(3);
                else if (library.GetInfo("title").content.Contains("neck relaxing pose"))
                    model.takePose(4);
                else
                {
                    model.hide();
                }
                if(interfaces[i] is VisualInterface)
                {
                    if (interfaces[i].transform.name == "LeftHandGuide")
                        (interfaces[i] as VisualInterface).SetPose(model.getLeftHandPos(), Quaternion.identity);
                    else if (interfaces[i].transform.name == "RightHandGuide")
                        (interfaces[i] as VisualInterface).SetPose(model.getRightHandPos(), Quaternion.identity);
                    else if (interfaces[i].transform.name == "HeadGuide")
                        (interfaces[i] as VisualInterface).SetPose(model.getHeadPos(), Quaternion.identity);
                }
            }
        }

        public override void WrapUp()
        {
            SetVisibility(false);
        }
    }
}

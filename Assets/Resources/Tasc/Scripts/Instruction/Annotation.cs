using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Annotation: Instruction
    {
        //public Instruction AnnotationSupport;
        //public Avatar demoAvatar;

        public Annotation(string _name, Interface _interface, Information _information): base(_name, _interface, _information)
        {
        
        }

        /*
        public override void Proceed()
        {
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                string poseName = entry.Value.GetContent(Information.Modality.AvatarPose);
                if (poseName != null)
                {
                    TakePose(poseName);
                }


            }
        }

        


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// AnnotationSupport : Need to be reimplemented
        /*
        if(interfaces[i] is VisualInterface)
        {
            if (interfaces[i].transform.name == "LeftHandGuide")
                (interfaces[i] as VisualInterface).SetPose(model.getLeftHandPos(), Quaternion.identity);
            else if (interfaces[i].transform.name == "RightHandGuide")
                (interfaces[i] as VisualInterface).SetPose(model.getRightHandPos(), Quaternion.identity);
            else if (interfaces[i].transform.name == "HeadGuide")
                (interfaces[i] as VisualInterface).SetPose(model.getHeadPos(), Quaternion.identity);
        }
        */
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

}

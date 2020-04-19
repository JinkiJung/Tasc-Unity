using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace TascUnity
{
    public class GuideBall:TerminusSteamVR
    {
        public string type;
        public Transform target;
        bool isHovering = false;
        float hoveringRange = 0.3f;

        private void Start()
        {
            isHovering = false;
        }

        protected override void OnHandHoverEnd(Hand hand)
        {
            isHovering = false;
        }

        protected override void HandHoverUpdate(Hand hand)
        {
            if(type == hand.handType.ToString())
                isHovering = true;
        }

        void SetInformation(Information information)
        {
            if(interfaces != null)
            {
                for (int i = 0; i < interfaces.Count; i++)
                {
                    interfaces[i].Send(information);
                }
            }
        }

        protected override void Update()
        {
            if (isHovering)
            {
                SetInformation(new Information(Information.Modality.Text, "OK"));
            }
            else
            {
                if (target != null)
                {
                    float distance = Vector3.Distance(transform.position, target.position);
                    if (type.Equals("HeadGuide"))
                    {
                        if(distance <= hoveringRange)
                        {
                            isHovering = true;
                            SetInformation(new Information(Information.Modality.Text, "OK"));
                        }
                        else
                        {
                            isHovering = false;
                            SetInformation(new Information(Information.Modality.Text, type + "\n" + distance.ToString()));
                        }                        
                    }
                    else
                        SetInformation(new Information(Information.Modality.Text, type + "\n" + distance.ToString()));
                }
                else
                {
                    SetInformation(new Information(Information.Modality.Text, "Target should be set."));
                }
            }
        }
    }
}

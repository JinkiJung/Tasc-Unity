using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TascUnity
{
    public class AuditoryInterface : Interface
    {
        protected int narrationInterval;
        protected bool isNarrationStarted = false;
        protected bool isNarrationEnded = false;

        public void Start()
        {
            modality = Information.Modality.Audio;
        }

        public override void Send(Information information)
        {
            if (!isNarrationStarted)
            {
                Play(information.GetContent(Information.Modality.Audio));

                isNarrationStarted = true;
                narrationInterval = GlobalConstraint.NARRATION_INTERVAL;
            }
            else
            {
                if (!isNarrationEnded && narrationInterval < 0) // && !AudioInformation.isSpeaking())
                    isNarrationEnded = false;
                narrationInterval--;
            }
        }

        public override void Conclude()
        {
            base.Conclude();
            isNarrationStarted = false;
            isNarrationEnded = false;
        }

        public override bool IsSent()
        {
            return isNarrationEnded;
        }

        protected virtual void Play(string content)
        {

        }
    }
}

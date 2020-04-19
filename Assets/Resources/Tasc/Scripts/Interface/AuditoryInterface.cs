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

                /*
                foreach (KeyValuePair<Interface, Information> entry in informationContainer)
                {
                    // do something with entry.Value or entry.Key
                    entry.Key.Send(entry.Value);
                }
                /*
                for (int i=0; i< interfaces.Count; i++)
                {
                    if (interfaces[i] != null)
                    {
                        Debug.Log(interfaces[i].purpose);
                        Debug.Log(library.GetInfo(interfaces[i].purpose).content);
                        interfaces[i].Send(library.GetInfo(interfaces[i].purpose).content);
                    }
                }
                */

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

        public override bool IsDone()
        {
            return isNarrationEnded;
        }

        protected virtual void Play(string content)
        {

        }
    }
}

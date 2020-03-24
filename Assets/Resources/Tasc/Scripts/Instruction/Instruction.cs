using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Instruction
    {
        public enum TaskContext { None, Training, Tutorial, Assessment };
        public InformationContainer library;
        public TaskContext context;
        public string name;
        protected List<Interface> interfaces;
        int narrationInterval;
        private bool isNarrationStarted = false;
        private bool isNarrationEnded = false;

        public Instruction(List<Interface> givenInterfaces)
        {
            name = "";
            interfaces = givenInterfaces;
            library = new InformationContainer();
        }

        public Instruction(string givenTitle, List<Interface> givenInterfaces)
        {
            name = givenTitle;
            interfaces = givenInterfaces;
            library = new InformationContainer();
        }

        public Instruction(Instruction another)
        {
            name = another.name;
            library = new InformationContainer(another.library);
            interfaces = new List<Interface>();
            for(int i=0; i< another.interfaces.Count; i++)
                interfaces.Add(another.interfaces[i]); 
        }

        public void SetInfo(string context, string inputContent)
        {
            library.SetInfo(context, new Information(inputContent));
        }

        public Information GetInfo(string context)
        {
            return library.GetInfo(context);
        }

        public virtual void Proceed(bool isAudioEnabled = true)
        {
            if (!isNarrationStarted)
            {
                for(int i=0; i< interfaces.Count; i++)
                {
                    if (interfaces[i] != null)
                    {
                        Debug.Log(interfaces[i].purpose);
                        Debug.Log(library.GetInfo(interfaces[i].purpose).content);
                        interfaces[i].Send(library.GetInfo(interfaces[i].purpose).content);
                    }
                }
                
                isNarrationStarted = true;
                narrationInterval = GlobalConstraint.NARRATION_INTERVAL;
            }
            else
            {
                if (!isNarrationEnded && narrationInterval < 0 ) // && !AudioInformation.isSpeaking())
                    isNarrationEnded = false;
                narrationInterval--;
            }
        }

        public virtual void WrapUp()
        {

        }

        public bool isAudioInstructionEnded()
        {
            return isNarrationEnded;
        }

        
    }
}

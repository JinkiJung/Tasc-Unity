using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Instruction
    {
        //public InformationContainer library;
        public string name;
        //protected List<Interface> interfaces;
        protected Dictionary<Interface, Information> informationContainer;

        public Instruction(string _name, Interface _interface, Information _information)
        {
            name = _name;
            informationContainer = new Dictionary<Interface, Information>();
            Add(_interface, _information);
        }

        public void Add(Interface _interface, Information _information) {
            if(informationContainer != null)
            {
                informationContainer.Add(_interface, _information);
            }
        }

        public bool IsDone()
        {
            bool result = false;
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                result = entry.Key.IsDone() ? true : result;
            }
            return result;
        }

        public virtual void Conclude()
        {

        }

        public virtual void Proceed()
        {
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                entry.Key.Send(entry.Value);
            }
        }

        public virtual void Activate()
        {
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                entry.Key.Activate();
            }
        }

        public virtual void Deactivate()
        {
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                entry.Key.Deactivate();
            }
        }

    }
}

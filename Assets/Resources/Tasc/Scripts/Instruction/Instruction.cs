using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Instruction
    {
        public enum Policy { Once, Twice, Repeatitive };
        public string name;
        public Policy policy = Policy.Once;
        public int repeatTerm = 0;
        protected bool isDone = false;
        protected Dictionary<Interface, Information> informationContainer;

        public Instruction(string _name, Interface _interface, Information _information)
        {
            name = _name;
            informationContainer = new Dictionary<Interface, Information>();
            Add(_interface, _information);
            isDone = false;
        }

        public void Add(Interface _interface, Information _information) {
            if(informationContainer != null)
            {
                informationContainer.Add(_interface, _information);
            }
        }

        public bool ToldYou()
        {
            return isDone;
        }

        public virtual void Reset()
        {
            isDone = false;
        }

        public bool AllSent()
        {
            bool result = false;
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                result = entry.Key.IsSent() ? true : result;
            }
            return result;
        }

        public virtual void Conclude()
        {
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                entry.Key.Conclude();
            }

            Reset();
        }

        public virtual void Instruct()
        {
            foreach (KeyValuePair<Interface, Information> entry in informationContainer)
            {
                entry.Key.Send(entry.Value);
            }
            if(policy == Policy.Once)
            {
                isDone = true;
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

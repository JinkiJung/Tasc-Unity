using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public abstract class State: IComparable
    {
        public int id = -1;
        /*
        {
            get { return internal_id; }
            set
            {
                if (internal_id == value) return;
                internal_id = value;
                if (OnStateChange != null)
                    OnStateChange(this);
            }
        }*/

        public string name { set; get; }
        public string description { set; get; }
        public bool shouldLog;

        public delegate void OnStateChangeDelegate(State newState);
        public event OnStateChangeDelegate OnStateChange;

        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual void Update() { }

        public override string ToString()
        {
            return name;
        }
    }
}

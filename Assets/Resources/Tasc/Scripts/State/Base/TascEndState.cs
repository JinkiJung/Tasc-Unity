using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public sealed class TascEndState: State
    {
        public static TascEndState None = new TascEndState(2000, "None", "Not evaluated.");
        public static TascEndState Correct = new TascEndState(2001, "Correct", "Task state does not initiated.");
        public static TascEndState Incorrect = new TascEndState(2002, "Incorrect", "Task state initiated.");
        public static TascEndState Interrupted = new TascEndState(2003, "Interrupted", "Task state terminated.");

        public float progressRate;

        public TascEndState(int _id, string _name, string _description)
        {
            internalStateCode = _id;
            name = _name;
            description = _description;
            progressRate = 0.0f;
        }

        
    }
}

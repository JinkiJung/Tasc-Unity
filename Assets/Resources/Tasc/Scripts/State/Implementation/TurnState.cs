using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class TurnState : QuaternionVariableState
    {
        public TurnState(Terminus _sub, string _variableName="moveState") : base(_sub, _variableName, Quaternion.identity)
        {
            name = "TurnState";
            description = "Rotating/Turning event of a subject";
            value = new Parameter<Quaternion>(_sub.transform.rotation);
        }

        public bool hasSameTerminusWith(TurnState other)
        {
            return subject.name.Equals(other.subject.name);
        }

        public static bool areFromSameTerminus(TurnState state1, TurnState state2)
        {
            if (state1.hasSameTerminusWith(state2) || state2.hasSameTerminusWith(state1))
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if ((obj as TurnState) != null)
                return subject.name.Equals((obj as TurnState).subject.name) && value.Equals((obj as TurnState).value);
            else
                return false;
        }

        public override int CompareTo(object obj)
        {
            if ((obj as TurnState) != null)
                return subject.name.CompareTo((obj as TurnState).subject.name);
            else
                return 1;
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class QuaternionVariableState : VariableState
    {
        public Parameter<Quaternion> value;
        public QuaternionVariableState(Terminus _sub, string _variableName, Quaternion _value) : base(_sub, _variableName)
        {
            name = "QuaternionVariableState";
            description = "Check an Quaternion variable of a subject";
            value = new Parameter<Quaternion>(_value);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && value.GetValue().Equals((obj as QuaternionVariableState).value.GetValue());
        }

        public override int CompareTo(object obj)
        {
            return 1;
        }

        public Quaternion GetValue()
        {
            return value.GetValue();
        }

        public override float Diff(VariableState other)
        {
            if (other == null)
                return base.Diff(other);
            if (this.GetType() != other.GetType())
                return base.Diff(other);
            else
                return Quaternion.Angle(this.GetValue(), (other as QuaternionVariableState).GetValue());
        }

        public override string ToString()
        {
            return base.ToString() + ":(Quaternion)" + value.GetValue();
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public sealed class TascProgressState: State
    {
        public static TascProgressState Idle = new TascProgressState(1000, "Idle", "Tasc is not initiated.");
        public static TascProgressState Started = new TascProgressState(1001, "Started", "Tasc is in progress.");
        public static TascProgressState Ended = new TascProgressState(1002, "Ended", "Tasc is terminated.");
        
        public TascProgressState(int _id, string _name, string _description)
        {
            internalStateCode = _id;
            name = _name;
            description = _description;
        }

        public override bool Equals(object obj)
        {
            if ((obj as TascProgressState) != null)
            {
                TascProgressState other = obj as TascProgressState;
                return internalStateCode.Equals(other.internalStateCode);
            }
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int CompareTo(object obj)
        {
            if ((obj as TascProgressState) != null)
            {
                return internalStateCode.CompareTo((obj as TascProgressState).internalStateCode);
            }
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int GetHashCode()
        {
            // At the moment GetHashCode is not implemented.
            throw new System.Exception("GetHashCode function is not implemented.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class TascState : State
    {
        public Tasc task;
        public TascProgressState progressState;

        public TascState(Tasc _task, TascProgressState _progressState)
        {
            task = _task;
            progressState = _progressState;
        }

        // initialize with existing task instance and its progressState
        public TascState(Tasc _task)
        {
            task = _task;
            progressState = _task.state;
        }

        public override string ToString()
        {
            return "TascState: " + task.name + "\t" + progressState;
        }

        public override bool Equals(object obj)
        {
            if((obj as TascState) != null)
            {
                TascState other = obj as TascState;
                return task.name.Equals(other.task.name) && progressState.Equals(other.progressState);
            }
            else
                throw new System.Exception("It tried comparison with different format");
        }

        public override int CompareTo(object obj)
        {
            if ((obj as TascState) != null && task.name.Equals((obj as TascState).task.name))
            {
                TascState other = obj as TascState;
                return progressState.CompareTo(other.progressState);
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

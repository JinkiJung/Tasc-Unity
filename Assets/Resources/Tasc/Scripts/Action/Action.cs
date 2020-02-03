using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Action
    {
        public enum Type
        {
            None = 0,
            Move = 1,
            Manipulate = 2,
            WalkingBySwing = 3
        }

        public Type state;
        public Condition termination;

        public Action(Type _state, Condition _termination) { state = _state; termination = _termination; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace TascUnity
{
    public sealed class SingleConditionPublisher // singleton
    {
        // Instance
        private static readonly SingleConditionPublisher instance = new SingleConditionPublisher();
        public delegate void OnCheckDelegate(State state);
        public event OnCheckDelegate OnCheck;

        public static SingleConditionPublisher Instance
        {
            get
            {
                return instance;
            }
        }

        private SingleConditionPublisher()
        {

        }

        public void Send(State state)
        {
            if(this.OnCheck != null)
            {
                this.OnCheck(state);
            }
        }
    }

}


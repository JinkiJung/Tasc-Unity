using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace TascUnity
{
    public sealed class ConditionPublisher // singleton
    {
        public bool storeLog = true;
        private StateLogger stateLogger;
        // Instance
        private static readonly ConditionPublisher instance = new ConditionPublisher();
        public delegate void OnCheckDelegate(State state);
        public event OnCheckDelegate OnCheck;

        public static ConditionPublisher Instance
        {
            get
            {
                return instance;
            }
        }

        private ConditionPublisher()
        {
            stateLogger = new StateLogger();
        }

        public void StoreLog(string fileNamePrefix)
        {
            stateLogger.Store(fileNamePrefix);
        }

        public void Send(State state)
        {
            if(this.OnCheck != null)
            {
                this.OnCheck(state);

                if (storeLog)
                    stateLogger.StoreALog(state);
            }

        }

        
    }

}


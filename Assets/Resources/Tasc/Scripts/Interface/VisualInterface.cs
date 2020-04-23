using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TascUnity
{
    public class VisualInterface : Interface
    {
        protected bool isSent = false;
        public void Start()
        {
            isSent = false;
        }

        Renderer currentRenderer;
        
        void Awake()
        {
            currentRenderer = GetComponentInChildren<Renderer>();
        }

        public override void Activate()
        {
            base.Activate();
            SetVisibility(isActive);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            SetVisibility(isActive);
        }

        public override void Send(Information information)
        {
            if (!isActive)
                return;
            isSent = true;
        }

        public virtual void SetVisibility(bool value)
        {
            if (currentRenderer != null)
                currentRenderer.enabled = value;
        }

        public void SetPose(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public override bool IsSent()
        {
            return isSent;
        }
    }
}



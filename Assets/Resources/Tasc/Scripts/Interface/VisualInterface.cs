using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TascUnity
{
    public class VisualInterface : Interface
    {
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

        public override void Send(string msg)
        {
            if (!isActive)
                return;
            Set3DText(msg);
            Set2DText(msg);
        }

        public virtual void Set3DText(string givenText)
        {
            if (this.GetComponent<TextMesh>() != null)
            {
                this.GetComponent<TextMesh>().text = givenText;
            }
        }

        public virtual void Set2DText(string givenText)
        {
            if (this.GetComponent<Text>() != null)
            {
                this.GetComponent<Text>().text = givenText;
            }
        }

        public void SetVisibility(bool value)
        {
            if (currentRenderer != null)
                currentRenderer.enabled = value;
        }

        public void SetPose(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}



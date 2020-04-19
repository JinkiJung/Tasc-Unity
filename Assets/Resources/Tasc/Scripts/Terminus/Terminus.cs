using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Terminus : MonoBehaviour
    {
        
        protected Collider terminusChecker;
        private Vector3 previousPosition;
        public bool isControlled;

        public virtual void Send()
        {

        }

        protected virtual void Update()
        {
            ProceedWhenTransformChanged();
        }

        public virtual void ProceedWhenTransformChanged()
        {
            if (transform.hasChanged)
            {
                SendMoveState();
                transform.hasChanged = false;
            }
        }

        protected void SendMoveState()
        {
            Vector3 currPos = gameObject.transform.position;
            if (previousPosition != currPos)
            {
                SingleConditionPublisher.Instance.Send(new MoveState(this));
                previousPosition = currPos;
            }
        }

        public string ToJSON()
        {
            return JSONFormatter.ToJSON(this);
        }

        public virtual void Initialize()
        {
            name = transform.name;
            isControlled = false;
        }

        public virtual Transform Control(Transform terminus, Vector3 contactPoint, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            if (!isControlled)
                return null;
            else
                return this.transform;
        }

        public virtual void Grab(Transform terminus, Vector3 contactPoint, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            isControlled = true;
        }

        public virtual void Release(Transform terminus, Vector3 contactPoint, Quaternion controlRotation, bool givenFromDesktop = false)
        {
            isControlled = false;
        }

        public virtual void UpdateInterface(Interface relatedInterface)
        {
            relatedInterface.Send(new Information(Information.Modality.Text, ToString()));
        }

        public override string ToString()
        {
            return name;
        }
    }

    
}
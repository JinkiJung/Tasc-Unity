using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

namespace TascUnity
{
    [RequireComponent(typeof(Interactable))]
    public class TerminusSteamVR : Terminus
    {
        public List<Interface> interfaces;

        protected bool isInControl;

        public virtual void Awake()
        {
            if (transform != null)
            {
                name = transform.name;
            }
            isInControl = false;
        }

        protected override void Update()
        {
            UpdateInterfaces(interfaces);
        }

        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        protected virtual void OnHandHoverBegin(Hand hand)
        {
            //Debug.Log("Hovering hand: " + hand.name);
        }


        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        protected virtual void OnHandHoverEnd(Hand hand)
        {

        }

        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        protected virtual void HandHoverUpdate(Hand hand)
        {
            //Debug.Log("Hovering hand: " + hand.name);
            Proceed(hand);
        }

        protected GrabTypes grabbedWithType;
        public virtual void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            //*
            if (isInControl)
            {
                Transform newTrans = Control(this.transform, hand.transform.position, hand.transform.rotation);
                if (newTrans)
                    this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
            //*/
        }

        protected void UpdateInControl(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting(); // ~SteamVR Plugin v2.3.2
            bool isGrabEnding = hand.IsGrabbingWithType(grabbedWithType) == false;

            if (startingGrabType != GrabTypes.None)
            {
                grabbedWithType = startingGrabType;
                isInControl = true;
            }
            else if(grabbedWithType != GrabTypes.None && isGrabEnding)
            {
                isInControl = false;
            }
        }

        public void UpdateInterfaces(List<Interface> interfaces)
        {
            for (int i = 0; i < interfaces.Count; i++)
            {
                UpdateInterface(interfaces[i]);
            }
        }

        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        protected virtual void OnAttachedToHand(Hand hand)
        {
            //textMesh.text = "Attached to hand: " + hand.name;
        }


        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        protected virtual void OnDetachedFromHand(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        protected virtual void HandAttachedUpdate(Hand hand)
        {
            //textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + ( Time.time - attachTime ).ToString( "F2" );
        }


        //-------------------------------------------------
        // Called when this attached GameObject becomes the primary attached object
        //-------------------------------------------------
        protected virtual void OnHandFocusAcquired(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called when another attached GameObject becomes the primary attached object
        //-------------------------------------------------
        protected virtual void OnHandFocusLost(Hand hand)
        {
        }
    }
}



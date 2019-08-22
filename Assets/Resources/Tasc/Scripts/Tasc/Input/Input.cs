using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Input
    {
        public enum Type { KEYBOARD = 0, MOUSE = 1, HTCVIVE = 2, OCULUSTOUCH = 3, JOYPAD = 4 };

        /*
        public static string getKeyInput(State idx)
        {
            if (idx == State.Input1Press)
                return "Z";
            else if (idx == State.Input2Press)
                return "X";
            else
                return "";
        }

        public static ulong getViveInput(State idx)
        {
            if (idx == State.Move)
                return SteamVR_Controller.ButtonMask.Touchpad;
            if (idx == State.Input1Press)
                return SteamVR_Controller.ButtonMask.Trigger;
            else if (idx == State.Input2Press)
                return SteamVR_Controller.ButtonMask.Grip;
            else
                return SteamVR_Controller.ButtonMask.Axis0;
        }

        public static ulong getOTouchInput(State idx)
        {
            if (idx == State.Move)
                return SteamVR_Controller.ButtonMask.Touchpad;
            if (idx == State.Input1Press)
                return SteamVR_Controller.ButtonMask.Trigger;
            else if (idx == State.Input2Press)
                return SteamVR_Controller.ButtonMask.Grip;
            else
                return SteamVR_Controller.ButtonMask.Axis0;
        }

        // get State
        public static State getStateFromString(string str)
        {
            string checkInput = str.ToUpper();
            if (checkInput.Equals(getKeyInput(State.Input1Press)))
                return State.Input1Press;
            else if (checkInput.Equals(getKeyInput(State.Input2Press)))
                return State.Input2Press;
            else
                return State.None;
        }

        public static State getStateFromUlong(ulong checkInput)
        {
            if (checkInput.Equals(getViveInput(State.Input1Press)))
                return State.Input1Press;
            else if (checkInput.Equals(getViveInput(State.Input2Press)))
                return State.Input2Press;
            else
                return State.None;
        }

        // get string for instruction
        public static string getString(Type type, State idx)
        {
            if (type == Type.KEYBOARD)
            {
                if (idx == State.Input1Press)
                    return getKeyInput(idx) + " 버튼";
                else if (idx == State.Input2Press)
                    return getKeyInput(idx) + " 버튼";
                else
                    return "";
            }
            else if (type == Type.HTCVIVE)
            {
                if (idx == State.Move)
                    return "터치패드";
                if (idx == State.Input1Press)
                    return "트리거 버튼";
                else if (idx == State.Input2Press)
                    return "그립 버튼";
                else
                    return "";
            }
            else if (type == Type.OCULUSTOUCH)
            {
                if (idx == State.Move)
                    return "터치패드";
                if (idx == State.Input1Press)
                    return "트리거 버튼";
                else if (idx == State.Input2Press)
                    return "그립 버튼";
                else
                    return "";
            }
            else
                return "";
        }
        */
    }
}
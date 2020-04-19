using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace TascUnity
{
    public class VoiceInterface : AuditoryInterface
    {
#if UNITY_STANDALONE_WIN
        [DllImport("WindowsTTS")]
        public static extern void initSpeech();

        [DllImport("WindowsTTS")]
        public static extern void destroySpeech();

        [DllImport("WindowsTTS")]
        public static extern void addToSpeechQueue(byte[] s);
        //[DllImport("WindowsVoice", CharSet=CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void addToSpeechQueue([MarshalAs(UnmanagedType.LPStr)] string s);

        [DllImport("WindowsTTS")]
        public static extern void clearSpeechQueue();

        [DllImport("WindowsTTS")]
        public static extern void statusMessage(StringBuilder str, int length);

        [DllImport("WindowsTTS")]
        public static extern void changeVoice(int vIdx);

        [DllImport("WindowsTTS")]
        public static extern bool isSpeaking();
#endif
        
        public static VoiceInterface theVoice = null;

        public int voiceIdx = 0;
        
        void OnEnable()
        {
#if UNITY_STANDALONE_WIN
            if (theVoice == null)
            {
                Activate();
            }
#endif
        }

        void OnDestroy()
        {
#if UNITY_STANDALONE_WIN
            if (theVoice == this)
            {
                Deactivate();
            }
#endif
        }

        protected override void Play(string content)
        {
            if (!isActive)
                return;
            VoiceInterface.Speak(content, false);
        }

        public override void Activate()
        {
            base.Activate();
            if(theVoice==null)
            {
                theVoice = this;
                Debug.Log("Tasc:Narrator - Initializing speech");
                initSpeech();
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
            if (theVoice != null)
            {
                Debug.Log("Tasc:Narrator - Destroying speech");
                destroySpeech();
                theVoice = null;
            }
        }


        public override void Conclude()
        {
            base.Conclude();
            Deactivate();
        }

        private static void Speak(string msg, bool interruptable = false)
        {
#if UNITY_STANDALONE_OSX
            System.Diagnostics.Process.Start("say", (msg));
#endif
#if UNITY_STANDALONE_WIN
            Encoding encoding = System.Text.Encoding.GetEncoding(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage);
            var data = encoding.GetBytes(msg);
            if (interruptable)
                clearSpeechQueue();
            addToSpeechQueue(data);
            //addToSpeechQueue(msg);
#endif
        }

        private void Awake()
        {
#if UNITY_STANDALONE_WIN
            changeVoice(voiceIdx);
#endif

        }

        public void TestSpeech()
        {
            VoiceInterface.Speak("Do you hear me?", false);
        }

        private void Update()
        {
            //FOR TESTING
            /*
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))
            {
                Narrator.Speak("Is there any problem in there?", false);
                //Narrator.speak("거기 문제 있나요?", false);
            }
            //*/
        }

        private void OnApplicationQuit()
        {
#if UNITY_STANDALONE_WIN
            VoiceInterface.destroySpeech();
#endif
        }


    }

}

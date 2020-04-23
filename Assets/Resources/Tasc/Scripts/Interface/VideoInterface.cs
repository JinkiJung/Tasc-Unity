using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace TascUnity
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoInterface: VisualInterface
    {
        VideoPlayer videoPlayer;
        public void Start()
        {
            videoPlayer.EnableAudioTrack(0, false);
            modality = Information.Modality.Video;
        }
        
        void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        void EndReached(UnityEngine.Video.VideoPlayer vp)
        {
            videoPlayer.loopPointReached -= EndReached;
            videoPlayer.Stop();
            ConditionPublisher.Instance.Send(VideoState.Ended);
        }

        public override void Send(Information information)
        {
            if (!isActive)
                return;
            if (information.GetContent(modality) == "play")
            {
                if (!videoPlayer.isPlaying)
                {
                    videoPlayer.Play();
                    videoPlayer.loopPointReached += EndReached;
                }
            }
            else if (information.GetContent(modality) == "stop")
            {
                if (!videoPlayer.isPlaying)
                    videoPlayer.Stop();
            }
        }
        
    }
}



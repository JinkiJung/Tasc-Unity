using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public sealed class VideoState: State
    {
        public static VideoState None = new VideoState(3000, "None", "Not played.");
        public static VideoState Playing = new VideoState(3001, "Playing", "The video is playing.");
        public static VideoState Ended = new VideoState(3002, "Ended", "Playing of the video has done.");

        public VideoState(int _id, string _name, string _description)
        {
            internalStateCode = _id;
            name = _name;
            description = _description;
        }       
    }
}

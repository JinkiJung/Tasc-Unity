using UnityEngine;
using System.Collections;

namespace TascUnity
{
	public abstract class Interface : MonoBehaviour
    {
        public string purpose = ""; // context in use, e.g., None, Title, Description, Status, Narration, InteractiveStatus, Guidance
        public bool isActive = false;
        public virtual void Send(string information) { }
        public virtual void Activate() { isActive = true; }
        public virtual void Deactivate() { isActive = false; }
        public bool HasSamePurpose(string other) { return purpose.ToLower().Equals(other.ToLower()); }
    }
}


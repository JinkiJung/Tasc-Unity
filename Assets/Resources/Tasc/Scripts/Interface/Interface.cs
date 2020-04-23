using UnityEngine;
using System.Collections;

namespace TascUnity
{
	public abstract class Interface : MonoBehaviour
    {
        public string purpose = ""; // context in use, e.g., None, Title, Description, Status, Narration, InteractiveStatus, Guidance
        public bool isActive = false;
        public Information.Modality modality;

        public virtual void Send(Information information) { }
        public virtual void Activate() { isActive = true; }
        public virtual void Deactivate() { isActive = false; }
        public virtual bool IsSent() { return false; }
        public virtual void Conclude() { }

        public void SetModality(Information.Modality _modality) { modality = _modality; }
        public bool HasSamePurpose(string other) { return purpose.ToLower().Equals(other.ToLower()); }
    }
}


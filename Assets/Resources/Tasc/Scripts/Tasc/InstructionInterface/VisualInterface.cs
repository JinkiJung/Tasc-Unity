using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class VisualInterface : Interface
    {
        public override void Transfer(string msg)
        {
            Set3DText(msg);
        }

        public virtual void Set3DText(string givenText)
        {
            if (this.GetComponent<TextMesh>() != null)
            {
                this.GetComponent<TextMesh>().text = givenText;
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TascUnity
{
    public class TextInterface: VisualInterface
    {
        public void Start()
        {
            modality = Information.Modality.Text;
            isSent = false;
        }

        public override void Send(Information information)
        {
            base.Send(information);
            Set3DText(information.GetText());
            Set2DText(information.GetText());
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
    }
}



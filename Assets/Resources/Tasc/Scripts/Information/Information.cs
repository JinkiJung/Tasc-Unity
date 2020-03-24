using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tasc
{
    public class Information: ICloneable
    {
        public string content;

        public Information(string content)
        {
            this.content = content;
        }

        public object Clone()
        {
            return content.Clone();
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tasc
{
    public class InformationContainer
    {
        public Dictionary<string, Information> contextContent; // string for context

        public InformationContainer()
        {
            Initialize();
        }

        //*
        public InformationContainer(InformationContainer another)
        {
            contextContent = CloneDictionaryCloningValues<string, Information>(another.contextContent);
        }

        public Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>(Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                                                                    original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
        //*/

        public void Initialize()
        {
            if (contextContent == null)
            {
                contextContent = new Dictionary<string, Information>();
            }
        }

        public void SetInfo(string context, Information content)
        {
            if (contextContent == null)
                Initialize();
            if (contextContent.ContainsKey(context))
                contextContent[context] = content;
            else
                contextContent.Add(context, content);
        }

        public virtual Information GetInfo(string context)
        {
            Information result = null;
            return contextContent.TryGetValue(context, out result) ? result : null;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TascUnity
{
    public class Information
    {
        public enum Modality { Text = 0, Audio = 1, Haptic = 2, AvatarPose = 3 };
        public Dictionary<Modality, string> contextContent; // string for context

        public Information(Modality context, string content)
        {
            Initialize();
            SetContent(context, content);
        }

        //*
        public Information(Information another)
        {
            contextContent = CloneDictionaryCloningValues<Modality, string>(another.contextContent);
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
                contextContent = new Dictionary<Modality, string>();
            }
        }

        public void SetText(string content)
        {
            SetContent(Modality.Text , content);
        }

        public string GetText()
        {
            return GetContent(Modality.Text);
        }

        public void SetContent(Modality context, string content)
        {
            if (contextContent == null)
                Initialize();
            if (contextContent.ContainsKey(context))
                contextContent[context] = content;
            else
                contextContent.Add(context, content);
        }

        public virtual string GetContent(Modality context)
        {
            string result;
            return contextContent.TryGetValue(context, out result) ? result : "";
        }
    }
}


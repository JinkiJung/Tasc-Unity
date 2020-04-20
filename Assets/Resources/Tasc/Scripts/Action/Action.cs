﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Action
    {
        public enum Type
        {
            // low
            None = 0,
            Create = 1,
            Destroy = 2,
            ActiveRender = 3,
            DeactiveRender = 4,
            SetPosition = 5,
            SumPosition = 6,
            SetRotation = 7,
            SumRotation = 8,
            SetScale = 9,
            SumScale = 10,
            SetBool = 11,
            SetFloat = 12,
            SumFloat = 13,
            EnableAT = 14,
            DisableAT = 15,
            SoundOn = 16,
            SoundOff = 17,
            StartParticle = 18,
            StopParticle = 19,
            AttachToCamera = 20,
            // high
            EnableWalking = 21,
            DisableWalking = 22,
            Move = 23,
            Manipulate = 24,
            WalkingBySwing = 25
        }

        public Type state;

        public Terminus actor;

        public Terminus target;

        public Action(Terminus _actor, Type _state, Terminus _target) { state = _state; target = _target; }

        public Action(Terminus _actor, Type _state) { actor = _actor; state = _state; }
    }
}
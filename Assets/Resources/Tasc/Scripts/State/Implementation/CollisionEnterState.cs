﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class CollisionEnterState : InterTerminusState
    {
        public Parameter<float> value;
        public CollisionEnterState(Terminus _sub1, Terminus _sub2)
        {
            name = "CollisionEnterState";
            description = "Collision enter between subject1 and subject2";
            subject1 = _sub1;
            subject2 = _sub2;
        }

        public override void Update()
        {
            //value.SetValue(Vector3.Distance(subject1.gameObject.transform.position, subject2.gameObject.transform.position));
        }
    }
}

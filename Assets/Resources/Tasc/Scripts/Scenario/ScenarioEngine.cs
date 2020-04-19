using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TascUnity;

public sealed class ScenarioEngine : MonoBehaviour
{
    // Instance
    private static readonly ScenarioEngine instance = new ScenarioEngine();

    public static ScenarioEngine Instance
    {
        get
        {
            return instance;
        }
    }

    static ScenarioEngine()
    {

    }

    private ScenarioEngine()
    {

    }
}
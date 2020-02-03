using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tasc
{
    [Serializable]
    public class ProceduralScenario
    {
        public string name;
        public string description;
        List<Terminus> terminuses;
        List<Action> actions;
        List<Condition> conditions;
        List<Instruction> instructions;
        List<Tasc> scenario;

        public bool isActivated;

        public ProceduralScenario(string _name, string _description)
        {
            name = _name;
            description = _description;
            scenario = new List<Tasc>();
            isActivated = false;
        }

        public void LoadFromJSON(TextAsset textAsset)
        {
            Debug.Log(textAsset.text);
        }

        public void Add(Tasc t)
        {
            scenario.Add(t);
        }

        public void MakeProcedure()
        {
            for (int i = 0; i < scenario.Count - 1; i++)
            {
                scenario[i].SetNext(TascEndState.Correct, scenario[i + 1]);
            }
        }

        public void Activate()
        {
            isActivated = true;
            scenario[0].Activate();
        }

        void exportTerminusJSON()
        {
            /*
            Tasc.Asset[] assets = (Tasc.Asset[])GameObject.FindObjectsOfType(typeof(Tasc.Asset));
            string terminuses = "{";
            foreach (Tasc.Asset asset in assets)
            {
                terminuses += asset.ToJSON() + ",";
            }
            terminuses = terminuses.Remove(terminuses.Length - 1, 1) + "}";
            //Debug.Log(terminuses);
            System.IO.File.WriteAllText(System.IO.Path.Combine("Assets\\Data", "terminus.json"), terminuses);
            isTerminusExported = true;
            */
        }

        public void Proceed()
        {
            for (int i = 0; i < scenario.Count; i++)
            {
                scenario[i].Proceed();
                if (scenario[i].HasFinished())
                    scenario.RemoveAt(i);
            }
        }
    }
}
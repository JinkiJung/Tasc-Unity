using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    [System.Serializable]
    public class Scenario
    {
        public string name;
        public string description;
        Tasc test;
        List<Tasc> scenario;

        public bool isActivated;

        public Scenario(string _name, string _description)
        {
            name = _name;
            description = _description;
            scenario = new List<Tasc>();
            isActivated = false;
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

        public override string ToString()
        {
            var str = "";
            //for (int i = 0; i < scenario.Count; i++)
                //str += scenario[i] + "\n";
            return "name: " + name + "\ndescription: " + description + "\n" + test;
        }
    }
}
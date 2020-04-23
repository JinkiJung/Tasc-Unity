using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class Tasc: PrimitiveTask
    {
        public string name;
        public string description;
        public int priority;
        public bool isActivated;

        public List<string> given;
        public Condition when;
        public Terminus who;
        public Action does;
        public List<Instruction> follow;
        public Condition before;
        public Dictionary<TascEndState, Tasc> next;
        public TimeState startingTime;
        
        int cantSkipInterval;
        
        public Tasc()
        {
            if(state==null)
                state = TascProgressState.Idle;
            if(taskResult==null)
                taskResult = TascEndState.None;
            state.OnStateChange += StateChangeHandler;
            isActivated = false;
            next = new Dictionary<TascEndState, Tasc>();
            when = SingleCondition.DummySingleCondition;
            follow = new List<Instruction>();
        }

        public Tasc(bool _isActivated): this()
        {
            isActivated = _isActivated;
        }

        public bool HasFinished()
        {
            return !isActivated && state == TascProgressState.Ended;
        }

        private void StateChangeHandler(State newState){
            Debug.Log(TimeState.GetGlobalTimer() + "\tTasc: "+name+"\tTascProgressState: " + newState.ToString());
            ConditionPublisher.Instance.Send(new TascState(this, newState as TascProgressState));
        }

        public override string ToString()
        {
            return name + ": " + description;
        }

        public Tasc(string _name, string _description) : this()
        {
            name = _name;
            description = _description;
        }

        public TascEndState Evaluate(){
            return TascEndState.Correct;
        }

        public void SetNext(TascEndState taskEndState, Tasc task)
        {
            if(next != null && task != null)
            {
                next.Add(taskEndState, task);
            }
        }

        public void MoveNext(TascEndState taskEndState)
        {
            Deactivate();
            if (taskEndState != TascEndState.None && next.ContainsKey(taskEndState) && next[taskEndState]!= null)
                next[taskEndState].Activate();
        }

        public void AddInstruction(Instruction instruction)
        {
            follow.Add(instruction);
        }

        public void Activate()
        {
            if (!isActivated)
            {
                isActivated = true;
                OnStateChange += StateChangeHandler;
                when.ActivateAndStartMonitoring();
            }
        }

        public void Deactivate()
        {
            if (isActivated)
            {
                isActivated = false;
                OnStateChange -= StateChangeHandler;
                when.Deactivate();
                before.Deactivate();
            }
        }

        public bool Proceed()
        {
            if (when == null || before == null)
                throw new MissingComponentException();
            if (!isActivated)
                return false;

            bool resultFromExit = false;
            if (state == TascProgressState.Idle)
            {
                if (when.CheckPassive()){
                    state = TascProgressState.Started;
                    startingTime = new TimeState(TimeState.GetGlobalTimer());
                    cantSkipInterval = GlobalConstraint.TASK_CANT_SKIP_INTERVAL;
                    when.Deactivate();
                    before.ActivateAndStartMonitoring();
                }
            }
            else if (state == TascProgressState.Started)
            {
                for (int i = 0; i < follow.Count; i++)
                {
                    if (!follow[i].ToldYou())
                        follow[i].Instruct();
                    cantSkipInterval--;
                }
                resultFromExit = before.CheckPassive();
                if (resultFromExit && cantSkipInterval < 0)
                {
                    TascEndState evaluateResult = Evaluate();
                    if(evaluateResult == TascEndState.Correct)
                    {
                        state = TascProgressState.Ended;
                        for (int i = 0; i < follow.Count; i++)
                        {
                            follow[i].Conclude();
                        }
                        MoveNext(evaluateResult);
                    }
                }
            }
            return resultFromExit;
        }
    }
}
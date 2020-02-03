using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Tasc: PrimitiveTasc
    {
        public string name;
        public string description;
        public int priority;
        public bool isActivated;
        public Effect given;
        public Terminus actor;
        public Expression entrance;
        public Terminus target;
        public Action action;
        public Expression exit;
        public List<Instruction> instructions;
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
            entrance = Condition.DummyCondition;
            instructions = new List<Instruction>();
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
            Debug.Log(TimeState.GetGlobalTimer() + "\tTask: "+name+"\tTascProgressState: " + newState.ToString());
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

        public void SetNext(TascEndState TascEndState, Tasc task)
        {
            if(next != null && task != null)
            {
                next.Add(TascEndState, task);
            }
        }

        public void MoveNext(TascEndState TascEndState)
        {
            Deactivate();
            if (TascEndState != TascEndState.None && next.ContainsKey(TascEndState) && next[TascEndState]!= null)
                next[TascEndState].Activate();
        }

        public void AddInstruction(Instruction instruction)
        {
            instructions.Add(instruction);
        }

        public void Activate()
        {
            if (!isActivated)
            {
                isActivated = true;
                OnStateChange += StateChangeHandler;
                entrance.ActivateAndStartMonitoring();
            }
        }

        public void Deactivate()
        {
            if (isActivated)
            {
                isActivated = false;
                OnStateChange -= StateChangeHandler;
                entrance.Deactivate();
                exit.Deactivate();
            }
        }

        public bool Proceed()
        {
            if (entrance == null || exit == null)
                throw new MissingComponentException();
            if (!isActivated)
                return false;

            bool resultFromExit = false;
            if (state == TascProgressState.Idle)
            {
                if (entrance.CheckPassive()){
                    state = TascProgressState.Started;
                    startingTime = new TimeState(TimeState.GetGlobalTimer());
                    cantSkipInterval = GlobalConstraint.TASK_CANT_SKIP_INTERVAL;
                    entrance.Deactivate();
                    exit.ActivateAndStartMonitoring();
                }
            }
            else if (state == TascProgressState.Started)
            {
                for (int i = 0; i < instructions.Count; i++)
                {
                    instructions[i].Proceed();
                    if (!instructions[i].isAudioInstructionEnded())
                        cantSkipInterval--;
                }
                resultFromExit = exit.CheckPassive();
                if (resultFromExit && cantSkipInterval < 0)
                {
                    TascEndState evaluateResult = Evaluate();
                    if(evaluateResult == TascEndState.Correct)
                    {
                        state = TascProgressState.Ended;
                        for (int i = 0; i < instructions.Count; i++)
                        {
                            instructions[i].WrapUp();
                        }
                        MoveNext(evaluateResult);
                    }
                }
            }
            return resultFromExit;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Task: PrimitiveTask
    {
        public string name;
        public string description;
        public int priority;
        public bool isActivated;

        public string given;
        public Expression when;
        public Terminus who;
        public Terminus target;
        public Task does;
        public List<Instruction> follow;
        public Expression before;
        public Dictionary<TaskEndState, Task> next;
        public TimeState startingTime;
        
        int cantSkipInterval;
        
        public Task()
        {
            if(state==null)
                state = TaskProgressState.Idle;
            if(taskResult==null)
                taskResult = TaskEndState.None;
            state.OnStateChange += StateChangeHandler;
            isActivated = false;
            next = new Dictionary<TaskEndState, Task>();
            when = Condition.DummyCondition;
            follow = new List<Instruction>();
        }

        public Task(bool _isActivated): this()
        {
            isActivated = _isActivated;
        }

        public bool HasFinished()
        {
            return !isActivated && state == TaskProgressState.Ended;
        }

        private void StateChangeHandler(State newState){
            Debug.Log(TimeState.GetGlobalTimer() + "\tTask: "+name+"\tTaskProgressState: " + newState.ToString());
            ConditionPublisher.Instance.Send(new TaskState(this, newState as TaskProgressState));
        }

        public override string ToString()
        {
            return name + ": " + description;
        }

        public Task(string _name, string _description) : this()
        {
            name = _name;
            description = _description;
        }

        public TaskEndState Evaluate(){
            return TaskEndState.Correct;
        }

        public void SetNext(TaskEndState taskEndState, Task task)
        {
            if(next != null && task != null)
            {
                next.Add(taskEndState, task);
            }
        }

        public void MoveNext(TaskEndState taskEndState)
        {
            Deactivate();
            if (taskEndState != TaskEndState.None && next.ContainsKey(taskEndState) && next[taskEndState]!= null)
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
            if (state == TaskProgressState.Idle)
            {
                if (when.CheckPassive()){
                    state = TaskProgressState.Started;
                    startingTime = new TimeState(TimeState.GetGlobalTimer());
                    cantSkipInterval = GlobalConstraint.TASK_CANT_SKIP_INTERVAL;
                    when.Deactivate();
                    before.ActivateAndStartMonitoring();
                }
            }
            else if (state == TaskProgressState.Started)
            {
                for (int i = 0; i < follow.Count; i++)
                {
                    follow[i].Proceed();
                    if (!follow[i].isAudioInstructionEnded())
                        cantSkipInterval--;
                }
                resultFromExit = before.CheckPassive();
                if (resultFromExit && cantSkipInterval < 0)
                {
                    TaskEndState evaluateResult = Evaluate();
                    if(evaluateResult == TaskEndState.Correct)
                    {
                        state = TaskProgressState.Ended;
                        for (int i = 0; i < follow.Count; i++)
                        {
                            follow[i].WrapUp();
                        }
                        MoveNext(evaluateResult);
                    }
                }
            }
            return resultFromExit;
        }
    }
}
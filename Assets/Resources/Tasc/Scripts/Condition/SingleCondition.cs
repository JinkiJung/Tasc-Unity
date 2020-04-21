using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TascUnity
{
    public class SingleCondition : Condition
    {
        public static SingleCondition DummySingleCondition = new SingleCondition(true);
        public static SingleCondition NeverSatisfied = new SingleCondition(false);
        public static SingleCondition StartFromBeginning = DummySingleCondition;

        ////////////////////////////////////////////////
        // should implement as multiple operator and operand
        ////////////////////////////////////////////////
        ///

        public RelationalOperator comparison;
        public State endSingleConditionState = null;
        public TimeState holdingTimer;
        protected bool isSatisfied;
        protected bool isActivated;
        public int holdingCount;

        // constructor for dummy condition
        public SingleCondition(bool _isSatisfied)
        {
            isSatisfied = _isSatisfied;
            isActivated = true;
            comparison = RelationalOperator.NotEqual;
            holdingTimer = null;
            holdingCount = 0;
        }

        public SingleCondition(State _endSingleCondition, RelationalOperator _comparison)
        {
            endSingleConditionState = _endSingleCondition;
            comparison = _comparison;
            isSatisfied = false;
            holdingTimer = null;
            holdingCount = 0;
        }

        public SingleCondition(State _endSingleCondition, RelationalOperator _comparison, TimeState _elapsedState): this(_endSingleCondition, _comparison)
        {
            holdingTimer = _elapsedState;
        }

        ~SingleCondition()
        {
            Deactivate();
        }

        public override void ActivateAndStartMonitoring()
        {
            if (!isActivated)
            {
                isActivated = true;
                StartMonitoring();                 
            }
        }

        public override void Activate()
        {
            isActivated = true;
        }

        public override void Deactivate()
        {
            if (isActivated)
            {
                isActivated = false;
                StopMonitoring();
            }
        }

        public override bool IsActivated()
        {
            return isActivated;
        }

        public override string ToString()
        {
            return endSingleConditionState + " : " + comparison + (holdingTimer == null ? "" : " (during " + holdingTimer.ToString() + ")");
        }

        public void StartMonitoring()
        {
            ConditionPublisher.Instance.OnCheck += Send;
        }

        public void StopMonitoring()
        {
            ConditionPublisher.Instance.OnCheck -= Send;
        }

        public void Send(State state)
        {
            if(IsActivated() && !IsSatisfied())
                Check(state);
        }

        public override bool CheckPassive()
        {
            if (isSatisfied)
                return true;
            if (IsActivated() && !IsSatisfied())
                if (ShouldCheckPassively())
                    return Check(null);
            return false;
        }

        protected override bool Check(State state1, Operator ope, State state2, TimeState timeState = null)
        {
            if (isSatisfied)
                return true;
            bool result = false;

            // unwrapping autovariable state: we convert it to the specific varible state inside of the AutoVariableState.
            if (state1.GetType() == typeof(AutoVariableState))
                state1 = (state1 as AutoVariableState).GetVariableState();
            if (state2.GetType() == typeof(AutoVariableState))
                state2 = (state2 as AutoVariableState).GetVariableState();

            if (state1.GetType() == state2.GetType())
            {
                //Debug.Log("Check: " + state1.ToString() + "\t" + state2.ToString());
                //Debug.Log(state1.CompareTo(state2));
                if (ope == RelationalOperator.Larger)
                    result = state1.CompareTo(state2) > 0;
                else if (ope == RelationalOperator.LargerOrEqual)
                    result = state1.CompareTo(state2) > 0 || state1.Equals(state2);
                else if (ope == RelationalOperator.Equal)
                    result = state1.Equals(state2);
                else if (ope == RelationalOperator.SmallerOrEqual)
                    result = state1.CompareTo(state2) < 0 || state1.Equals(state2);
                else if (ope == RelationalOperator.Smaller)
                    result = state1.CompareTo(state2) < 0;
                else if (ope == RelationalOperator.NotEqual)
                    result = state1 != state2;
            }
            if (holdingTimer != null)
            {
                if (result)
                {
                    if(holdingCount<30)
                        holdingCount += 1;
                    if (!holdingTimer.IsTimerOn())
                        holdingTimer.StartTimer();
                }
                else
                {
                    if (holdingCount > 0)
                        holdingCount -= 1;
                    else
                    {
                        holdingTimer.StopTimer();
                    }
                }
                if (holdingTimer.IsOver())
                    isSatisfied = true;
            }
            else
                isSatisfied = result;
            return isSatisfied;
        }

        public override bool Check(State state, TimeState timeState = null)
        {
            if (endSingleConditionState == null)
                throw new MissingComponentException();

            if (endSingleConditionState.GetType() == typeof(TimeState))
                return Check(TimeState.GlobalTimer, comparison, endSingleConditionState);
            else if (endSingleConditionState.GetType() == typeof(TascState))
            {
                TascState taskState = endSingleConditionState as TascState;
                //Debug.Log("HandleTascState : " + Check(new TascState(taskState.task), cond.endSingleConditionState, cond.comparison));
                return Check(new TascState(taskState.task), comparison, endSingleConditionState);
            }
            else if (endSingleConditionState.GetType() == typeof(VariableDistanceState) && state.GetType().IsSubclassOf(typeof(VariableState)))
            {
                VariableDistanceState var1 = endSingleConditionState as VariableDistanceState;
                if ((state as VariableState) != null)
                {
                    VariableState var2 = state as VariableState;
                    return VariableState.IsSameVariable(var1.stateVar1, var2) ? Check(new VariableDistanceState(var1, var2), comparison, endSingleConditionState, timeState) : false;
                }
                return false;
            }
            else if (endSingleConditionState.GetType() == typeof(DistanceState))
            {
                DistanceState var1 = endSingleConditionState as DistanceState;
                MoveState var2 = state as MoveState;
                if ((state as MoveState) != null)
                {
                    return var1.hasMoveStateFromSameTerminus(var2) ? Check(var1.GetUpdated(var2), comparison, endSingleConditionState, timeState) : false;
                }
                else
                    return false;
            }
            else
                return Check(state, comparison, endSingleConditionState, timeState);
        }

        public bool ShouldCheckPassively()
        {
            return ShouldCheckTascState() || ShouldCheckTimeState();
        }

        public bool ShouldCheckTimeState()
        {
            return endSingleConditionState.GetType() == typeof(TimeState);
        }

        public bool ShouldCheckTascState()
        {
            return endSingleConditionState.GetType() == typeof(TascState);
        }

        public override bool IsSatisfied()
        {
            return isSatisfied;
        }
    }
}
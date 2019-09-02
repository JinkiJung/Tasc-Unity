using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasc;

public class ScenarioController : MonoBehaviour
{
    Scenario scenario = new Scenario("Test1", "Test scenario for a unit test");
    public List<Interface> interfaces;
    public Actor actor;

    // Use this for initialization
    void Start()
    {
        InitializeScenario();
    }

    void InitializeScenario()
    {
        MakeTestScenario();
    }

    void MakeTestScenario()
    {
        TestTerminus testTerminus = GameObject.Find("TestTerminus").GetComponent<TestTerminus>();

        actor = FindObjectsOfType<Actor>()[0];
        if (actor == null)
            throw new Exception("An actor should be assigned first!");

        Task test1 = new Task("Test1 - TimeState", "");
        test1.instruction = new Instruction(test1.name);
        test1.instruction.SetContentWithContext(test1.name, Information.Context.Title);
        test1.instruction.SetContentWithContext(test1.name, Information.Context.Narration);
        test1.instruction.SetContentWithContext(test1.name, Information.Context.Description);
        test1.exit = new Condition(new TimeState(0, 0, 1), Condition.RelationalOperator.Larger);
        scenario.Add(test1);

        Task test2 = new Task("Test2 - TaskState", "");
        test2.instruction = new Instruction(test2.name);
        test2.instruction.SetContentWithContext(test2.name, Information.Context.Title);
        test2.instruction.SetContentWithContext(test2.name, Information.Context.Narration);
        test2.instruction.SetContentWithContext(test2.name, Information.Context.Description);
        test2.exit = new Condition(new TaskState(test2, TaskProgressState.Started), Condition.RelationalOperator.Equal);
        scenario.Add(test2);

        Task test3 = new Task("Test3 - TaskState + TimeState", "");
        test3.instruction = new Instruction(test3.name);
        test3.instruction.SetContentWithContext(test3.name, Information.Context.Title);
        test3.instruction.SetContentWithContext(test3.name, Information.Context.Narration);
        test3.instruction.SetContentWithContext(test3.name, Information.Context.Description);
        test3.exit = new Condition(new TaskState(test3, TaskProgressState.Started), Condition.RelationalOperator.Equal, new TimeState(0,0,1));
        scenario.Add(test3);

        Task test4 = new Task("Test4 - BoolVariableState", "");
        test4.instruction = new Instruction(test4.name);
        test4.instruction.SetContentWithContext(test4.name, Information.Context.Title);
        test4.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test4.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test4.exit = new Condition(new AutoVariableState(testTerminus, "isPushed", true), Condition.RelationalOperator.Equal);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(test4);

        Task test5 = new Task("Test5 - IntVariableState", "");
        test5.instruction = new Instruction(test5.name);
        test5.instruction.SetContentWithContext(test5.name, Information.Context.Title);
        test5.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test5.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test5.exit = new Condition(new AutoVariableState(testTerminus, "incrementValue_int", 2), Condition.RelationalOperator.LargerOrEqual);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(test5);

        Task test6 = new Task("Test6 - FloatVariableState", "");
        test6.instruction = new Instruction(test6.name);
        test6.instruction.SetContentWithContext(test6.name, Information.Context.Title);
        test6.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test6.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test6.exit = new Condition(new AutoVariableState(testTerminus, "incrementValue", 1.5f), Condition.RelationalOperator.LargerOrEqual);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(test6);

        Task test7 = new Task("Test7 - VectorVariableState", "");
        test7.instruction = new Instruction(test7.name);
        test7.instruction.SetContentWithContext(test7.name, Information.Context.Title);
        test7.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test7.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test7.exit = new Condition(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), Condition.RelationalOperator.Equal);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), Condition.RelationalOperator.Equal);
        scenario.Add(test7);

        Task test8 = new Task("Test8 - InputDownState", "");
        test8.instruction = new Instruction(test8.name);
        test8.instruction.SetContentWithContext(test8.name, Information.Context.Title);
        test8.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test8.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test8.exit = new Condition(new InputDownState(actor, (int)KeyCode.Z), Condition.RelationalOperator.Equal);
        scenario.Add(test8);

        Task test9 = new Task("Test9 - InputUpState", "");
        test9.instruction = new Instruction(test9.name);
        test9.instruction.SetContentWithContext(test9.name, Information.Context.Title);
        test9.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test9.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test9.exit = new Condition(new InputUpState(actor, (int)KeyCode.Z), Condition.RelationalOperator.Equal);
        scenario.Add(test9);

        Task test10 = new Task("Test10 - VariableDistanceState", "");
        test10.instruction = new Instruction(test10.name);
        test10.instruction.SetContentWithContext(test10.name, Information.Context.Title);
        test10.instruction.SetContentWithContext("Press Z button.", Information.Context.Narration);
        test10.instruction.SetContentWithContext("Press Z button.", Information.Context.Description);
        test10.exit = new Condition(new VariableDistanceState(new VectorVariableState(testTerminus, "incrementVector",Vector3.zero),0.1f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(test10);

        Task test11 = new Task("Test11 - DistanceState", "");
        test11.instruction = new Instruction(test11.name);
        test11.instruction.SetContentWithContext(test11.name, Information.Context.Title);
        test11.instruction.SetContentWithContext("Move forward.", Information.Context.Narration);
        test11.instruction.SetContentWithContext("Move forward.", Information.Context.Description);
        test11.exit = new Condition(new DistanceState(new MoveState(actor), new MoveState(testTerminus), 3.0f), Condition.RelationalOperator.SmallerOrEqual);
        scenario.Add(test11);

        ///////////////////////////////////////////////////////////////////////////
        // tests to be implemented...
        // input hold
        // Collision
        // Hover
        // Gaze
        ///////////////////////////////////////////////////////////////////////////
        
        ///////////////////////////////////////////////////////////////////////////
        // tests as multiple conditions...
        /*
        //introduction.exit = new Condition(new TaskState(introduction, TaskProgressState.Started), Condition.Operator.Equal, new TimeState(0,0,1));
        introduction.exit = new Condition(new Condition(new TaskState(introduction, TaskProgressState.Started), Condition.RelationalOperator.Equal, new TimeState(0, 0, 1)),
            Condition.LogicalOperator.And,
            new Condition(new DistanceState(new MoveState(testTerminus), new MoveState(actor), 1.5f), Condition.RelationalOperator.SmallerOrEqual, new TimeState(0, 0, 1)));
        */

        Task ending = new Task("Finish", "");
        ending.instruction = new Instruction(ending.name);
        ending.instruction.SetContentWithContext("Finish", Information.Context.Title);
        ending.instruction.SetContentWithContext("Well done! Your training is successfully terminated.", Information.Context.Narration);
        ending.instruction.SetContentWithContext("Well done! Your training is successfully terminated.", Information.Context.Description);
        ending.exit = new Condition(new InputUpState(actor, (int)KeyCode.C), Condition.RelationalOperator.Equal);
        scenario.Add(ending); 

        scenario.MakeProcedure();

        scenario.Activate();

        //Debug.Log(JsonUtility.ToJson(scenario));
    }

    

    // Update is called once per frame
    void Update()
    {
        if (scenario != null)
            scenario.Proceed(interfaces);
    }
}

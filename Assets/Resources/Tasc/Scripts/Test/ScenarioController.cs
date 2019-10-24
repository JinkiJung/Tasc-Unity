using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasc;

public class ScenarioController : MonoBehaviour
{
    ProceduralScenario scenario = new ProceduralScenario("Test1", "Test scenario for a unit test");
    public List<TransferElement> interfaces;
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
        Instruction test1instruction = new Instruction(test1.name, interfaces);
        test1instruction.SetContent("Title",test1.name);
        test1instruction.SetContent("Narration", test1.name);
        test1instruction.SetContent("Description", test1.name);
        test1.AddInstruction(test1instruction);
        test1.exit = new Condition(new TimeState(0, 0, 1), RelationalOperator.Larger);
        scenario.Add(test1);

        Task test2 = new Task("Test2 - TaskState", "");
        Instruction test2instruction = new Instruction(test2.name, interfaces);
        test2instruction.SetContent("Title", test2.name);
        test2instruction.SetContent("Narration", test2.name);
        test2instruction.SetContent("Description", test2.name);
        test2.AddInstruction(test2instruction);
        test2.exit = new Condition(new TaskState(test2, TaskProgressState.Started), RelationalOperator.Equal);
        scenario.Add(test2);

        Task test3 = new Task("Test3 - TaskState + TimeState", "");
        Instruction test3instruction = new Instruction(test3.name, interfaces);
        test3instruction.SetContent("Title", test3.name);
        test3instruction.SetContent("Narration", test3.name);
        test3instruction.SetContent("Description", test3.name);
        test3.AddInstruction(test3instruction);
        test3.exit = new Condition(new TaskState(test3, TaskProgressState.Started), RelationalOperator.Equal, new TimeState(0, 0, 1));
        scenario.Add(test3);

        Task test4 = new Task("Test4 - BoolVariableState", "");
        Instruction test4instruction = new Instruction(test4.name, interfaces);
        test4instruction.SetContent("Title", test4.name);
        test4instruction.SetContent("Narration", "Press Z button.");
        test4instruction.SetContent("Description", "Press Z button.");
        test4.AddInstruction(test4instruction);
        test4.exit = new Condition(new AutoVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test4);

        Task test5 = new Task("Test5 - IntVariableState", "");
        Instruction test5instruction = new Instruction(test5.name, interfaces);
        test5instruction.SetContent("Title", test5.name);
        test5instruction.SetContent("Narration", "Press Z button.");
        test5instruction.SetContent("Description", "Press Z button.");
        test5.AddInstruction(test5instruction);
        test5.exit = new Condition(new AutoVariableState(testTerminus, "incrementValue_int", 2), RelationalOperator.LargerOrEqual);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test5);

        Task test6 = new Task("Test6 - FloatVariableState", "");
        Instruction test6instruction = new Instruction(test6.name, interfaces);
        test6instruction.SetContent("Title", test6.name);
        test6instruction.SetContent("Narration", "Press Z button.");
        test6instruction.SetContent("Description", "Press Z button.");
        test6.AddInstruction(test6instruction);
        test6.exit = new Condition(new AutoVariableState(testTerminus, "incrementValue", 1.5f), RelationalOperator.LargerOrEqual);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test6);

        Task test7 = new Task("Test7 - VectorVariableState", "");
        Instruction test7instruction = new Instruction(test7.name, interfaces);
        test7instruction.SetContent("Title", test7.name);
        test7instruction.SetContent("Narration", "Press Z button.");
        test7instruction.SetContent("Description", "Press Z button.");
        test7.AddInstruction(test7instruction);
        test7.exit = new Condition(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), RelationalOperator.Equal);
        //test4.exit = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test7);

        Task test8 = new Task("Test8 - InputDownState", "");
        Instruction test8instruction = new Instruction(test8.name, interfaces);
        test8instruction.SetContent("Title", test8.name);
        test8instruction.SetContent("Narration", "Press Z button.");
        test8instruction.SetContent("Description", "Press Z button.");
        test8.AddInstruction(test8instruction);
        test8.exit = new Condition(new InputDownState(actor, (int)KeyCode.Z), RelationalOperator.Equal);
        scenario.Add(test8);

        Task test9 = new Task("Test9 - InputUpState", "");
        Instruction test9instruction = new Instruction(test9.name, interfaces);
        test9instruction.SetContent("Title", test9.name);
        test9instruction.SetContent("Narration", "Press Z button.");
        test9instruction.SetContent("Description", "Press Z button.");
        test9.AddInstruction(test9instruction);
        test9.exit = new Condition(new InputUpState(actor, (int)KeyCode.Z), RelationalOperator.Equal);
        scenario.Add(test9);

        Task test10 = new Task("Test10 - VariableDistanceState", "");
        Instruction test10instruction = new Instruction(test10.name, interfaces);
        test10instruction.SetContent("Title", test10.name);
        test10instruction.SetContent("Narration", "Press Z button.");
        test10instruction.SetContent("Description", "Press Z button.");
        test10.AddInstruction(test10instruction);
        test10.exit = new Condition(new VariableDistanceState(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), 0.1f), RelationalOperator.SmallerOrEqual);
        scenario.Add(test10);

        Task test11 = new Task("Test11 - DistanceState", "");
        Instruction test11instruction = new Instruction(test11.name, interfaces);
        test11instruction.SetContent("Title", test11.name);
        test11instruction.SetContent("Narration", "Move forward.");
        test11instruction.SetContent("Description", "Move forward.");
        test11.AddInstruction(test11instruction);
        test11.exit = new Condition(new DistanceState(new MoveState(actor), new MoveState(testTerminus), 3.0f), RelationalOperator.SmallerOrEqual);
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
        Instruction endinginstruction = new Instruction(ending.name, interfaces);
        endinginstruction.SetContent("Title", "Finish");
        endinginstruction.SetContent("Narration", "Well done! Your training is successfully terminated.");
        endinginstruction.SetContent("Description", "Well done! Your training is successfully terminated.");
        ending.AddInstruction(endinginstruction);
        ending.exit = new Condition(new InputUpState(actor, (int)KeyCode.C), RelationalOperator.Equal);
        scenario.Add(ending);

        scenario.MakeProcedure();

        scenario.Activate();
    }



    // Update is called once per frame
    void Update()
    {
        if (scenario != null)
            scenario.Proceed();
    }
}
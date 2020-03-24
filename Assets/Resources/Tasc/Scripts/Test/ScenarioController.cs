using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TascUnity;

public class ScenarioController : MonoBehaviour
{
    ProceduralScenario scenario = new ProceduralScenario("Test1", "Test scenario for a unit test");
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

        for(int i=0; i<interfaces.Count; i++)
        {
            if(interfaces[i])
                interfaces[i].Activate();
        }

        Tasc test1 = new Tasc("Test1 - TimeState", "");
        Instruction test1instruction = new Instruction(test1.name, interfaces);
        test1instruction.SetInfo("Title",test1.name);
        test1instruction.SetInfo("Narration", test1.name);
        test1instruction.SetInfo("Description", test1.name);
        test1.AddInstruction(test1instruction);
        test1.before = new Condition(new TimeState(0, 0, 1), RelationalOperator.Larger);
        scenario.Add(test1);

        Tasc test2 = new Tasc("Test2 - TascState", "");
        Instruction test2instruction = new Instruction(test2.name, interfaces);
        test2instruction.SetInfo("Title", test2.name);
        test2instruction.SetInfo("Narration", test2.name);
        test2instruction.SetInfo("Description", test2.name);
        test2.AddInstruction(test2instruction);
        test2.before = new Condition(new TascState(test2, TascProgressState.Started), RelationalOperator.Equal);
        scenario.Add(test2);

        Tasc test3 = new Tasc("Test3 - TascState + TimeState", "");
        Instruction test3instruction = new Instruction(test3.name, interfaces);
        test3instruction.SetInfo("Title", test3.name);
        test3instruction.SetInfo("Narration", test3.name);
        test3instruction.SetInfo("Description", test3.name);
        test3.AddInstruction(test3instruction);
        test3.before = new Condition(new TascState(test3, TascProgressState.Started), RelationalOperator.Equal, new TimeState(0, 0, 1));
        scenario.Add(test3);

        Tasc test4 = new Tasc("Test4 - BoolVariableState", "");
        Instruction test4instruction = new Instruction(test4.name, interfaces);
        test4instruction.SetInfo("Title", test4.name);
        test4instruction.SetInfo("Narration", "Press Z button.");
        test4instruction.SetInfo("Description", "Press Z button.");
        test4.AddInstruction(test4instruction);
        test4.before = new Condition(new AutoVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        //test4.before = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test4);

        Tasc test5 = new Tasc("Test5 - IntVariableState", "");
        Instruction test5instruction = new Instruction(test5.name, interfaces);
        test5instruction.SetInfo("Title", test5.name);
        test5instruction.SetInfo("Narration", "Press Z button.");
        test5instruction.SetInfo("Description", "Press Z button.");
        test5.AddInstruction(test5instruction);
        test5.before = new Condition(new AutoVariableState(testTerminus, "incrementValue_int", 2), RelationalOperator.LargerOrEqual);
        //test4.before = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test5);

        Tasc test6 = new Tasc("Test6 - FloatVariableState", "");
        Instruction test6instruction = new Instruction(test6.name, interfaces);
        test6instruction.SetInfo("Title", test6.name);
        test6instruction.SetInfo("Narration", "Press Z button.");
        test6instruction.SetInfo("Description", "Press Z button.");
        test6.AddInstruction(test6instruction);
        test6.before = new Condition(new AutoVariableState(testTerminus, "incrementValue", 1.5f), RelationalOperator.LargerOrEqual);
        //test4.before = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test6);

        Tasc test7 = new Tasc("Test7 - VectorVariableState", "");
        Instruction test7instruction = new Instruction(test7.name, interfaces);
        test7instruction.SetInfo("Title", test7.name);
        test7instruction.SetInfo("Narration", "Press Z button.");
        test7instruction.SetInfo("Description", "Press Z button.");
        test7.AddInstruction(test7instruction);
        test7.before = new Condition(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), RelationalOperator.Equal);
        //test4.before = new Condition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test7);

        Tasc test8 = new Tasc("Test8 - InputDownState", "");
        Instruction test8instruction = new Instruction(test8.name, interfaces);
        test8instruction.SetInfo("Title", test8.name);
        test8instruction.SetInfo("Narration", "Press Z button.");
        test8instruction.SetInfo("Description", "Press Z button.");
        test8.AddInstruction(test8instruction);
        test8.before = new Condition(new InputDownState(actor, (int)KeyCode.Z), RelationalOperator.Equal);
        scenario.Add(test8);

        Tasc test9 = new Tasc("Test9 - InputUpState", "");
        Instruction test9instruction = new Instruction(test9.name, interfaces);
        test9instruction.SetInfo("Title", test9.name);
        test9instruction.SetInfo("Narration", "Press Z button.");
        test9instruction.SetInfo("Description", "Press Z button.");
        test9.AddInstruction(test9instruction);
        test9.before = new Condition(new InputUpState(actor, (int)KeyCode.Z), RelationalOperator.Equal);
        scenario.Add(test9);

        Tasc test10 = new Tasc("Test10 - VariableDistanceState", "");
        Instruction test10instruction = new Instruction(test10.name, interfaces);
        test10instruction.SetInfo("Title", test10.name);
        test10instruction.SetInfo("Narration", "Press Z button.");
        test10instruction.SetInfo("Description", "Press Z button.");
        test10.AddInstruction(test10instruction);
        test10.before = new Condition(new VariableDistanceState(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), 0.1f), RelationalOperator.SmallerOrEqual);
        scenario.Add(test10);

        Tasc test11 = new Tasc("Test11 - DistanceState", "");
        Instruction test11instruction = new Instruction(test11.name, interfaces);
        test11instruction.SetInfo("Title", test11.name);
        test11instruction.SetInfo("Narration", "Move forward.");
        test11instruction.SetInfo("Description", "Move forward.");
        test11.AddInstruction(test11instruction);
        test11.before = new Condition(new DistanceState(new MoveState(actor), new MoveState(testTerminus), 3.0f), RelationalOperator.SmallerOrEqual);
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
        //introduction.before = new Condition(new TascState(introduction, TascProgressState.Started), Condition.Operator.Equal, new TimeState(0,0,1));
        introduction.before = new Condition(new Condition(new TascState(introduction, TascProgressState.Started), Condition.RelationalOperator.Equal, new TimeState(0, 0, 1)),
            Condition.LogicalOperator.And,
            new Condition(new DistanceState(new MoveState(testTerminus), new MoveState(actor), 1.5f), Condition.RelationalOperator.SmallerOrEqual, new TimeState(0, 0, 1)));
        */

        Tasc ending = new Tasc("Finish", "");
        Instruction endinginstruction = new Instruction(ending.name, interfaces);
        endinginstruction.SetInfo("Title", "Finish");
        endinginstruction.SetInfo("Narration", "Well done! Your training is successfully terminated.");
        endinginstruction.SetInfo("Description", "Well done! Your training is successfully terminated.");
        ending.AddInstruction(endinginstruction);
        ending.before = new Condition(new InputUpState(actor, (int)KeyCode.C), RelationalOperator.Equal);
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
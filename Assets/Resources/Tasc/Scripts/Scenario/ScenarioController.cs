using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TascUnity;

public class ScenarioController : MonoBehaviour
{
    ProceduralScenario scenario = new ProceduralScenario("Test1", "Test scenario for a unit test");
    public List<Interface> interfaceList;
    private Dictionary<string, Interface> interfaceDict;
    public Actor actor;

    // Use this for initialization
    void Start()
    {
        InitializeScenario();
    }

    void InitializeScenario()
    {
        initializeInterfaceDictionary();
        MakeTestScenario();
    }

    void initializeInterfaceDictionary()
    {
        interfaceDict = new Dictionary<string, Interface>();
        for (int i = 0; i < interfaceList.Count; i++)
        {
            interfaceDict.Add(interfaceList[i].name, interfaceList[i]);
        }
    }

    void MakeTestScenario()
    {
        TestTerminus testTerminus = GameObject.Find("TestTerminus").GetComponent<TestTerminus>();

        actor = FindObjectsOfType<Actor>()[0];
        if (actor == null)
            throw new Exception("An actor should be assigned first!");

        for(int i=0; i< interfaceList.Count; i++)
        {
            if(interfaceList[i])
                interfaceList[i].Activate();
        }

        Tasc test1 = new Tasc("Test1 - TimeState", "");
        Instruction test1instruction = new Instruction(test1.name, interfaceDict["Title"], new Information(Information.Modality.Text, test1.name));
        test1instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, test1.name));
        test1instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, test1.name));
        test1.AddInstruction(test1instruction);
        test1.before = new SingleCondition(new TimeState(0, 0, 1), RelationalOperator.Larger);
        scenario.Add(test1);

        Tasc test2 = new Tasc("Test2 - TascState", "");
        Instruction test2instruction = new Instruction(test2.name, interfaceDict["Title"], new Information(Information.Modality.Text, test2.name));
        test2instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, test2.name));
        test2instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, test2.name));
        test2.AddInstruction(test2instruction);
        test2.before = new SingleCondition(new TascState(test2, TascProgressState.Started), RelationalOperator.Equal);
        scenario.Add(test2);

        Tasc test3 = new Tasc("Test3 - TascState + TimeState", "");
        Instruction test3instruction = new Instruction(test3.name, interfaceDict["Title"], new Information(Information.Modality.Text, test3.name));
        test3instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, test3.name));
        test3instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, test3.name));
        test3.AddInstruction(test3instruction);
        test3.before = new SingleCondition(new TascState(test3, TascProgressState.Started), RelationalOperator.Equal, new TimeState(0, 0, 1));
        scenario.Add(test3);

        Tasc test4 = new Tasc("Test4 - BoolVariableState", "");
        Instruction test4instruction = new Instruction(test4.name, interfaceDict["Title"], new Information(Information.Modality.Text, test4.name));
        test4instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test4instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test4.AddInstruction(test4instruction);
        test4.before = new SingleCondition(new AutoVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        //test4.before = new SingleCondition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test4);

        Tasc test5 = new Tasc("Test5 - IntVariableState", "");
        Instruction test5instruction = new Instruction(test5.name, interfaceDict["Title"], new Information(Information.Modality.Text, test5.name));
        test5instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test5instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test5.AddInstruction(test5instruction);
        test5.before = new SingleCondition(new AutoVariableState(testTerminus, "incrementValue_int", 2), RelationalOperator.LargerOrEqual);
        //test4.before = new SingleCondition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test5);

        Tasc test6 = new Tasc("Test6 - FloatVariableState", "");
        Instruction test6instruction = new Instruction(test6.name, interfaceDict["Title"], new Information(Information.Modality.Text, test6.name));
        test6instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test6instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test6.AddInstruction(test6instruction);
        test6.before = new SingleCondition(new AutoVariableState(testTerminus, "incrementValue", 1.5f), RelationalOperator.LargerOrEqual);
        //test4.before = new SingleCondition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test6);

        Tasc test7 = new Tasc("Test7 - VectorVariableState", "");
        Instruction test7instruction = new Instruction(test7.name, interfaceDict["Title"], new Information(Information.Modality.Text, test7.name));
        test7instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test7instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test7.AddInstruction(test7instruction);
        test7.before = new SingleCondition(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), RelationalOperator.Equal);
        //test4.before = new SingleCondition(new BoolVariableState(testTerminus, "isPushed", true), RelationalOperator.Equal);
        scenario.Add(test7);

        Tasc test8 = new Tasc("Test8 - InputDownState", "");
        Instruction test8instruction = new Instruction(test8.name, interfaceDict["Title"], new Information(Information.Modality.Text, test8.name));
        test8instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test8instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test8.AddInstruction(test8instruction);
        test8.before = new SingleCondition(new InputDownState(actor, (int)KeyCode.Z), RelationalOperator.Equal);
        scenario.Add(test8);

        Tasc test9 = new Tasc("Test9 - InputUpState", "");
        Instruction test9instruction = new Instruction(test9.name, interfaceDict["Title"], new Information(Information.Modality.Text, test9.name));
        test9instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test9instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test9.AddInstruction(test9instruction);
        test9.before = new SingleCondition(new InputUpState(actor, (int)KeyCode.Z), RelationalOperator.Equal);
        scenario.Add(test9);

        Tasc test10 = new Tasc("Test10 - VariableDistanceState", "");
        Instruction test10instruction = new Instruction(test10.name, interfaceDict["Title"], new Information(Information.Modality.Text, test10.name));
        test10instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Press Z button."));
        test10instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Press Z button."));
        test10.AddInstruction(test10instruction);
        test10.before = new SingleCondition(new VariableDistanceState(new VectorVariableState(testTerminus, "incrementVector", Vector3.zero), 0.1f), RelationalOperator.SmallerOrEqual);
        scenario.Add(test10);

        Tasc test11 = new Tasc("Test11 - DistanceState", "");
        Instruction test11instruction = new Instruction(test11.name, interfaceDict["Title"], new Information(Information.Modality.Text, test11.name));
        test11instruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Move forward."));
        test11instruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Move forward."));
        test11.AddInstruction(test11instruction);
        test11.before = new SingleCondition(new DistanceState(new MoveState(actor), new MoveState(testTerminus), 3.0f), RelationalOperator.SmallerOrEqual);
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
        //introduction.before = new SingleCondition(new TascState(introduction, TascProgressState.Started), SingleCondition.Operator.Equal, new TimeState(0,0,1));
        introduction.before = new SingleCondition(new SingleCondition(new TascState(introduction, TascProgressState.Started), SingleCondition.RelationalOperator.Equal, new TimeState(0, 0, 1)),
            SingleCondition.LogicalOperator.And,
            new SingleCondition(new DistanceState(new MoveState(testTerminus), new MoveState(actor), 1.5f), SingleCondition.RelationalOperator.SmallerOrEqual, new TimeState(0, 0, 1)));
        */

        Tasc ending = new Tasc("Finish", "");
        Instruction endinginstruction = new Instruction(ending.name, interfaceDict["Title"], new Information(Information.Modality.Text, ending.name));
        endinginstruction.Add(interfaceDict["Description"], new Information(Information.Modality.Text, "Well done! Your training is successfully terminated."));
        endinginstruction.Add(interfaceDict["Narration"], new Information(Information.Modality.Audio, "Well done! Your training is successfully terminated."));
        ending.AddInstruction(endinginstruction);
        ending.before = new SingleCondition(new InputUpState(actor, (int)KeyCode.C), RelationalOperator.Equal);
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
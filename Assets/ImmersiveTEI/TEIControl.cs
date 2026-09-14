using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum LinkWidgets
{
    off,
    on
}

public enum DrawSlider
{
    off,
    on
}

public enum MoveButton
{
    off,
    on
}

public enum CreatePlane
{
    off,
    on
}

public enum CreateWritingBoard
{
    off,
    on
}

public enum CustomizedSlider
{
    off,
    on
}

public enum MoveNotepad
{
    off,
    on
}

public enum GestureGenerate
{
    off,
    on
}

public class TEICondition
{
    
    public LinkWidgets linkwidgets;
    public DrawSlider drawslider;
    public CustomizedSlider customizedslider;
    public MoveButton movebutton;
    public CreatePlane createplane;
    public CreateWritingBoard createwritingboard;
    public MoveNotepad movenotepad;
    public GestureGenerate gesturegenerate;
}


public class TEIControl : MonoBehaviour
{
    [SerializeField] GameObject Content;
    List<Tuple<string, Action<GameObject>>> featureList;
    TEICondition teicondition = new TEICondition();
    List<Type> conditionEnums = new List<Type>();
    LineManager linemanager;
    DrawLine drawLine;

    public bool LinkingWidgets = false;
    public bool DrawingSlider = false;
    public bool CustomizingSlider = false;
    public bool MovingButton = false;
    public bool DrawingPlane = false;
    public bool CreatingWritingBoard = false;
    public bool MovingNotepad = false;
    public bool GestureGenerating = false;

    


    // Start is called before the first frame update
    void Start()
    {
        linemanager = GameObject.FindGameObjectWithTag("Line").GetComponent<LineManager>();
        drawLine = GameObject.FindGameObjectWithTag("Line").GetComponent<DrawLine>();
        InitUI();  
    }

    // Update is called once per frame
    void Update()
    {
        GetControlCondition();
    }


    void InitUI()
    {
        conditionEnums.Add(typeof(LinkWidgets));
        conditionEnums.Add(typeof(DrawSlider));
        conditionEnums.Add(typeof(CustomizedSlider));
        conditionEnums.Add(typeof(MoveButton));
        conditionEnums.Add(typeof(CreatePlane));
        conditionEnums.Add(typeof(CreateWritingBoard));
        conditionEnums.Add(typeof(MoveNotepad));
        conditionEnums.Add(typeof(GestureGenerate));


        featureList = new List<Tuple<string, Action<GameObject>>>();
        featureList.Add(new Tuple<string, Action<GameObject>>("DeleteAllLines", DeleteLinkedLines));
        featureList.Add(new Tuple<string, Action<GameObject>>("GenerateButton", GenerateButton));
        featureList.Add(new Tuple<string, Action<GameObject>>("GenerateNotepad", GenerateNotpad));



        foreach (Type type in conditionEnums)
        {
            GameObject ui = Instantiate((GameObject)Resources.Load("UIConditionOptionPrefeb",
                typeof(GameObject)), Content.transform);
            ui.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = type.Name;

            List<GameObject> conditionBtnList = new List<GameObject>();

            foreach (int i in Enum.GetValues(type))
            {
                GameObject uibtn = Instantiate((GameObject)Resources.Load("UIBtn",
                    typeof(GameObject)), ui.transform);
                uibtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Enum.GetName(type, i);

                // init the btn toggle state 
                if ((int)teicondition.GetType().GetField(type.Name.ToLower()).GetValue(teicondition) == i)
                    uibtn.transform.Find("ToggleState").gameObject.SetActive(true);
                else
                    uibtn.transform.Find("ToggleState").gameObject.SetActive(false);

                // call back 
                uibtn.GetComponent<Button>().onClick.AddListener(delegate {
                    teicondition.GetType().GetField(type.Name.ToLower()).SetValue(teicondition, i);// cast?
                    uibtn.transform.Find("ToggleState").gameObject.SetActive(true);
                    ShowCondition();
                });
                conditionBtnList.Add(uibtn);
            }

            foreach (GameObject currentBtn in conditionBtnList)// disable the other btns 
            {
                currentBtn.GetComponent<Button>().onClick.AddListener(delegate {
                    foreach (GameObject btn in conditionBtnList)
                    {
                        if (!btn.Equals(currentBtn))
                        {
                            btn.transform.Find("ToggleState").gameObject.SetActive(false);
                        }
                    }
                });
            }


        }
        foreach (var t in featureList)
        {
            GameObject ui = Instantiate((GameObject)Resources.Load("DebugUITextedBtnPrefeb", typeof(GameObject)), Content.transform);
            ui.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = t.Item1;
            ui.transform.Find("Btn").Find("ToggleState").gameObject.SetActive(false);
            ui.transform.Find("Btn").GetComponent<Button>().onClick.AddListener(delegate { t.Item2(ui); });
        }
    }

    void ShowCondition()
    {
        foreach (FieldInfo fi in teicondition.GetType().GetFields())
        {
            ixUtility.LogMe(fi.GetValue(teicondition).ToString());
        }
    }

    void GetControlCondition()
    {
        void LogMe(string msg)
        {
            if (GameObject.FindGameObjectWithTag("LogInfo"))
                GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                    .AppendText(msg);
        }

        List<string> ControlCondition = new List<string>();
        foreach (FieldInfo fi in teicondition.GetType().GetFields())
        {
            ControlCondition.Add(fi.GetValue(teicondition).ToString());
        }

        if (ControlCondition[0] == "on")
        {
            LinkingWidgets = true;
        }
        else if (ControlCondition[0] == "off")
        {
            LinkingWidgets = false;
        }

        if (ControlCondition[1] == "on")
        {
            DrawingSlider = true;
        }
        else if (ControlCondition[1] == "off")
        {
            DrawingSlider = false;
        }

        if (ControlCondition[2] == "on")
        {
            CustomizingSlider = true;
        }
        else if (ControlCondition[2] == "off")
        {
            CustomizingSlider = false;
        }

        if (ControlCondition[3] == "on")
        {
            MovingButton = true;
        }
        else if (ControlCondition[3] == "off")
        {
            MovingButton = false;
        }

        if (ControlCondition[4] == "on")
        {
            DrawingPlane = true;
        }
        else if (ControlCondition[4] == "off")
        {
            DrawingPlane = false;
        }

        if (ControlCondition[5] == "on")
        {
            CreatingWritingBoard = true;
        }
        else if (ControlCondition[5] == "off")
        {
            CreatingWritingBoard = false;
        }

        if (ControlCondition[6] == "on")
        {
            MovingNotepad = true;
        }
        else if (ControlCondition[6] == "off")
        {
            MovingNotepad = false;
        }

        if (ControlCondition[7] == "on")
        {
            GestureGenerating = true;
        }
        else if (ControlCondition[7] == "off")
        {
            GestureGenerating = false;
        }

    }

    void DeleteLinkedLines(GameObject ui)
    {
        linemanager.DeleteAllLines();
    }

    void GenerateButton(GameObject ui)
    {
        drawLine.GenerateButton();
    }

    void GenerateNotpad(GameObject ui)
    {
        drawLine.GenerateNotepad();
    }
}

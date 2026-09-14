using JfranMora.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public GameObject Button;
    public GameObject ButtonMenu;
    LineManager lineManager;
    GameObject Slot;
    GameObject Slot1;
    GameObject Slot2;
    GameObject ButtonMove;
    ExampleVis exampleVis;
    Rigidbody rb;
    public GameObject PressableBtn;
    public GameObject XRBtn;
    OVRHand oVRHand;
    ChangeTexture changeTexture;
    ScreenPrefabUIPanel screenPrefabUIPanel;
    VideoPlayerTest videoPlayerTest;
    public GameObject AdditionalInstruction;
    TEIControl teicontrol;


    // Start is called before the first frame update
    void Start()
    {
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        lineManager = GameObject.Find("Line").GetComponent<LineManager>();
        exampleVis = GameObject.FindGameObjectWithTag("ExampleVis")
            .GetComponent<ExampleVis>();
        Slot = GameObject.Find("ExampleVis/Slot");
        Slot1 = GameObject.Find("ExampleVis/Slot1");
        Slot2 = GameObject.Find("ExampleVis/Slot2");
        ButtonMove = GameObject.Find("XRConfigurableBtn");
        changeTexture = GameObject.Find("ScreenPrefab/Slides/Plane").GetComponent<ChangeTexture>();
        screenPrefabUIPanel = GameObject.Find("ScreenPrefab/UIContextMenu/ButtonCollection/UIPanel").GetComponent<ScreenPrefabUIPanel>();
        videoPlayerTest = GameObject.Find("ScreenPrefab").GetComponent<VideoPlayerTest>();
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
        var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
        float dist = Vector3.Distance(indexFingerTip, XRBtn.transform.position);
        if (oVRHand.Pinched == true && dist < 0.1f && teicontrol.MovingButton == true)
        {            
            XRBtn.transform.position = indexFingerTip;
            
        }
    }

    [Button]
    public void TestButton()
    {
        Debug.Log(Button.transform.Find("slot").transform.position.ToString());
        videoPlayerTest = GameObject.FindGameObjectWithTag("ScreenPrefab").GetComponent<VideoPlayerTest>();
        Debug.Log(videoPlayerTest.x);
        foreach(LineRenderer l in lineManager.Lines)
            if (Button.transform.Find("slot").transform.position == l.GetPosition(0))
            //if (Button.transform.Find("slot").transform.position == l.GetComponent<LineProperty>().Line.GetPosition(0))
            {
                if (l.GetComponent<LineProperty>().Line.GetPosition(1) == Slot.transform.position)
                {
                    exampleVis.DoBlink();
                }
                else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == Slot1.transform.position)
                {
                    exampleVis.DoDeform();
                }
                else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == Slot2.transform.position)
                {
                    exampleVis.StopVis();
                }
                else if(l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.FindGameObjectWithTag("ScreenPrefab").transform.Find("slot1").transform.position)
                {
                    //LogMe(GameObject.Find("ScreenPrefab/Screen").activeInHierarchy.ToString());
                    //if(GameObject.Find("ScreenPrefab/Slides").activeInHierarchy == true)
                    //{
                        changeTexture = GameObject.FindGameObjectWithTag("ScreenPrefab").transform.Find("Slides/Plane").GetComponent<ChangeTexture>();
                        //changeTexture = GameObject.Find("ScreenPrefab/Slides/Plane").GetComponent<ChangeTexture>();
                        changeTexture.PageMinus();
                    //}
                    if (GameObject.Find("ScreenPrefab/Screen").activeInHierarchy == true)
                    {
                        videoPlayerTest = GameObject.Find("ScreenPrefab").GetComponent<VideoPlayerTest>();
                        videoPlayerTest.VideoBackward();
                    }
                    
                }
                else if(l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.FindGameObjectWithTag("ScreenPrefab").transform.Find("slot2").transform.position)
                {
                    //if (GameObject.Find("ScreenPrefab/Slides").activeInHierarchy == true)
                    //{
                        changeTexture = GameObject.FindGameObjectWithTag("ScreenPrefab").transform.Find("Slides/Plane").GetComponent<ChangeTexture>();
                        changeTexture.PagePlus();
                    //}
                    if (GameObject.Find("ScreenPrefab/Screen").activeInHierarchy == true)
                    {
                        videoPlayerTest = GameObject.Find("ScreenPrefab").GetComponent<VideoPlayerTest>();
                        videoPlayerTest.VideoForward();
                    }
                }
                else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.FindGameObjectWithTag("ScreenPrefab").transform.Find("slot3").transform.position)
                {
                    videoPlayerTest = GameObject.Find("ScreenPrefab").GetComponent<VideoPlayerTest>();
                    videoPlayerTest.VideoPause();
                }

            }

    }

    [Button]
    public void ButtonConfirm()
    {
        PressableBtn.SetActive(true);
        //GameObject.Find("XRConfigurableBtn/Button/BottonCollection/PressableButton").SetActive(true);
        //rb = GameObject.Find("XRConfigurableBtn/BlackPlate").GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezePositionX;
        //rb.constraints = RigidbodyConstraints.FreezePositionY;
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }


    [Button]
    public void ButtonDelete()
    {
        Destroy(XRBtn, 0);
    }

    public void ActiveInstruction()
    {
        if(AdditionalInstruction.active == false)
        {
            AdditionalInstruction.SetActive(true);
        }
        else
        {
            AdditionalInstruction.SetActive(false);
        }
        
    }

    void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }


    
}

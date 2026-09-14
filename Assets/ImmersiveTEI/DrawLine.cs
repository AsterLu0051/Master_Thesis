using Oculus.Interaction.Input;
using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using JfranMora.Inspector;
using Unity.SharpZipLib;

public class DrawLine : MonoBehaviour
{
    public GameObject SliderPrefeb;
    private LineRenderer currLine;
    private int numClicks;
    //public OVRHand _OVRHand;
    OVRHand oVRHand;
    TEIControl teicontrol;
    private int X;

    private bool _isTrue;

    private bool isLine;
    public bool isTrue;
    public GameObject XRBtn;
    public GameObject NotepadPrefab;
    public GameObject ControlPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        X = 0;
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
        isTrue = oVRHand.Pinched;
        isLine = false;
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();


    }

    // Update is called once per frame
    void Update()
    {
        
        
        

        if (Input.GetMouseButtonDown(0))
        {
            var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
            GameObject go = new GameObject();
            currLine = go.AddComponent<LineRenderer>();
            currLine.startWidth = 0.01f;
            numClicks = 0;
            //currLine.positionCount = 0;
            currLine.SetPosition(0, indexFingerTip);
        }
        else if (Input.GetMouseButton(0))
        {
            currLine.endWidth = 0.01f;
            currLine.positionCount = numClicks + 1;
            var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
            currLine.SetPosition(numClicks, indexFingerTip);
            numClicks++;
        }

        

        if ((oVRHand.Pinched == true) && (isLine == false) && (teicontrol.DrawingSlider == true))
        {
            var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
            
            GameObject go = new GameObject();
            currLine = go.AddComponent<LineRenderer>();
            currLine.startWidth = 0.01f;
            numClicks = 0;
            //currLine.positionCount = 0;
            currLine.SetPosition(0, indexFingerTip);
            
            isLine = true;
        }
        else if((oVRHand.Pinched == true) && (isLine == true) && (teicontrol.DrawingSlider == true))
        {
            currLine.endWidth = 0.01f;
            currLine.positionCount = numClicks + 1;
            var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
            currLine.SetPosition(numClicks, indexFingerTip);
            numClicks++;
        }
        else if((oVRHand.Pinched == false) && (isLine == true) && (teicontrol.DrawingSlider == true))
        {
            var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
            GameObject slider = Instantiate(SliderPrefeb, new Vector3(0, 0, 0), Quaternion.identity);
            

            Vector3 MiddlePosition = new Vector3((indexFingerTip.x + currLine.GetPosition(0).x)/2, (indexFingerTip.y + currLine.GetPosition(0).y) / 2, (indexFingerTip.z + currLine.GetPosition(0).z) / 2);
            
            slider.transform.position = MiddlePosition;
            slider.transform.position = slider.transform.position;


            float Hyp = Vector3.Distance(currLine.GetPosition(0), indexFingerTip);

            slider.transform.Find("SliderTrackSimple").transform.localScale = new Vector3(Hyp * 4, 0.5f, 0.5f);

            float OppY = indexFingerTip.y - currLine.GetPosition(0).y;
            float OppZ = indexFingerTip.z - currLine.GetPosition(0).z;
            float OppX = indexFingerTip.x - currLine.GetPosition(0).x;

            double RadianZ = Math.Atan(OppY / OppX);
            float AngleZ = (float)(RadianZ / Math.PI * 180);

            double RadianY = Math.Atan(OppZ / OppX);
            float AngleY = (float)(RadianY / Math.PI * 180);

            Vector3 Angle = new Vector3(0f, -AngleY, AngleZ);

            slider.transform.rotation = Quaternion.Euler(Angle);
            slider.transform.rotation = slider.transform.rotation;

            //slider.transform.rotation = GameObject.Find("XRConfigurableBtn").transform.rotation; ;
            //LogMe("line" + currLine.GetPosition(0).ToString());
            DestroyImmediate(currLine);
            isLine = false;
        }
    }

    

    public void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }

    [Button]
    void CreateTest()
    {
        GameObject slider = Instantiate(SliderPrefeb, new Vector3(0,0,0), Quaternion.identity);
        GameObject Cubefrom = GameObject.Find("Cubefrom");
        GameObject Cubeto = GameObject.Find("Cubeto");
        Vector3 MiddlePosition = new Vector3((Cubefrom.transform.position.x + Cubeto.transform.position.x) / 2, (Cubefrom.transform.position.y + Cubeto.transform.position.y) / 2, (Cubefrom.transform.position.z + Cubeto.transform.position.z) / 2);
        LogMe(MiddlePosition.ToString());
        slider.transform.position = MiddlePosition;
        slider.transform.position = slider.transform.position;


        float Hyp = Vector3.Distance(Cubefrom.transform.position, Cubeto.transform.position);

        
        slider.transform.Find("SliderTrackSimple").transform.localScale = new Vector3(Hyp * 4 ,0.5f,0.5f);

        float OppY = Cubeto.transform.position.y - Cubefrom.transform.position.y;
        float OppZ = Cubeto.transform.position.z - Cubefrom.transform.position.z;
        float OppX = Cubeto.transform.position.x - Cubefrom.transform.position.x;
        
        
        double RadianZ = Math.Atan(OppY / OppX);
        float AngleZ = (float)(RadianZ / Math.PI * 180);

        double RadianY = Math.Atan(OppZ / OppX);
        float AngleY = (float)(RadianY / Math.PI * 180);
        

        Vector3 Angle = new Vector3(0f,-AngleY, AngleZ);
        

        slider.transform.rotation = Quaternion.Euler(Angle);
        slider.transform.rotation = slider.transform.rotation;
    }

    public void GenerateButton()
    {
        GameObject btn = GameObject.Instantiate(XRBtn, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void GenerateNotepad()
    {
        GameObject note = GameObject.Instantiate(NotepadPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    [Button]
    public void ActivateControlPanel()
    {
        if (ControlPanel.activeInHierarchy == false)
        {
            ControlPanel.SetActive(true);
        }
        else
        {
            ControlPanel.SetActive(false);
        }
    }
}

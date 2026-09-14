using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UIElements;
using JfranMora.Inspector;
using Unity.SharpZipLib;
using System;

public class DrawLine2 : MonoBehaviour
{

    public GameObject SliderPrefeb;
    private LineRenderer currLine;
    private int numClicks;
    //public OVRHand _OVRHand;
    OVRHand oVRHand;
    TEIControl teicontrol;
    private int X;
    LineManager lineManager;

    private bool _isTrue;

    private bool isLine;
    public bool isTrue;
    public List<LineRenderer> CustomizedSliderTrack;




    // Start is called before the first frame update
    void Start()
    {
        CustomizedSliderTrack = new List<LineRenderer>();
        lineManager = GameObject.Find("Line").GetComponent<LineManager>();
        
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        X = 0;
        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
        isTrue = oVRHand.Pinched;
        isLine = false;
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // (teicontrol.DrawingSlider == true)
        if ((oVRHand.Pinched == true) && (isLine == false) && (teicontrol.CustomizingSlider == true))
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
        else if ((oVRHand.Pinched == true) && (isLine == true) && (teicontrol.CustomizingSlider == true))
        {
            currLine.endWidth = 0.01f;
            currLine.positionCount = numClicks + 1;
            var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
            currLine.SetPosition(numClicks, indexFingerTip);
            numClicks++;
        }
        else if ((oVRHand.Pinched == false) && (isLine == true) && (teicontrol.CustomizingSlider == true))
        {
            var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
            int i = (int)Math.Floor((double)numClicks / 2);
            GameObject slider = Instantiate(SliderPrefeb, currLine.GetPosition(0), Quaternion.identity);
            CustomizedSliderTrack.Add(currLine);
            isLine = false;
        }
    }

    void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }
}

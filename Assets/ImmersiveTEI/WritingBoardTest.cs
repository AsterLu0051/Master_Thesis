using JfranMora.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingBoardTest : MonoBehaviour
{
    Bounds WritingBoardBounds;
    private LineRenderer currLine;
    private int numClicks;
    OVRHand oVRHand;
    private int X;
    private bool _isTrue;
    private bool isLine;
    List<LineRenderer> WrittenLines;
    public GameObject WriteBoard;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        WrittenLines = new List<LineRenderer>();
        WritingBoardBounds.center = this.transform.position;
        WritingBoardBounds.extents = new Vector3(0.03f,0.25f,0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        var indexFingerTipR = GameObject.Find("r_index_finger_tip_marker").transform.position;
        //var indexFingerTipL = GameObject.Find("l_index_finger_tip_marker").transform.position;
        bool WritingIsInsideTheBox = WritingBoardBounds.Contains(indexFingerTipR);

        if ((WritingIsInsideTheBox == true) && (isLine == false))
        {
            GameObject go = new GameObject();
            currLine = go.AddComponent<LineRenderer>();
            currLine.startWidth = 0.01f;
            numClicks = 0;
            //currLine.positionCount = 0;
            currLine.SetPosition(0, indexFingerTipR);
            WrittenLines.Add(currLine);
            isLine = true;
        }
        else if ((WritingIsInsideTheBox == true) && (isLine == true))
        {
            currLine.endWidth = 0.01f;
            currLine.positionCount = numClicks + 1;
            currLine.SetPosition(numClicks, indexFingerTipR);
            numClicks++;
        }
        else if ((WritingIsInsideTheBox == false) && (isLine == true))
        {
            isLine = false;
        }

        var PalmL = GameObject.Find("l_index_finger_tip_marker").transform.position;
        //var PalmL = GameObject.Find("l_index_palm_knuckle_marker").transform.position;
        float dist = Vector3.Distance(PalmL, WriteBoard.transform.position);
        
        //if (oVRHand.Pinched == true)
        //{
            //LogMe("true");
            //WriteBoard.transform.position = PalmL;
            //WritingBoardBounds.center = this.transform.position;
        //}
        
    }

    public void ClearWritingBoard()
    {
        foreach (LineRenderer l in WrittenLines)
        {
            Destroy(l);
        }
        WrittenLines.Clear();
    }

    void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }
}

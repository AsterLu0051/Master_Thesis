using JfranMora.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Linq;

public class LineManager : MonoBehaviour
{

    GameObject BeginClickGameObejct;
    GameObject EndClickGameObejct;
    private GameObject[] StartSlots;
    private GameObject[] EndSlots;
    public GameObject LinePrefab;
    public Material sourceMat;
    LineProperty lineProperty;
    TEIControl teicontrol;
    OVRHand oVRHand;
    public Material UIDefaultMat;



    //GameObject LineNode;
    GameObject Slot;
    GameObject Slot1;
    GameObject Slot2;
    ExampleVis exampleVis;

    public List<LineRenderer> Lines;
    LineRenderer thisLine;


    [SerializeField]
    private Vector3 mousePos;
    private Vector3 startPos;   // Start position of line
    private Vector3 endPos;
    private bool isLinked;
    private bool isLine;
    private bool _isTrue;
    private bool allIsLinked;
    private int numClicks;
    
    
    
    

    void Start()
    {
        
        exampleVis = GameObject.FindGameObjectWithTag("ExampleVis")
            .GetComponent<ExampleVis>();
        Slot = GameObject.Find("ExampleVis/Slot");
        Slot1 = GameObject.Find("ExampleVis/Slot1");
        Slot2 = GameObject.Find("ExampleVis/Slot2");
        
        Lines = new List<LineRenderer>();
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
        isLine = false;
    }

    void Update()
    {
        StartSlots = GameObject.FindGameObjectsWithTag("StartSlot");
        EndSlots = GameObject.FindGameObjectsWithTag("EndSlot");
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();

        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;

        if (StartSlots.Count() == Lines.Count())
        {
            allIsLinked = true;
        }

        

        //if (oVRHand.Pinched == true && isLine == false && teicontrol.LinkingWidgets == true && allIsLinked == false)
        if (oVRHand.Pinched == true && isLine == false && teicontrol.LinkingWidgets == true)
        {
            //LogMe("intrue");
            GameObject line = Instantiate(LinePrefab, new Vector3(0f,0f,0f),Quaternion.identity);
            lineProperty = line.GetComponent<LineProperty>();
            thisLine = lineProperty.Line;
            thisLine.material = UIDefaultMat;
            Lines.Add(thisLine);
            //thisLine.GetComponent<MeshRenderer>().material = UIDefaultMat;
            startPos = indexFingerTip;
            endPos = new Vector3(5, 5, 5);
            FindNearestStartSlot();
            thisLine.SetPosition(0, BeginClickGameObejct.transform.position);
            thisLine.SetPosition(1, indexFingerTip);
            isLine = true;
        }
        //else if ((oVRHand.Pinched == true) && (isLine == true) && (teicontrol.LinkingWidgets == true))
        else if ((oVRHand.Pinched == true) && (isLine == true) && (teicontrol.LinkingWidgets == true))
        {
            
            thisLine.SetPosition(1, indexFingerTip);
        }
        else if ((oVRHand.Pinched == false) && (isLine == true) && (teicontrol.LinkingWidgets == true))
        //else if ((oVRHand.Pinched == false) && (isLine == true) && (teicontrol.LinkingWidgets == true))
        {
            FindNearestEndSlot();
            thisLine.SetPosition(1, EndClickGameObejct.transform.position);
            isLine = false;
        }




        if (Input.GetMouseButtonDown(0))
        {
            
            //isTrue = true;
            LinkLine();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            startPos = mousePos;
            endPos = new Vector3(5,5,5);
            FindNearestStartSlot();
            
            thisLine.SetPosition(0, BeginClickGameObejct.transform.position);
            thisLine.SetPosition(1, mousePos);

            //FindNearestStartSlot();


        }
        else if (Input.GetMouseButtonUp(0))
        {
            FindNearestEndSlot();
            thisLine.SetPosition(1, EndClickGameObejct.transform.position);
        }
        else if (Input.GetMouseButton(0))
        {
            if (thisLine)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                thisLine.SetPosition(1, mousePos);
            }
        }
    }




    [Button]
    void Link()
    {
        LinkLine();
    }

    [Button]
    public void Execute()
    {

        foreach(LineRenderer l in Lines)
        {
            if (EndClickGameObejct == Slot)
            {
                exampleVis.DoBlink();
            }
            else if (EndClickGameObejct == Slot1)
            {
                exampleVis.DoDeform();
            }
            else if (EndClickGameObejct == Slot2)
            {
                exampleVis.StopVis();
            }
     
        }

        
    }

    [Button]
    public void DeleteAllLines()
    {
        foreach (LineRenderer l in Lines)
        {
            Destroy(l);  
        }
        Lines.Clear();
    }

    void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }


    public void LinkLine()
    {
        GameObject line = Instantiate(LinePrefab, this.transform);
        lineProperty = line.GetComponent<LineProperty>();
        thisLine = lineProperty.Line;
        Lines.Add(thisLine);
            
    }

    public void FindNearestStartSlot()
    {
        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
        float minDist = 2.0f;
        GameObject NearestStartSlot = gameObject;
        foreach (GameObject ss in StartSlots)
        {
            isLinked = false;
            foreach(LineRenderer l in Lines)
            {
                if(l.GetComponent<LineProperty>().Line.GetPosition(0) == ss.transform.position)
                {
                    isLinked = true;
                }
                
            }
            float dist = Vector3.Distance(indexFingerTip, ss.transform.position);
            LogMe("Current Dist = " + dist + " minDist = " + minDist);
            if ((dist < minDist) && (isLinked == false))
            {
                minDist = dist;
                NearestStartSlot = ss;
            }
        }
        BeginClickGameObejct = NearestStartSlot;
    }

    public void FindNearestEndSlot()
    {
        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
        float minDist = 2.0f;
        GameObject NearestEndSlot = gameObject;
        foreach (GameObject ss in EndSlots)
        {
            float dist = Vector3.Distance(indexFingerTip, ss.transform.position);
            LogMe("Current Dist = " + dist + " minDist = " + minDist);
            if (dist < minDist)
            {
                minDist = dist;
                NearestEndSlot = ss;
            }
        }
        EndClickGameObejct = NearestEndSlot;
    }

}




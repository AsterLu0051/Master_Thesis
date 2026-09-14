using Oculus.Interaction.Input;
using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using JfranMora.Inspector;
using Unity.SharpZipLib;
using UnityEngine.Device;
using UnityEngine.UIElements;
using Microsoft.MixedReality.Toolkit;

public class DrawPlane : MonoBehaviour
{
    public GameObject ScreenPrefeb;
    public GameObject WritingBoardPrefab;
    OVRHand oVRHand;
    TEIControl teicontrol;
    bool isPlane;
    Transform playerEyePose;
    Vector3 StartPosition;
    Vector3 EndPosition;
    GameObject SamplePlane;
    OVRCameraRig overCameraRig;
    public Material UIDefaultMat;


    // Start is called before the first frame update
    void Start()
    {
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
        isPlane = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
        
        playerEyePose = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform;

        if (oVRHand.Pinched == true && isPlane == false && teicontrol.DrawingPlane == true)
        {
            SamplePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            SamplePlane.transform.position = new Vector3(0,0,0);
            SamplePlane.GetComponent<MeshRenderer>().material = UIDefaultMat;
            StartPosition = indexFingerTip;
            StartPosition = StartPosition;
            SamplePlane.transform.rotation = Quaternion.identity;

            SamplePlane.transform.Rotate(-90, 0, 0);

            //Vector3 direction = (playerEyePose.transform.position - SamplePlane.transform.position).normalized;
            //SamplePlane.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            //SamplePlane.transform.rotation = SamplePlane.transform.rotation;
            SamplePlane.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            isPlane = true;
        }
        else if (oVRHand.Pinched == true && isPlane == true && teicontrol.DrawingPlane == true)
        {
            Vector3 MiddlePosition = new Vector3((indexFingerTip.x + StartPosition.x) / 2, (indexFingerTip.y + StartPosition.y) / 2, (indexFingerTip.z + StartPosition.z) / 2);
            SamplePlane.transform.position = MiddlePosition;
            float Hyp = Vector3.Distance(StartPosition, indexFingerTip);
            float Length = (float)(Hyp / System.Math.Sqrt(2));
            SamplePlane.transform.localScale = new Vector3(Length/10, Length/10, Length/10);
        }
        else if (oVRHand.Pinched == false && isPlane == true && teicontrol.DrawingPlane == true)
        {
            GameObject screen = Instantiate(ScreenPrefeb, new Vector3(0, 0, 0), Quaternion.identity);
            screen.transform.position = SamplePlane.transform.position;
            screen.transform.position = screen.transform.position;
            screen.transform.rotation = SamplePlane.transform.rotation;
            screen.transform.rotation = screen.transform.rotation;

            screen.transform.Rotate(90, 0, 0);

            //screen.transform.Rotate(90,0,-90);
            screen.transform.localScale = new Vector3(SamplePlane.transform.localScale.x * 24, SamplePlane.transform.localScale.y * 24, SamplePlane.transform.localScale.z);
            screen.transform.localScale = screen.transform.localScale;
            DestroyImmediate(SamplePlane);
            isPlane = false;
        }

        


        if (oVRHand.Pinched == true && isPlane == false && teicontrol.CreatingWritingBoard == true)
        {
            SamplePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            SamplePlane.transform.position = new Vector3(0, 0, 0);
            StartPosition = indexFingerTip;
            StartPosition = StartPosition;
            SamplePlane.transform.rotation = Quaternion.identity;

            SamplePlane.transform.Rotate(-90, 0, 0);

            //Vector3 direction = (playerEyePose.transform.position - SamplePlane.transform.position).normalized;
            //SamplePlane.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            //SamplePlane.transform.rotation = SamplePlane.transform.rotation;
            SamplePlane.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            isPlane = true;
        }
        else if (oVRHand.Pinched == true && isPlane == true && teicontrol.CreatingWritingBoard == true)
        {
            Vector3 MiddlePosition = new Vector3((indexFingerTip.x + StartPosition.x) / 2, (indexFingerTip.y + StartPosition.y) / 2, (indexFingerTip.z + StartPosition.z) / 2);
            SamplePlane.transform.position = MiddlePosition;
            float Hyp = Vector3.Distance(StartPosition, indexFingerTip);
            float Length = (float)(Hyp / System.Math.Sqrt(2));
            SamplePlane.transform.localScale = new Vector3(Length / 10, Length / 10, Length / 10);
        }
        else if (oVRHand.Pinched == false && isPlane == true && teicontrol.CreatingWritingBoard == true)
        {
            GameObject board = Instantiate(WritingBoardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            board.transform.position = SamplePlane.transform.position;
            board.transform.position = board.transform.position;
            board.transform.rotation = SamplePlane.transform.rotation;
            board.transform.rotation = board.transform.rotation;

            board.transform.Rotate(90, 0, 0);

            //screen.transform.Rotate(90, 0, -90);
            board.transform.localScale = new Vector3(SamplePlane.transform.localScale.x * 24, SamplePlane.transform.localScale.y * 24, SamplePlane.transform.localScale.z);
            board.transform.localScale = board.transform.localScale;
            DestroyImmediate(SamplePlane);
            isPlane = false;
        }

    }

    public void gestureGenerateWritingBoard()
    {
        Transform RightPalm = GameObject.Find("r_palm_center_marker").transform;
        GameObject screen = Instantiate(WritingBoardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        screen.transform.position = RightPalm.position;
        screen.transform.position = screen.transform.position;
        screen.transform.rotation = RightPalm.rotation;
        screen.transform.Rotate(90,0,0);
        screen.transform.Rotate(0, 0, 90);
        screen.transform.Rotate(0, 0, 180);
        screen.transform.rotation = screen.transform.rotation;

    }

    void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }

    [Button]
    public void TESTPlane()
    {
        SamplePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        SamplePlane.transform.position = new Vector3(0, 0, 0);
        SamplePlane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //StartPosition = indexFingerTip;
        //StartPosition = StartPosition;
        SamplePlane.transform.rotation = Quaternion.identity;
        SamplePlane.transform.Rotate(90, 0, 0);
    }

    [Button]
    public void SnapToSOI()
    {       
        List<GameObject> SnapSourceSois = new List<GameObject>();
        SnapSourceSois.AddRange(GameObject.FindGameObjectsWithTag("SoiPrefab"));
        float MinDist = float.MaxValue;
        float Dist = float.MaxValue;
        GameObject NearestSOI = ScreenPrefeb;
        
        foreach(GameObject sss in SnapSourceSois)
        {
            Dist = Vector3.Distance(sss.transform.position, ScreenPrefeb.transform.position);
            if(Dist < MinDist)
            {
                MinDist = Dist;
                NearestSOI = sss;
            }
        }

        ScreenPrefeb.transform.position = NearestSOI.transform.position;
        ScreenPrefeb.transform.rotation = NearestSOI.transform.rotation;
        ScreenPrefeb.transform.Find("Plane").transform.localScale = NearestSOI.transform.Find("SOIIllustrator/BoundingBox").transform.localScale;
        //ScreenPrefeb.transform.Find("Screen/Plane").transform.localScale = NearestSOI.transform.Find("SOIIllustrator/BoundingBox").transform.localScale;
        //ScreenPrefeb.transform.Find("Plane").transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
}

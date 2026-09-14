using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SnapToNearestSOI : MonoBehaviour
{
    public bool DoSnapWhenStarts = false;

    SpatialAnchorManager spatialAnchorManager;
    Transform SnapTarget;
    Transform mypose;
    Transform playerEyePose;
    GameObject MyPoseIndicator_NN;
    GameObject SAIndicator_NN;
    GameObject NearestSA_NN;
    GameObject SnapSource;
    List<Transform> SAPoseList;
    List<GameObject> SAIndicatorList;
    List<SnapData> snapDatas;
    List<GameObject> SOIs;
    UniversalSOIGenerator universalSOIGenerator;
    // Start is called before the first frame update
    void Start()
    {
        // get ref
        spatialAnchorManager = GameObject.FindGameObjectWithTag("SpatialAnchorManager")
            .GetComponent<SpatialAnchorManager>();
        SnapSource = GameObject.FindGameObjectWithTag("SnapSource");

        universalSOIGenerator = GameObject.Find("UniversalSOIGenerator").GetComponent<UniversalSOIGenerator>();

        //
        SAPoseList = new List<Transform>();
        SAIndicatorList = new List<GameObject>();
        snapDatas = new List<SnapData>();

        SOIs = new List<GameObject>();

        // init
        if (DoSnapWhenStarts)
            SnapOnce(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LogMe(string msg)
    {
        if (GameObject.FindGameObjectWithTag("LogInfo"))
            GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                .AppendText(msg);
    }

    bool EnableAutoSnap = true;

    public void UpdateAutoSnap(bool b)
    {
        EnableAutoSnap = b;
    }

    public void AutoSnapOnce()
    {
        if (EnableAutoSnap)
            SnapOnce();
    }

    public void SnapOnce()
    {
        Snap();
    }

    public void SnapOnce(float delay)
    {
        Invoke("Snap", delay);
    }

    //parameter class(addable)
    class SnapData
    {
        public Transform t;
        public float dist;
        public float angle;
        public SnapData(Transform t, float dist, float angle)
        {
            this.t = t;
            this.dist = dist;
            this.angle = angle;
        }

    }

    void Snap()
    {
        foreach (GameObject s in universalSOIGenerator.SOIs)
        {
            SOIs.Add(s);
        }

        if (SOIs == null) return;


        GameObject nearestSOI = gameObject;
        float minDist = float.MaxValue;
        //float minDist = 2.0f;

        mypose = this.transform;
        playerEyePose = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform;
        SnapSource.transform.position = this.transform.position; // change the snap source first 

        // visual: show my pose before align
        if (MyPoseIndicator_NN)
            MyPoseIndicator_NN.transform.position = this.transform.position;


        foreach (GameObject s in SOIs)
        {

            float dist = Vector3.Distance(SnapSource.transform.position, s.transform.position);

            LogMe("Current Dist = " + dist + " minDist = " + minDist);
            if (dist < minDist)
            {
                minDist = dist;
                nearestSOI = s;
            }
        }
        
        //
        this.transform.position = nearestSOI.transform.position;
        this.transform.rotation = nearestSOI.transform.rotation;

        // for the Panels that are not snapping at the beginning, do not move scene accordingly 
        if (DoSnapWhenStarts)
            UpdateCalibrationAcrossScene(nearestSOI.transform.position, nearestSOI.transform.rotation);

        // visual 
        if (NearestSA_NN)
            NearestSA_NN.transform.position = nearestSOI.transform.position;
    }

    void UpdateCalibrationAcrossScene(Vector3 p, Quaternion q)
    {
        // update sub scenes 
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("SceneGeometry"))
        {
            o.transform.position = p;
            o.transform.rotation = q;
        }
    }
}

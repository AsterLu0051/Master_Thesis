using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class SnapToNearestAOI : MonoBehaviour
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
    List<GameObject> AOIs;
    List<Transform> AOIAnchors;
    List<GameObject> temperPOIList;
    UniversalPOIGenerator universalPOIGenerator;

    // Start is called before the first frame update
    void Start()
    {
        // get ref
        spatialAnchorManager = GameObject.FindGameObjectWithTag("SpatialAnchorManager")
            .GetComponent<SpatialAnchorManager>();
        SnapSource = GameObject.FindGameObjectWithTag("SnapSource");

        universalPOIGenerator = GameObject.Find("UniversalPOIGenerator").GetComponent<UniversalPOIGenerator>();

        //
        SAPoseList = new List<Transform>();
        SAIndicatorList = new List<GameObject>();
        snapDatas = new List<SnapData>();

        AOIs = new List<GameObject>();
        AOIAnchors = new List<Transform>();

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

    public void Snap()
    {
        LogMe("Sanping");
        temperPOIList = new List<GameObject>();
        temperPOIList.AddRange(GameObject.FindGameObjectsWithTag("PoiPrefab"));

        foreach (GameObject p in temperPOIList)
        //foreach (GameObject p in universalPOIGenerator.POIs)
        {
            AOIs.Add(p);
        }

        if (AOIs == null)
        {
            LogMe("no poi");
            return;
        }


        GameObject nearestAOI = gameObject;
        Transform nearestAOIAnchor = transform;
        float minDist = float.MaxValue;
        //float minDist = 2.0f;

        mypose = this.transform;
        playerEyePose = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform;
        SnapSource.transform.position = this.transform.position; // change the snap source first 

        // visual: show my pose before align
        if (MyPoseIndicator_NN)
            MyPoseIndicator_NN.transform.position = this.transform.position;

        
        foreach (GameObject a in AOIs)
        {
            
            float dist = Vector3.Distance(SnapSource.transform.position, a.transform.position);
            
            LogMe("Current Dist = " + dist + " minDist = " + minDist);
            if (dist < minDist)
            {
                minDist = dist;
                nearestAOI = a;
            }
        }
        LogMe("Final minDist = " + minDist);
        LogMe("neartestSpatialAnchor position = " + nearestAOI.transform.position);

        AOIAnchors.Clear();
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor0"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor1"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor2"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor3"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor4"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor5"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor6"));
        AOIAnchors.Add(nearestAOI.transform.Find("AOIIllustrator/AnchorRoot/Anchor7"));

        foreach (Transform a in AOIAnchors)
        {
            float dist = Vector3.Distance(SnapSource.transform.position, a.transform.position);

            LogMe("Current Dist = " + dist + " minDist = " + minDist);
            if (dist < minDist)
            {
                minDist = dist;
                nearestAOIAnchor = a;
            }
        }

        //
        this.transform.position = nearestAOIAnchor.transform.position;
        this.transform.rotation = nearestAOIAnchor.transform.rotation;

        // for the Panels that are not snapping at the beginning, do not move scene accordingly 
        if (DoSnapWhenStarts)
            UpdateCalibrationAcrossScene(nearestAOIAnchor.transform.position, nearestAOIAnchor.transform.rotation);

        // visual 
        if (NearestSA_NN)
            NearestSA_NN.transform.position = nearestAOIAnchor.transform.position;
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

    public void endmunipi()
    {
        LogMe("end process");
    }
}

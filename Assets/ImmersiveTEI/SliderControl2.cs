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

namespace UnityVolumeRendering
{
    public class SliderControl2 : MonoBehaviour
    {
        public GameObject slider;
        public float SliderValue;
        
        public GameObject sphere;
        
        [SerializeField] public TextMeshPro Text;
        LineManager lineManager;
        ExampleVis exampleVis;
        GameObject Slot;
        GameObject Slot1;
        GameObject Slot2;
        VideoPlayerTest videoPlayerTest;
        Vector3 x;
        float y;
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

        DrawLine2 drawLine2;
        LineRenderer SliderTrack;

        void Start()
        {
            SliderValue = 0.5f;
            lineManager = GameObject.Find("Line").GetComponent<LineManager>();
            exampleVis = GameObject.FindGameObjectWithTag("ExampleVis")
            .GetComponent<ExampleVis>();
            Slot = GameObject.Find("ExampleVis/Slot");
            Slot1 = GameObject.Find("ExampleVis/Slot1");
            Slot2 = GameObject.Find("ExampleVis/Slot2");
            videoPlayerTest = GameObject.Find("ScreenPrefab").GetComponent<VideoPlayerTest>();
            oVRHand = GameObject.FindObjectOfType<OVRHand>();
            X = 0;
            var indexFingerTip = GameObject.Find("r_index_finger_tip_marker").transform.position;
            isTrue = oVRHand.Pinched;
            isLine = false;
            teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
            drawLine2 = GameObject.Find("Line").GetComponent<DrawLine2>();

            foreach (LineRenderer l in drawLine2.CustomizedSliderTrack)
            {
                if(sphere.transform.position == l.GetPosition(0))
                {
                    SliderTrack = l;
                    break;
                }
            }

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

        public void PointerAttachedtoTrack()
        {
            int NearestLinePoint = 0;
            float MinDist = float.MaxValue;
            for (int i = 0; i <= SliderTrack.positionCount; i++)
            {
                if(Vector3.Distance(SliderTrack.GetPosition(i), sphere.transform.position) < MinDist)
                {
                    MinDist = Vector3.Distance(SliderTrack.GetPosition(i), sphere.transform.position);
                    NearestLinePoint = i;
                    NearestLinePoint = NearestLinePoint;
                }
            }
            sphere.transform.position = SliderTrack.GetPosition(NearestLinePoint);
            SliderValue = (float)Math.Round((double)NearestLinePoint/SliderTrack.positionCount, 2);
            Text.text = SliderValue.ToString("f2");
        }

        public void TestSlider()
        {
            //Debug.Log(slider.transform.Find("slot").transform.position.ToString());
            foreach (LineRenderer l in lineManager.Lines)
                if (slider.transform.Find("Slot").transform.position == l.GetPosition(0))
                //if (Button.transform.Find("slot").transform.position == l.GetComponent<LineProperty>().Line.GetPosition(0))
                {
                    if (l.GetComponent<LineProperty>().Line.GetPosition(1) == Slot.transform.position)
                    {
                        exampleVis.SliderColorChange(SliderValue);
                    }
                    else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == Slot1.transform.position)
                    {
                        exampleVis.SliderDeform(SliderValue);
                    }
                    else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == Slot2.transform.position)
                    {
                        exampleVis.StopVis();
                    }
                    else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.Find("ScreenPrefab/slot1").transform.position)
                    {
                        if (sphere.transform.position != x)
                        {
                            videoPlayerTest.SliderControlVideo(SliderValue, 1f);
                        }
                        x = sphere.transform.position;
                        x = x;

                        /*if (videoPlayerTest._videoplayer.frame != y)
                        {
                            VideoControlSlider(1f);
                        }
                        y = videoPlayerTest._videoplayer.frame;
                        y = y;*/

                        //videoPlayerTest.SliderControlVideo(SliderValue, 1f);
                        //VideoControlSlider(1f);
                    }
                    else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.Find("ScreenPrefab/slot2").transform.position)
                    {
                        //videoPlayerTest.SliderControlVideo(SliderValue, 1f);
                        //VideoControlSlider(1f);
                    }
                }
        }

        public void VideoControlSlider(float SliderMaxValue)
        {
            SliderValue = (float)(videoPlayerTest._videoplayer.frame * SliderMaxValue / videoPlayerTest._videoplayer.frameCount);
            float Length = (this.transform.Find("SliderTrackSimple").transform.localScale.x) / 4;
            sphere.transform.localPosition = new Vector3(Length * SliderValue - Length / 2, sphere.transform.localPosition.y, sphere.transform.localPosition.z);
        }

       
        private void WhenSliderValueChange(float value)
        {
            videoPlayerTest.SliderControlVideo(SliderValue, 1f);

        }

        
    }
}

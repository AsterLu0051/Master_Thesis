using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UIElements;

namespace UnityVolumeRendering
{
    public class SliderControl : MonoBehaviour
    {
        public GameObject slider;
        public float SliderValue;
        /*{
            get { return SliderValue; }
            set
            {
                if (value != SliderValue)
                {
                    WhenSliderValueChange(SliderValue);
                }
                SliderValue = value;

            }
        }*/
        public GameObject sphere;
        public GameObject SliderTrack;
        [SerializeField] public TextMeshPro Text;
        LineManager lineManager;
        ExampleVis exampleVis;
        GameObject Slot;
        GameObject Slot1;
        GameObject Slot2;
        VideoPlayerTest videoPlayerTest;
        Vector3 x;
        float y;
        

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

            //videoPlayerTest._videoplayer.prepareCompleted += OnVideoReady;
            //videoPlayerTest._videoplayer.frameReady += OnFrameReady;

        }

        // Update is called once per frame
        void Update()
        {
            lineManager = GameObject.Find("Line").GetComponent<LineManager>();
            //videoPlayerTest = GameObject.Find("ScreenPrefab").GetComponent<VideoPlayerTest>();
            float Length = (this.transform.Find("SliderTrackSimple").transform.localScale.x) / 4;
            //float Moved = Vector3.Distance(sphere.transform.position, this.transform.position);

            if (sphere.transform.localPosition.x >= 0.5 * Length)
            {
                SliderValue = (float)1;
                sphere.transform.localPosition = new Vector3(sphere.transform.localPosition.x - 0.001f, sphere.transform.localPosition.y, sphere.transform.localPosition.z);
            }
            else if (sphere.transform.localPosition.x <= -0.5 * Length)
            {
                SliderValue = 0;
                sphere.transform.localPosition = new Vector3(sphere.transform.localPosition.x + 0.001f, sphere.transform.localPosition.y, sphere.transform.localPosition.z);
            }
            else
            {

                SliderValue = (sphere.transform.localPosition.x + Length / 2) / Length;
            }

            TestSlider();

            Text.text = SliderValue.ToString("f2");

            
        }

        void LogMe(string msg)
        {
            if (GameObject.FindGameObjectWithTag("LogInfo"))
                GameObject.FindGameObjectWithTag("LogInfo").GetComponent<DebugInfoManager>()
                    .AppendText(msg);
        }

        public void TestSlider()
        {
            videoPlayerTest = GameObject.FindGameObjectWithTag("ScreenPrefab").GetComponent<VideoPlayerTest>();
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
                    else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.FindGameObjectWithTag("ScreenPrefab").transform.Find("slot3").transform.position)
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
                    else if (l.GetComponent<LineProperty>().Line.GetPosition(1) == GameObject.Find("ScreenPrefab/slot1").transform.position)
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

        /*private void OnVideoReady(VideoPlayer source)
        {
            // 视频准备完成后可以开始播放  
            videoPlayerTest._videoplayer.Play();
        }

        private void OnFrameReady(VideoPlayer source, long frameIdx)
        {
            SliderValue = (float)(videoPlayerTest._videoplayer.frame * 1f / videoPlayerTest._videoplayer.frameCount);
            float Length = (this.transform.Find("SliderTrackSimple").transform.localScale.x) / 4;
            sphere.transform.localPosition = new Vector3(Length * SliderValue - Length / 2, sphere.transform.localPosition.y, sphere.transform.localPosition.z);
        }*/


        private void WhenSliderValueChange(float value)
        {
            videoPlayerTest.SliderControlVideo(SliderValue, 1f);

        }

        
    }
}


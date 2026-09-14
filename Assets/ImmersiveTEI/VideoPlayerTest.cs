using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using JfranMora.Inspector;

public class VideoPlayerTest : MonoBehaviour
{
    public VideoPlayer _videoplayer;
    public GameObject UIPanel;
    public float x = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     

    public void VideoPause()
    {
        if (_videoplayer.isPlaying == false) 
        {
            _videoplayer.Play();
        }
        else if (_videoplayer.isPlaying)
        {
            _videoplayer.Pause();
        }
        else if (_videoplayer.isPaused)
        {
            _videoplayer.Play();
        }
    }

    public void VideoForward()
    {
        _videoplayer.frame += 900;
    }

    public void VideoBackward()
    {
        _videoplayer.frame -= 900;
    }

    public void SliderControlVideo(float SliderValue, float SliderMaxValue)
    {
        _videoplayer.frame = (long)(SliderValue * _videoplayer.frameCount / SliderMaxValue);
    }

    

    public void ButtonMore()
    {
        UIPanel.SetActive(true);
    }
}

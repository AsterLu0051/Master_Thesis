using JfranMora.Inspector;
using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVolumeRendering;

public enum VisState{
    Idle,
    Blinking, 
    Rotating,
    Deforming
};



public class ExampleVis : MonoBehaviour
{
    public GameObject Original;
    private GameObject[] StartSlots;
    private GameObject[] EndSlots;

    GameObject Slot;
    GameObject Slot1;
    GameObject Slot2;
    GameObject Slider;
    GameObject ControlType;

    LineManager linemanager;
    SliderControl slidercontrol;
    float SliderValue;


    // Start is called before the first frame update
    void Start()
    {
        Original = this.gameObject;
        linemanager = GameObject.FindGameObjectWithTag("Line")
            .GetComponent<LineManager>();
        Slot = GameObject.Find("ExampleVis/Slot");
        Slot1 = GameObject.Find("ExampleVis/Slot1");
        Slot2 = GameObject.Find("ExampleVis/Slot2");
        StartSlots = GameObject.FindGameObjectsWithTag("StartSlot");
        EndSlots = GameObject.FindGameObjectsWithTag("EndSlot");
        slidercontrol = GameObject.FindGameObjectWithTag("SliderPrefab").GetComponent<SliderControl>();
        SliderValue = slidercontrol.SliderValue;
    }

    // Update is called once per frame
    void Update()
    {
        //ExecuteSlider();
    }
    
    bool isActive = true;
    float curScale = 1f;

    void Blink() {
        isActive = !isActive;
        this.transform.Find("Sphere").gameObject.SetActive(isActive);
    }

    public void SliderColorChange(float x)
    {
        this.transform.Find("Sphere").GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(x, 1, 1);
    }

    void Deform()
    {
        if(curScale<2f)
            curScale += 0.1f;
        else
            curScale = 1f;

        this.transform.Find("Sphere").transform.localScale = new Vector3(curScale, curScale, curScale);
    }

    public void SliderDeform(float x)
    {
        curScale = x * 2;
        this.transform.Find("Sphere").transform.localScale = new Vector3(curScale, curScale, curScale);
    }


    void UpdateVisState(VisState visState)
    {
        if(visState == VisState.Blinking)
        {
            InvokeRepeating("Blink", 0, 1.0f / 10);
            return;
        }

        if (visState == VisState.Deforming)
        {
            InvokeRepeating("Deform", 0, 1.0f / 10);
            return;
        }

        if(visState == VisState.Idle)
        {
            CancelInvoke();
        }
    }

    /*public void Execute()
    {

        foreach (LineRenderer l in linemanager.Lines)
        {
            ControlType = linemanager.BeginClickGameObejct.transform.parent.parent.gameObject;

            if (ControlType.name == "XRConfigurableBtn")
            {
                if (linemanager.EndClickGameObejct == Slot)
                {
                    DoBlink();
                }
                else if (linemanager.EndClickGameObejct == Slot1)
                {
                    DoDeform();
                }
                else if (linemanager.EndClickGameObejct == Slot2)
                {
                    StopVis();
                }
            }
            else if (ControlType.name == "SliderPrefab")
            {
                
                if (linemanager.EndClickGameObejct == Slot)
                {
                    DoBlink();
                }
                else if (linemanager.EndClickGameObejct == Slot1)
                {
                    SliderDeform(SliderValue);
                }
                else if (linemanager.EndClickGameObejct == Slot2)
                {
                    StopVis();
                }
            }
        }


    }

    void ExecuteSlider()
    {
        foreach (LineRenderer l in linemanager.Lines)
        {
            foreach (GameObject ss in StartSlots)
            {

            }
            ControlType = linemanager.BeginClickGameObejct.transform.parent.parent.gameObject;
            if (ControlType.name == "SliderPrefab")
            {

                if (linemanager.EndClickGameObejct == Slot)
                {
                    SliderColorChange(SliderValue);
                }
                else if (linemanager.EndClickGameObejct == Slot1)
                {
                    SliderDeform(SliderValue);
                }
                else if (linemanager.EndClickGameObejct == Slot2)
                {
                    StopVis();
                }
            }
        }
    }*/

    [Button]
    public void DoDeform()
    {
        UpdateVisState(VisState.Deforming);
    }

    [Button]
    public void DoBlink()
    {
        UpdateVisState(VisState.Blinking);
    }

    [Button]
    public void StopVis()
    {
        UpdateVisState(VisState.Idle);
    }


}

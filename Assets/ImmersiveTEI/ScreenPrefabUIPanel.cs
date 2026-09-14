using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using JfranMora.Inspector;
public class ScreenPrefabUIPanel : MonoBehaviour

{
    [SerializeField] GameObject Content;
    List<Tuple<string, Action<GameObject>>> featureList;
    public GameObject UIPanel;
    public GameObject ScreenVideoPlayer;
    public GameObject ScreenImagePlayer;
    public GameObject plane;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public bool ScreenPrefabCondition;


    // Start is called before the first frame update
    void Start()
    {
        ScreenPrefabCondition = true;
        ScreenVideoPlayer.SetActive(false);
        initUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initUI()
    {
        featureList = new List<Tuple<string, Action<GameObject>>>();
        featureList.Add(new Tuple<string, Action<GameObject>>("As Image Player", ActivateSlides));
        featureList.Add(new Tuple<string, Action<GameObject>>("As Video Player", ActivateVideo));

        foreach (var t in featureList)
        {
            GameObject ui = Instantiate((GameObject)Resources.Load("DebugUITextedBtnPrefeb", typeof(GameObject)), Content.transform);
            ui.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = t.Item1;
            ui.transform.Find("Btn").Find("ToggleState").gameObject.SetActive(false);
            ui.transform.Find("Btn").GetComponent<Button>().onClick.AddListener(delegate { t.Item2(ui); });
        }
    }

    [Button]
    void ActivateSlides(GameObject ui)
    {
        plane.SetActive(false);
        ScreenImagePlayer.SetActive(true);
        ScreenVideoPlayer.SetActive(false);
        UIPanel.SetActive(false);
        ScreenPrefabCondition = false;
        slot1.SetActive(true);
        slot2.SetActive(true);
        slot3.SetActive(false);
    }

    [Button]
    void ActivateVideo(GameObject ui)
    {
        plane.SetActive(false);
        ScreenImagePlayer.SetActive(false);
        ScreenVideoPlayer.SetActive(true);
        UIPanel.SetActive(false);
        ScreenPrefabCondition = true;
        slot1.SetActive(true);
        slot2.SetActive(true);
        slot3.SetActive(true);
    }
}

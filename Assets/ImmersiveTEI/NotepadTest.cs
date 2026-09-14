using JfranMora.Inspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class NotepadTest : MonoBehaviour
{
    TMP_InputField NodepadText;
    public GameObject NotepadPrefab;
    TEIControl teicontrol;
    OVRHand oVRHand;


    // Start is called before the first frame update
    void Start()
    {
        oVRHand = GameObject.FindObjectOfType<OVRHand>();
        NodepadText = GameObject.Find("NotepadPrefab/NNKeyboard/Canvas/InputField (TMP)").GetComponent<TMP_InputField>();
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        teicontrol = GameObject.Find("TEIControl").GetComponent<TEIControl>();
        var indexFingerTip = GameObject.Find("l_index_finger_tip_marker").transform.position;
        float dist = Vector3.Distance(indexFingerTip, NotepadPrefab.transform.position);
        if (oVRHand.Pinched == true && teicontrol.MovingNotepad == true && dist < 0.5f)
        {      
            NotepadPrefab.transform.position = indexFingerTip;
        }
    }


    public void DeleteNotepad()
    {
        DestroyImmediate(NotepadPrefab);
    }

    [Button]
    public void SaveNotepadText()
    {

        ixLogger.LogMe("Start writing");
        string RelativeFolderPath = "/Evaluations/";
        //File.Delete(Application.persistentDataPath + "/Evaluations/" + "Poiproperty.csv");

        string record = NodepadText.text;
        //ixLogger.LogMe(record);

        string content = "";
        string fpath = Application.persistentDataPath + RelativeFolderPath + "Note.csv";
        FileStream fs = new FileStream(fpath, FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        content += record;
        ixLogger.LogMe("Writing Note To File: " + record);
        sw.WriteLine(content);
        sw.Flush();
        sw.Close();
    }
}

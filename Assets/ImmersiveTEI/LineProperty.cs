using JfranMora.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineProperty : MonoBehaviour
{
    public GameObject BegainClickGameObejct;
    public GameObject EndClickGameObejct;
    public GameObject[] StartSlots;
    public GameObject[] EndSlots;

    GameObject LineNode;
    GameObject Slot;
    ExampleVis exampleVis;

    List<LineRenderer> Lines;
    public LineRenderer Line;

    [SerializeField]
    
    private Vector3 mousePos;
    private Vector3 startPos;   // Start position of line
    private Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

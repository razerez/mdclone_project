using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTable : MonoBehaviour
{
    public Color tableLineColor = Color.black;
    public float width;
    public float length;
    public float depth;
    public float x;
    public float y;
    public float z;
    public float rotation;
    string output;
    //Dictionary dataDict;
    GameObject tableLine;

    private void Start()
    {
        RunPython();
        SetVariables();
        CreateLine();
        rotation = 90;
        CreateLine();

    }


    private void RunPython()
    {
        string file = "data";
        output = ((TextAsset)Resources.Load(file)).text;
        Debug.Log(output);
    }


    public void SetVariables()
    {
        width = 0.01f;
        length = 70.0f;
        depth = 0.2f;
        x = 0;
        y = 0f;
        rotation = 0;
    }
    public void CreateLine()
    {
        tableLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tableLine.GetComponent<Collider>().enabled = !tableLine.GetComponent<Collider>().enabled;
        tableLine.transform.localScale = new Vector3(length, width, depth);
        tableLine.GetComponent<Renderer>().material.color = tableLineColor;
        tableLine.transform.Translate(x, y, z);
        tableLine.transform.rotation = Quaternion.Euler(0, this.rotation, 0);
    }
}
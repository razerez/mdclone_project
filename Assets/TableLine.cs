using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLine : MonoBehaviour {

    private Color tableLineColor = Color.black;
    public float width = 0.01f;
    private float length;
    public float depth = 0.2f;
    private float x;
    private float y;
    private float z;
    private float rotation = 0;
    GameObject tableLine;


    public TableLine(float x, float y, float z, float length)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.length = length;
    }


    public void SetX(float x)
    {
        this.x = x;
    }
    public void SetY(float y)
    {
        this.y = y;
    }
    public void SetZ(float z)
    {
        this.z = z;
    }
    public void SetLength(float length)
    {
        this.length = length;
    }

    public float GetX()
    {
        return this.x;
    }
    public float GetY()
    {
        return this.y;
    }
    public float GetZ()
    {
        return this.z;

    }
    public float GetLength()
    {
        return this.length;
    }
    public void Rotate()
    {
        this.rotation = 90;
    }
    public void UnRotate()
    {
        this.rotation = 0;
    }
    public void CreateLine()
    {
        tableLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tableLine.transform.localScale += new Vector3(this.length, width, depth);
        tableLine.GetComponent<Renderer>().material.color = tableLineColor;
        tableLine.transform.Translate(this.x, this.y, this.z);
        tableLine.transform.rotation = Quaternion.Euler(0, this.rotation,0);
    }
    
}

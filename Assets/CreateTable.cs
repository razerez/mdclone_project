using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class CreateTable : MonoBehaviour
{
    GameObject textCanvas;
    Text myText;
    int gj = -1;
    int pX;
    int pZ;
    public Color tableLineColor = Color.black;
    public float width;
    public float length;
    public float depth;
    public float x;
    public float y;
    public float z;
    public float rotation;
    public float height;
    public string output;
    public JToken o2;
    public float o;
    public float o3;
    public int xParameterCount;
    public int zParameterCount;
    public int colorParameterCount;
    public int dataCount;
    public List<object> data;
    public JArray JParameters;
    public JArray JData;
    public GameObject cube;
    public GameObject tableLine;
    public string firstKey = "";
    public string secondKey = "";
    public string thiredKey = "";
    public float minHeight;
    public float maxHeight;
    public float space;
    public int[,] cubesArray;
    public int[,] heightArray;
    float cubeHeight;
    float cubeDepth;
    float cubeWidth;

    private void Start()
    {
        RunPython();
        AnalyzeJson();
        SetVariables();
        ArrangeData();
        CreateTableLines();
        //tableLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
    //private void Update()
    //{

    //    tableLine.GetComponent<Collider>().enabled = !tableLine.GetComponent<Collider>().enabled;
    //    tableLine.transform.localScale = new Vector3(1, 1, o);
    //    tableLine.GetComponent<Renderer>().material.color = tableLineColor;
    //    tableLine.transform.position = new Vector3(0, o3, 0);
    //    tableLine.transform.rotation = Quaternion.Euler(90, 0, 0);
    //}
    private void CreateTableLines()
    {
        float x0 = length / 2;
        float z0 = 0;
        float x1 = 0;
        float z1 = length / 2;
        for (int i = 0; i < xParameterCount + 2 || i < zParameterCount + 2; i++)
        {

            if (xParameterCount > zParameterCount)
            {
                if (gj + 1 < zParameterCount)
                {
                    gj++;
                    PlaceCube(x1, z0);
                }
            }
            else
            {
                if (gj + 1 < xParameterCount)
                {
                    gj++;
                    PlaceCube(x1, z0);
                }
            }
            if (i < xParameterCount + 1)
            {
                rotation = 0;
                z = z0;
                x = x0;
                CreateLine();
            }
            if (i < zParameterCount + 1)
            {
                rotation = 90;
                z = z1;
                x = x1;
                CreateLine();
            }
            if (i < xParameterCount + 1)
            {
                
                z0 += length / (xParameterCount);
                //CreateTableData(mid, i, 'x');
            }
            if (i < zParameterCount + 1)
            {
                x1 += length / (zParameterCount);
                //CreateTableData(mid, i, 'x');
            }
        }
    }

    private void PlaceCube(float x0, float z0)
    {
        float x1 = x0 + (length / (zParameterCount)) / 2;
        float z1 = z0 + (length / (xParameterCount)) / 2;
        float cubeHeightP = minHeight;
        Color color = Color.red;
        bool textFlag = false;
        for (int i = gj; i < cubesArray.GetLength(0); i++)
        {
            color = Color.red;
            while(cubesArray[i,gj] > 0)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                cube.transform.localScale = new Vector3(cubeWidth, cubeHeight, cubeDepth);
                cube.GetComponent<Renderer>().material.color = color;
                cube.transform.Translate(x1, cubeHeightP, z1);
                cubesArray[i, gj]--;
                cubeHeightP += cubeHeight + (space / 2);
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                cube.transform.localScale = new Vector3(cubeWidth, 0.01f, cubeDepth);
                cube.GetComponent<Renderer>().material.color = Color.black;
                cube.transform.Translate(x1, cubeHeightP, z1);
                cubeHeightP += cubeHeight + (space / 2);
                textFlag = true;
            }
            if (textFlag)
            {
                CreateTableData(x1, cubeHeightP, z1, gj, i);
            }
            textFlag = false;
            cubeHeightP = minHeight;
            z1 += length / (xParameterCount);
        }
        x1 = x0 + (length / (zParameterCount)) / 2;
        z1 = z0 + (length / (xParameterCount)) / 2;
        for (int i = gj; i < cubesArray.GetLength(1); i++)
        {
            color = Color.red;
            while (cubesArray[gj, i] > 0)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                cube.transform.localScale = new Vector3(cubeWidth, cubeHeight, cubeDepth);
                cube.GetComponent<Renderer>().material.color = color;
                cube.transform.Translate(x1, cubeHeightP, z1);
                cubesArray[gj, i]--;
                cubeHeightP += cubeHeight + (space / 2);
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                cube.transform.localScale = new Vector3(cubeWidth, 0.01f, cubeDepth);
                cube.GetComponent<Renderer>().material.color = Color.black;
                cube.transform.Translate(x1, cubeHeightP, z1);
                cubeHeightP += cubeHeight + (space / 2);
                textFlag = true;
            }
            if (textFlag)
            {
                CreateTableData(x1, cubeHeightP, z1, i, gj);
            }
            textFlag = false;
            cubeHeightP = minHeight;
            x1 += length / (zParameterCount);
        }
    }
    private void CalcCubeDemetionsDimension()
    {
        cubeHeight = (height / (maxHeight + space));
        cubeDepth = (length / xParameterCount) - ((space *16) + width);
        cubeWidth = (length / zParameterCount) - ((space * 16) + width);
        space = cubeHeight / 5;
        minHeight += cubeHeight / 2;
    }
    private void ArrangeData()
    {
        int xPosition = -1;
        int zPosition = -1;
        int i = 0;
        int j = 0;
        foreach (JToken person in JData)
        {
            i = 0;
            j = 0;
            foreach (JToken xParameter in JParameters[0][firstKey])
            {
                if (person[firstKey].Equals(xParameter))
                {
                    xPosition = i;
                }
                i++;
            }
            foreach (JToken zParameter in JParameters[0][secondKey])
            {
                if (person[secondKey].Equals(zParameter))
                {
                    zPosition = j;
                }
                j++;
            }
            cubesArray[xPosition, zPosition]++;
            if (cubesArray[xPosition, zPosition] > maxHeight)
            {
                maxHeight = cubesArray[xPosition, zPosition];
            }
        }
        CalcCubeDemetionsDimension();
    }
    private void CreateTableData(float xt, float yt, float zt, int j, int i)
    {
        float size = 0.1f;
        GameObject textBox = new GameObject();
        textBox.AddComponent<TextMesh>();
        textBox.transform.localScale = new Vector3(size, size, size);
        textBox.transform.Translate(xt, yt, zt);
        textBox.GetComponent<TextMesh>().text = (string)JParameters[0][firstKey][i] + 'x' + (string)JParameters[0][secondKey][j];
        textBox.GetComponent<TextMesh>().fontSize = 200;
        textBox.GetComponent<TextMesh>().transform.localEulerAngles += new Vector3(0, 0, 90);
        textBox.GetComponent<TextMesh>().color = Color.yellow;
        //PutText(textCanvas, (string)JParameters[0][firstKey][i] + ' ' + (string)JParameters[0][firstKey][gj], size);
    }
    private void AnalyzeJson()
    {
        int i = 0;
        data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(output);
        JParameters = data[0] as JArray;
        JData = data[1] as JArray;
        foreach (JObject content in JParameters.Children<JObject>())
        {
            foreach (JProperty prop in content.Properties())
            {
                    if (i < 2)
                    {
                        firstKey = secondKey;
                        secondKey = prop.Name;
                        //thiredKey = prop.Name;
                    i++;
                    }
            }
        }
        foreach (JToken xParameter in JParameters[0][firstKey])
        {
            xParameterCount++;
        }
        foreach (JToken zParameter in JParameters[0][secondKey])
        {
            zParameterCount++;
        }
        foreach (JToken colorParameter in JParameters[0][secondKey])
        {
            colorParameterCount++;
        }
        foreach (JToken person in JData)
        {
            dataCount++;
        }
    }


    private void RunPython()
    {
        string file = "data";
        output = ((TextAsset)Resources.Load(file)).text;
        Debug.Log(output);
    }


    public void SetVariables()
    {
        pX = 0;
        pZ = 0;
        width = 0.01f;
        length = 70.0f;
        depth = 0.4f;
        x = 0;
        y = -0.15f;
        rotation = 0;
        height = 10f;
        minHeight = 0f;
        space = 0.1f;
        cubesArray = new int[xParameterCount,zParameterCount];
        heightArray = new int[xParameterCount, zParameterCount];
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
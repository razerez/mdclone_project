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
    public float minHeight;
    public float maxHeight;
    public float space;
    public int[,] cubesArray;
    public int[,] heightArray;
    float cubeHeight;
    float cubeDepth;
    float cubeWidth;
    public Dictionary<string, Color> colorDict = new Dictionary<string, Color>();
    public Dictionary<string, JToken> personDict;
    public List<string> keysList = new List<string>();

    private void Start()
    {
        RunPython();
        AnalyzeJson();
        SetVariables();
        CreateColorDict();
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
        int heightCounter = 1;
        for (int i = gj; i < cubesArray.GetLength(0); i++)
        {
            color = new Color();
            while(cubesArray[i,gj] > 0)
            {
                if (colorDict.ContainsKey("null"))
                {
                    color = Color.red;
                }
                else
                {
                    color = colorDict[(personDict[i.ToString() + " " + gj.ToString() + " " + heightCounter.ToString()][keysList[2]]).ToString()];
                }
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                cube.transform.localScale = new Vector3(cubeWidth, cubeHeight, cubeDepth);
                cube.GetComponent<Renderer>().material.color = color;
                cube.transform.Translate(x1, cubeHeightP, z1);
                cubesArray[i, gj]--;
                cubeHeightP += cubeHeight - space * 2;
                heightCounter++;
                if (maxHeight > 10)
                {
                    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                    cube.transform.localScale = new Vector3(cubeWidth, space - space / 10, cubeDepth);
                    cube.GetComponent<Renderer>().material.color = Color.black;
                    cube.transform.Translate(x1, cubeHeightP, z1);
                }
                cubeHeightP += (space) * 3;
                textFlag = true;
            }
            if (textFlag)
            {
                CreateTableData(x1, cubeHeightP, z1, gj, i);
            }
            textFlag = false;
            cubeHeightP = minHeight;
            heightCounter = 1;
            z1 += length / (xParameterCount);
        }
        x1 = x0 + (length / (zParameterCount)) / 2;
        z1 = z0 + (length / (xParameterCount)) / 2;
        for (int i = gj; i < cubesArray.GetLength(1); i++)
        {
            color = new Color();
            while (cubesArray[gj, i] > 0)
            {
                if (colorDict.ContainsKey("null"))
                {
                    color = Color.red;
                }
                else
                {
                    color = colorDict[(personDict[gj.ToString() + " " + i.ToString() + " " + heightCounter.ToString()][keysList[2]]).ToString()];
                }
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                cube.transform.localScale = new Vector3(cubeWidth, cubeHeight, cubeDepth);
                cube.GetComponent<Renderer>().material.color = color;
                cube.transform.Translate(x1, cubeHeightP, z1);
                cubesArray[gj, i]--;
                cubeHeightP += cubeHeight - space * 2;
                heightCounter++;
                if (maxHeight > 10)
                {
                    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.GetComponent<Collider>().enabled = !cube.GetComponent<Collider>().enabled;
                    cube.transform.localScale = new Vector3(cubeWidth, space - space/10, cubeDepth);
                    cube.GetComponent<Renderer>().material.color = Color.black;
                    cube.transform.Translate(x1, cubeHeightP, z1);
                }
                cubeHeightP += space * 3;
                textFlag = true;
            }
            if (textFlag)
            {
                CreateTableData(x1, cubeHeightP, z1, i, gj);
            }
            textFlag = false;
            cubeHeightP = minHeight;
            heightCounter = 1;
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
        int[] subArray;
        foreach (JToken person in JData)
        {
            i = 0;
            j = 0;
            foreach (JToken xParameter in JParameters[0][keysList[0]])
            {
                if (person[keysList[0]].Equals(xParameter))
                {
                    xPosition = i;
                }
                i++;
            }
            foreach (JToken zParameter in JParameters[0][keysList[1]])
            {
                if (person[keysList[1]].Equals(zParameter))
                {
                    zPosition = j;
                }
                j++;
            }
            cubesArray[xPosition, zPosition]++;
            subArray = new int[3] { xPosition, zPosition, cubesArray[xPosition, zPosition] };
            personDict.Add(subArray[0].ToString() + " " + subArray[1].ToString() + " " + subArray[2].ToString(), person);
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
        textBox.GetComponent<TextMesh>().text = (string)JParameters[0][keysList[0]][i] + 'x' + (string)JParameters[0][keysList[1]][j];
        textBox.GetComponent<TextMesh>().fontSize = 100;
        textBox.GetComponent<TextMesh>().transform.localEulerAngles += new Vector3(0, 0, 90);
        textBox.GetComponent<TextMesh>().color = Color.yellow;
    }
    private void CreateColorDict()
    {
        Color currColor = Color.red;
        Color newCol = new Color();
        long longColor = 0;
        string hex;
        long maxColor = 4294967295;
        for (int i = 0; i < colorParameterCount; i++)
        {
            if (colorParameterCount > 1)
            {
                hex = "#" + longColor.ToString("X8");
                if (ColorUtility.TryParseHtmlString(hex, out newCol))
                {
                    currColor = newCol;
                }
                longColor += maxColor / (colorParameterCount - 1);
            }
            else
            {
                currColor = Color.red;
            }
            colorDict.Add(JParameters[0][keysList[2]][i].ToString(), currColor);
        }
        if (colorParameterCount == 0)
        {
            colorDict.Add("null", Color.red);
        }
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
                keysList.Add(prop.Name);
                i++;
            }
        }
        foreach (JToken xParameter in JParameters[0][keysList[0]])
        {
            xParameterCount++;
        }
        foreach (JToken zParameter in JParameters[0][keysList[1]])
        {
            zParameterCount++;
        }
        foreach (JToken zParameter in JParameters[0][keysList[2]])
        {
            colorParameterCount++;
        }
        foreach (JToken person in JData)
        {
            dataCount++;
        }
        SortByKeyVal(keysList[2]);
    }

    private void SortByKeyVal(JToken inpKey)
    {
        JToken prvVal = "";
        JToken currVal;
        JToken prvPerson = "";
        JToken tempPerson;
        JToken person;
        int count = -1;
        while (count != 0)
        {
            if (count < 0)
            {
                count = 1;
            }
            else
            {
                count = 0;
            }
            for (int i = 0; i < dataCount; i++)
            {

                person = JData[i];
                currVal = person[inpKey.ToString()];
                if (i != 0)
                {
                    if (string.Compare(currVal.ToString(), prvVal.ToString()) > 0)
                    {
                        tempPerson = person;
                        JData[i] = prvPerson;
                        JData[i - 1] = tempPerson;
                        count++;
                    }
                }
                prvPerson = person;
                prvVal = currVal;
            }
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
        personDict = new Dictionary<string, JToken>();
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
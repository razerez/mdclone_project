using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList : MonoBehaviour {

    public List<object> mainList = new List<object>();
    private List<string> parameterList = new List<string>();
    private Dictionary<string, string> dataDict = new Dictionary<string, string>();


    public DataList(List<string> parameterList, Dictionary<string, string> dataDict)
    {
        this.parameterList = parameterList;
        this.dataDict = dataDict;

    }
    public void SetParameterList(List<string> parameterList)
    {
        this.parameterList = parameterList;
    }
    public void SetDataDict(Dictionary<string, string> dataDict)
    {
        this.dataDict = dataDict;
    }
    public List<string> GetParameterList()
    {
        return this.parameterList;
    }
    public Dictionary<string, string> GetDataDict()
    {
        return this.dataDict;
    }
    public void CreateMainList()
    {
        mainList.Add(parameterList);
        mainList.Add(dataDict);
    }
    public void DecodeMainList()
    {
        //this.parameterList = mainList[0];
    }
}

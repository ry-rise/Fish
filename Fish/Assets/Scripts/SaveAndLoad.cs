using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class SaveAndLoad : MonoBehaviour
{
    public void SaveData(Data data, string path)
    {
        Data saveData = data;
        string jsonData = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(path, jsonData);
    }
    public Data LoadData(string path)
    {
        var jsonData = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<Data>(jsonData);
    }
}

[Serializable]
public class Data
{
    [SerializeField]
    public List<string> Type = new List<string>();
    public List<int> Amount = new List<int>();
    public List<float> Time = new List<float>();
    public List<string> Name = new List<string>();
}
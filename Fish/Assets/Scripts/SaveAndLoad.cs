using UnityEngine;

public class SaveAndLoad
{
    public static void SaveData<T>(T data, string path)
    {
        var jsonData = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(path, jsonData);
    }

    public static T LoadData<T>(string path)
    {
        var jsonData = System.IO.File.ReadAllText(path);
        T data = JsonUtility.FromJson<T>(jsonData);
        return data;
    }
}
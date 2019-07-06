using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseFish : ScriptableObject
{
    //魚の名前
    [SerializeField]
    private string fishName = "A";
    public string FishName { get { return fishName; } }
    //魚のレベル
    [SerializeField]
    private int minlevel = 1;
    public int MinLevel { get { return minlevel; } }
    [SerializeField]
    private int maxlevel = 1;
    public int MaxLevel { get { return maxlevel; } }
    //速度
    [SerializeField]
    private float speed = 1;
    public float Speed { get { return speed; } }
    //ツール化
    [MenuItem("Tool/Create Fish")]
    static void CreateAsset()
    {
        var fishData = CreateInstance<BaseFish>();

        AssetDatabase.CreateAsset(fishData, "Assets/Editor/Fish.asset");
        AssetDatabase.Refresh();
    }
}

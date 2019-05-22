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
    private int level = 1;
    public int Level { get { return level; } }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseFish : ScriptableObject
{
    //魚の名前
    [SerializeField]
    private string fishName;
    public string FishName { get { return fishName; } }
    //魚のレベル
    [SerializeField]
    private int level;
    public int Level { get { return level; } }
    //速度
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }
    //タイプ
    [SerializeField]
    private int type;
    public int Type { get { return type; } }
    //ツール化
    [MenuItem("Tool/Create Fish")]
    static void CreateAsset()
    {
        var fishData = CreateInstance<BaseFish>();

        AssetDatabase.CreateAsset(fishData, "Assets/Editor/Fish.asset");
        AssetDatabase.Refresh();
    }
}

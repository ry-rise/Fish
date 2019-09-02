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
    private int minLevel = 1;
    public int MinLevel { get { return minLevel; } }
    [SerializeField]
    private int maxLevel = 1;
    public int MaxLevel { get { return maxLevel; } }
    //速度
    [SerializeField]
    private float speed = 1;
    public float Speed { get { return speed; } }

#if UNITY_EDITOR
    //ツール化
    [MenuItem("Tool/Create Fish")]
    static void CreateAsset()
    {
        var fishData = CreateInstance<BaseFish>();

        AssetDatabase.CreateAsset(fishData, "Assets/Editor/Fish.asset");
        AssetDatabase.Refresh();
    }
#endif
}

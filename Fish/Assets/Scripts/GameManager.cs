using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private float time;
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        timeText.text = $"Time:{Mathf.Floor(time)}";
    }
}

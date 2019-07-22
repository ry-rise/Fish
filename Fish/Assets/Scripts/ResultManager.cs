using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ResultManager : MonoBehaviour
{

    [SerializeField]
    private Button result;
    
    GameManager  time;

    public void Go()
    {
        SceneManager.LoadScene("Title");
        time = GetComponent < GameManager>();
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

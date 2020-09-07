﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInTheGame : MonoBehaviour
{
    public GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void ExitInMenu()
    {
        SceneManager.LoadScene(0);
    }
}

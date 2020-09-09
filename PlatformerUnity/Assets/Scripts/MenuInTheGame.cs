using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInTheGame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settings;
    bool settingsIsActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (settingsIsActive && Input.GetKey(KeyCode.Escape)){
            settings.SetActive(false);
            menuPanel.SetActive(true);
            settingsIsActive = false;
        }
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

    public void Settings() {
        settingsIsActive = true;
        settings.SetActive(true);
        menuPanel.SetActive(false);
    }    
}

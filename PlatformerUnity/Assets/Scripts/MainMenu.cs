using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   

    public GameObject settings;
    bool settingsIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (settingsIsActive && Input.GetKey(KeyCode.Escape)){
            settings.SetActive(false);
            settingsIsActive = false;
        }
    }
    public void Play()
    {   
        settingsIsActive = false;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Settings() {
        settingsIsActive = true;
        settings.SetActive(true);
    }  
}

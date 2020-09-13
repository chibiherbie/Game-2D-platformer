using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiseGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)){
        SceneManager.LoadScene(1);
        }
    }

    public void SinglePlay()
    {
        SceneManager.LoadScene(4);
    }

    public void MultiPlayer()
    {
        SceneManager.LoadScene(3);
    }
}

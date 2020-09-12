using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class choiseGame : MonoBehaviour
{
    private void Update() {
        if (Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene(1);
        }
    }
    public void SinglePlay(){
        SceneManager.LoadScene(5);
    }

    public void MultiPlayer(){
        SceneManager.LoadScene(3);
    }
}

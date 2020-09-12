using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class levelChoise : MonoBehaviour
{   
    int count = 0;
    public Button lv1;
    public Button lv2;
    public Button lv3;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = lv1.GetComponent<RectTransform>().position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }
    public void play()
    {
        SceneManager.LoadScene(2);
    }
    public void left()
    {
        if (count == 1) {
            lv1.GetComponent<RectTransform>().position = startPosition;
            lv2.GetComponent<RectTransform>().position = new Vector3(-3210.1f, 0, 0);
            count -= 1;
        }

    }

     public void right()
    {   
        
        
        if (count == 0) {
            lv1.GetComponent<RectTransform>().position = new Vector3(-3200, 0, 0);
            lv2.GetComponent<RectTransform>().position = startPosition;
            count += 1;
        }
    }


}

using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class Music : MonoBehaviour
{   

    public Slider slider;
    public Text valueCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        valueCount.text = slider.value.ToString();
        AudioListener.volume = slider.value;
    }
}

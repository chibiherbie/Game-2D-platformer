using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTextBonus : MonoBehaviour
{   

    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
        time -= 1 * Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
    }
}

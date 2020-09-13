using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{    
    public GameObject player;
    float camY;
    float camZ;
    float screenScript_L;
    float screenScript_R;
    // Start is called before the first frame update
    void Start()
    {   
        // 
        camY = gameObject.transform.position.y;
        camZ = gameObject.transform.position.z;

        // левая граница крана тригера для камеры
        screenScript_L = Screen.width / 2 - Screen.width / 2 / 100 * 60;
        // правая граница крана тригера для камеры
        screenScript_R = Screen.width / 2 + Screen.width / 2 / 100 * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // левая сторона экрана
        if (player.transform.position.x - gameObject.transform.position.x <= -6f) {
            gameObject.transform.position = new Vector3(player.transform.position.x + 6f, camY, camZ);
        }

        // правая сторона экрана
        if (player.transform.position.x - gameObject.transform.position.x >= 6f) {
            gameObject.transform.position = new Vector3(player.transform.position.x - 6f, camY, camZ);
        }
    }
}

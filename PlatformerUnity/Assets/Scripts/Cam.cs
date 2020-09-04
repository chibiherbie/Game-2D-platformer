﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{    
    public GameObject player;
    float camY;
    float camZ;
    // Start is called before the first frame update
    void Start()
    {
        camY = gameObject.transform.position.y;
        camZ = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {   
        if (player.transform.position.x - gameObject.transform.position.x <= -7f) {
            gameObject.transform.position = new Vector3(player.transform.position.x + 7f, camY, camZ);
        }

        if (player.transform.position.x - gameObject.transform.position.x >= 7f) {
            gameObject.transform.position = new Vector3(player.transform.position.x - 7f, camY, camZ);
        }
    }
}

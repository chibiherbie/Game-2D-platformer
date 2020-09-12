using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{   

    GameObject player;
    bool timer = true;
    public float XP;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (XP >= 100){
            if (Input.GetKey(KeyCode.Q)){
                player.GetComponent<PlayerControl>().health = player.GetComponent<PlayerControl>().maxHealth;
                XP = 0;
            }
        }
        
    }
}

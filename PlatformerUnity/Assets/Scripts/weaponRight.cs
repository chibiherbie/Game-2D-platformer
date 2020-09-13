using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponRight : MonoBehaviour
{   
    Transform pointForWeapon;
    public bool isRightW = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == pointForWeapon)
            gameObject.transform.position = pointForWeapon.transform.position;
            
        if (transform.parent.GetComponent<PlayerControl>().weaponNow == gameObject){
            if (FindObjectOfType<PlayerControl>().isRight) isRightW = true;
            else isRightW = false;
        }
    }
}

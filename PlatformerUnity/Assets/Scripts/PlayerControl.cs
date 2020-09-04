using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;

    public Rigidbody rb;
    public int speed;

    public int JumpSpeed;
    public bool Jumping;

    Vector3 destinationPoint;
float smoothing;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }   

    // Update is called once per frame
    void FixedUpdate()
    {
       transform.position = Vector3.Lerp (transform.position, destinationPoint, smoothing * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Jumping == false)
            {
                Jumping = true;
                rb.AddForce(player.transform.up * JumpSpeed, ForceMode.Impulse);
            }
        }

    }
    void OnCollisionEnter(Collision collis)
    {
        if (collis.gameObject == platform)
        {
            Jumping = false;
        }
    }
}
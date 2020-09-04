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
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }   

    // Update is called once per frame
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        var direction = new Vector3(h, v);  
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        
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
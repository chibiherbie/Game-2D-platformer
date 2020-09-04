using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;

    public Rigidbody rb;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position - transform.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position - transform.right * speed * Time.deltaTime);
        }
    }
}
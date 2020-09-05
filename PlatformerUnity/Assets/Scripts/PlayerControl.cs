using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    public Rigidbody rb;

    public int speed;
    public int JumpSpeed;
    public bool Jumping;
    public float health;
    public float damage;
    public int currentWeapon;
    public List<float> weaponList;
    public Slider hpbar;
    //public float[] weaponList = new float[10];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        health = 100f;
        damage = 5f;
    }   

    // Update is called once per frame
    void FixedUpdate()
    {   
        // передвижение персонажа
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        var v = new Vector3(x, 0.0f, z);
        transform.position += v * speed * Time.deltaTime;   
        
        // прыжок персонажа
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Jumping == false)
            {
                Jumping = true;
                rb.AddForce(player.transform.up * JumpSpeed, ForceMode.Impulse);
            }
        }

        //отображение жизней персонажа
        hpbar.value = health;
    }

    void OnCollisionEnter(Collision collis)
    {
        if (collis.gameObject == platform)
        {
            Jumping = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        // атака врага
        if (other.tag == "Enemies") {
            if (Input.GetKeyDown(KeyCode.F)) {
                other.GetComponent<AI>().health -= damage;
            }
        }

        // подбор оружия
        if (other.tag == "weapon") {
            if (Input.GetKeyDown(KeyCode.E)) {
                currentWeapon = int.Parse(other.name);
                for (int i=0; i < 10;i++)
                {
                    if (i == currentWeapon) {
                        damage = weaponList[i];
                        break;
                    }
                }
            }
        }
    }
}
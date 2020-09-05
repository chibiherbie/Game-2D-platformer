using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    public Rigidbody rb;
    public Transform pointForWeapon;
    public int speed;
    public float health;
    public float damage;
    public int currentWeapon;
    public List<float> weaponList;
    public Slider hpbar;
    bool is_ground = false; // на земле ли игрок
    public float force = 5;
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

        //отображение жизней персонажа
        hpbar.value = health;
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
            // отоброжение характеристик
            

            if (Input.GetKeyDown(KeyCode.E)) {
                
                // смена оружия
                if (currentWeapon != 0) {
                    //lastname.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                }

                currentWeapon = int.Parse(other.name);
                for (int i=0; i < 10; i++)
                {
                    if (i == currentWeapon) {
                        other.transform.parent = pointForWeapon;
                        other.transform.position = pointForWeapon.position;
                        damage = weaponList[i];
                        break;
                    }
                }
            }
        }

        if (other.tag == "ground"){  //если в тригере что то есть и у обьекта тег "ground"
            is_ground = true; //то включаем переменную "на земле"
            }   
    }
    
     void OnTriggerExit(Collider col){              //если из триггера что то вышло и у обьекта тег "ground"
        if (col.tag == "ground") {
            is_ground = false;
        }     //то выключаем переменную "на земле"
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) & is_ground) {        //если нажата кнопка "пробел" и игрок на земле
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);   //то придаем ему силу вверх импульсным пинком
        }
    }
}
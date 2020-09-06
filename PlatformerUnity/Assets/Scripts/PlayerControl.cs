using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    GameObject weaponNow;
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
    bool checkW;
    float pickDelay = 0;
    public int countCoin = 0;
    public Text coinValue;
    public List<int> countJar;
    public Image currentJar;
    public Sprite hpJar;
    public Sprite speedJar;
    public Sprite attackJar;
    public Text currentJarsValue;
    float usingJarDelay=0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        health = 100f;
        damage = 5f;
        currentJar.sprite = hpJar;
        currentJarsValue.text = countJar[0].ToString();
        checkW = false;
    }   

    // Update is called once per frame
    void FixedUpdate()
    {   
        coinValue.text = countCoin.ToString();

        // передвижение персонажа
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        var v = new Vector3(x, 0.0f, z);
        transform.position += v * speed * Time.deltaTime;   

        //отображение жизней персонажа
        hpbar.value = health;

        // задержкжа для подбора оружия и атаки
        if (checkW && pickDelay > 0)
        {
            pickDelay -= 1 * Time.deltaTime;
        }

        else if(checkW && pickDelay <= 0)
        {
            checkW = false;
        }
        if(usingJarDelay > 0)
        {
            usingJarDelay -= 1 * Time.deltaTime;
        }
        if(currentJar.sprite == hpJar)
        {
            currentJarsValue.text = countJar[0].ToString();
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[0] >= 1)
            {
                usingJarDelay = 1;
                health += 25;
                countJar[0]--;
            }
        }
        if(currentJar.sprite == attackJar)
        {
            currentJarsValue.text = countJar[1].ToString();
            currentJarsValue.text = countJar[0].ToString();
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[1] >= 1)
            {
                usingJarDelay = 1;
                damage += 10;
                countJar[1]--;
            }
        }
        if(currentJar.sprite == speedJar)
        {
            currentJarsValue.text = countJar[2].ToString();
            currentJarsValue.text = countJar[0].ToString();
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[2] >= 1)
            {
                usingJarDelay = 1;
                speed += 10;
                countJar[2]--;
            }
        }
        if(Input.GetKey(KeyCode.Alpha1))
        {
            currentJar.sprite = hpJar;
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            currentJar.sprite = speedJar;
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            currentJar.sprite = attackJar;
        }
    }


    private void OnTriggerStay(Collider other) {
        // атака врага
        if (other.tag == "Enemies") {
            if (Input.GetKey(KeyCode.F)) {
                if (!checkW ) {

                    checkW = true;
                    // задержка атаки
                    pickDelay = 0.5f;

                    other.GetComponent<AI>().health -= damage;
                }
                
            }
        }

        // подбор оружия
        if (other.CompareTag("weapon") && Input.GetKey(KeyCode.E)){
            // отоброжение характеристик


                
            if (!checkW) {
            // смена оружия
                if (currentWeapon != 0) {
                    weaponNow.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                    weaponNow.transform.parent = null;
                }

                checkW = true;
                pickDelay = 0.5f;

                currentWeapon = int.Parse(other.name);
                for (int i=0; i < 10; i++)
                {
                    if (i == currentWeapon) {
                        other.transform.parent = pointForWeapon;
                        other.transform.position = pointForWeapon.position;
                        damage = weaponList[i];
                        weaponNow = other.gameObject; 
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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{   
    public GameObject wall;
    public GameObject platform;
    public GameObject player;
    GameObject weaponNow;
    public Rigidbody rb;
    public Transform pointForWeapon;
    public int speed;
    public float health;
    public float damage;
    int damageJar = 0;
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
    bool timeJarSpeed = false;
    bool timeJarAttack = false;
    float timeJarSecS = 15;
    float timeJarSecA = 15;
    float usingJarDelay=0;
    string nameJar;
    bool isRight = true;
    public float distantionThrow = 15f;
    public TextMesh textSpeed;
    public TextMesh textAttack;
    public TextMesh textHealth;
    public Slider speedbar;
    public Slider attackbar;
    public GameObject menuPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        health = 100f;
        damage = 2f;
        currentJar.sprite = hpJar;
        currentJarsValue.text = countJar[0].ToString();
        checkW = false;

       
    }   

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKey(KeyCode.Escape)){
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }


        coinValue.text = countCoin.ToString();

        // передвижение персонажа
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        var v = new Vector3(x, 0.0f, z);
        transform.position += v * speed * Time.deltaTime;

        // сторона персонажа
        if (Input.GetKey(KeyCode.D) && !isRight && !Input.GetKey(KeyCode.A)) {
            isRight = true;
            Vector3 theScale = gameObject.transform.localScale;
            theScale.x *= -1;
            gameObject.transform.localScale = theScale;
        }
        
        else if (Input.GetKey(KeyCode.A) && isRight && !Input.GetKey(KeyCode.D)) {
            isRight = false;
            Vector3 theScale = gameObject.transform.localScale;
            theScale.x *= -1;
            gameObject.transform.localScale = theScale;
        }   

        //отображение жизней персонажа
        hpbar.value = health;

        // таймер для скорости
        if (timeJarSpeed && timeJarSecS > 0)
        {
            timeJarSecS -= 1 * Time.deltaTime;

            speedbar.value = timeJarSecS;
        }

        else if(timeJarSpeed && timeJarSecS <= 0)
        {
            timeJarSpeed = false;
            timeJarSecS = 15;
            speed -= 2;
        }

        // таймер для атаки
        if (timeJarAttack && timeJarSecA > 0)
        {
            timeJarSecA -= 1 * Time.deltaTime;

            attackbar.value = timeJarSecA;
        }

        else if(timeJarAttack && timeJarSecA <= 0)
        {
            timeJarAttack = false;
            timeJarSecA = 15;
            damageJar = 0;
        }

        // задержкжа для подбора оружия и атаки
        if (checkW && pickDelay > 0)
        {
            pickDelay -= 1 * Time.deltaTime;
        }

        else if(checkW && pickDelay <= 0)
        {
            checkW = false;
        }

        // задержка для использовния банок
        if(usingJarDelay > 0)
        {
            usingJarDelay -= 1 * Time.deltaTime;
        }
     
        // банка хп
        if(currentJar.sprite == hpJar)
        {
            currentJarsValue.text = countJar[0].ToString();
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[0] >= 1)
            {
                usingJarDelay = 1;
                health += 35;
                countJar[0]--;

                textHealth.text = "+35 хп";
                // вывод, сколько прибавилось 
                Instantiate(textHealth, new Vector3(gameObject.transform.position.x - 1 , gameObject.transform.position.y + 5, gameObject.transform.position.z + 2), textHealth.transform.rotation);
            }
        }

        // банка скорости
        if(currentJar.sprite == speedJar)
        {
            currentJarsValue.text = countJar[1].ToString();
            

            
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[1] >= 1)
            {   
                if (!timeJarSpeed) { // проверка, не используем ли мы в даный момент другую банку
                    usingJarDelay = 1;
                    speed += 2;
                    countJar[1]--;

                    textSpeed.text = "+2 к скорости";
                    // вывод, сколько прибавилось
                    Instantiate(textSpeed, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y + 5, gameObject.transform.position.z + 2), textSpeed.transform.rotation);

                    timeJarSpeed = true;
                } 

                else { // вывод, что вторую использовать сразу нельзя
                    usingJarDelay = 1;

                    textSpeed.text = "Рано";
                    Instantiate(textSpeed, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y + 5, gameObject.transform.position.z + 2), textSpeed.transform.rotation);
                }
            }
        }
        
        // банка атаки
        if(currentJar.sprite == attackJar)
        {
            currentJarsValue.text = countJar[2].ToString();
            
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[2] >= 1)
            {    
                if (!timeJarAttack) {  // проверка, не используем ли мы в даный момент другую банку
                    usingJarDelay = 1;
                    damageJar += 10;
                    countJar[2]--;

                    textAttack.text = "+10 к атаке";
                    // вывод, сколько прибавилось
                    Instantiate(textAttack, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y + 5, gameObject.transform.position.z + 2), textAttack.transform.rotation);

                    timeJarAttack = true;
                }

                else { // вывод, что вторую использовать сразу нельзя
                    usingJarDelay = 1;
                     
                    textAttack.text = "Рано";
                    Instantiate(textAttack, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y + 5, gameObject.transform.position.z + 2), textAttack.transform.rotation);

                }
            }   
        }

        // использовния банок
        if(Input.GetKey(KeyCode.Alpha1)) // ХП
        {
            currentJar.sprite = hpJar;
        }

        if(Input.GetKey(KeyCode.Alpha2)) // скорость
        {
            currentJar.sprite = speedJar;
        }

        if(Input.GetKey(KeyCode.Alpha3)) // атака
        {
            currentJar.sprite = attackJar;
        }

        // линия, куда кидается оружие (проверка)
        Debug.DrawRay(pointForWeapon.transform.position, pointForWeapon.transform.right * distantionThrow, Color.red);
        Debug.DrawRay(pointForWeapon.transform.position, -pointForWeapon.transform.right * distantionThrow, Color.yellow);
        
        // бросок оружия
        if (Input.GetKey(KeyCode.G) && is_ground){
            if (currentWeapon != 0) {
                
                // для правой стороны игрока
                if (isRight) {  
                    Ray ray = new Ray(pointForWeapon.transform.position, pointForWeapon.transform.right * distantionThrow);
                    RaycastHit hit;

                    weaponNow.transform.Translate(ray.direction * distantionThrow, Space.World);
                    weaponNow.transform.parent = null;

                    if (Physics.Raycast(ray, out hit)){
                        if (hit.transform.CompareTag("Enemies")){
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.7f, hit.transform.position.z);
                            hit.transform.gameObject.GetComponent<AI>().health -= (damage + damageJar) * 1.5f;
                        }
                        else {
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 0.5f, hit.transform.position.z);
                        }
                    }
                }

                // для левой стороны игрока
                else {
                    Ray ray = new Ray(pointForWeapon.transform.position, -pointForWeapon.transform.right * distantionThrow);
                    RaycastHit hit;
                    
                    weaponNow.transform.Translate(ray.direction * distantionThrow, Space.World);
                    weaponNow.transform.parent = null;

                    if (Physics.Raycast(ray, out hit)){
                        if (hit.transform.CompareTag("Enemies")){
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.7f, hit.transform.position.z);
                            hit.transform.gameObject.GetComponent<AI>().health -= (damage + damageJar) * 1.5f;
                        }
                        else {
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 0.5f, hit.transform.position.z);
                        }
                    }
                }
                
            damage = 5;
            currentWeapon = 0;
            } 
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

                    other.GetComponent<AI>().health -= damage + damageJar;
                }
            }
        }

        // подбор оружия
        if (other.CompareTag("weapon")){
            // отоброжение характеристик
            


            if (Input.GetKey(KeyCode.E)) {
                if (!checkW) {
                // смена оружия
                    if (currentWeapon != 0) {
                        weaponNow.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);

                        // кручение оружия
                        //weaponNow.transform.localScale = other.transform.localScale;
                        weaponNow.transform.parent = null;
                        
                        // изменения слоя
                        weaponNow.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    }

                    checkW = true;
                    pickDelay = 0.5f;

                    currentWeapon = int.Parse(other.name);
                    for (int i=0; i < 10; i++)
                    {
                        if (i == currentWeapon) {
                            if (isRight) {

                                Vector3 theScale = other.transform.localScale;
                                theScale.x *= -1;
                                other.transform.localScale = theScale;

                                other.transform.parent = pointForWeapon;
                                other.transform.position = pointForWeapon.position;
                                damage = weaponList[i];
                                weaponNow = other.gameObject; 
                                weaponNow.GetComponent<SpriteRenderer>().sortingOrder = 2;
                                break;
                            }
                            else {
                                
                                Vector3 theScale = other.transform.localScale;
                                theScale.x *= -1;
                                other.transform.localScale = theScale;

                                other.transform.parent = pointForWeapon;
                                other.transform.position = pointForWeapon.position;
                                damage = weaponList[i];
                                weaponNow = other.gameObject; 
                                weaponNow.GetComponent<SpriteRenderer>().sortingOrder = 2;
                                break;
                            }
                        }
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
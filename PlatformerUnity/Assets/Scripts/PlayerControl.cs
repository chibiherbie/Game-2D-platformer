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
    float timeJarSec = 15;
    float usingJarDelay=0;
    string nameJar;
    bool isRight = true;
    public float distantionThrow = 15f;
    Transform _transform;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        _transform = GetComponent<Transform>();

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
        if (timeJarSpeed && timeJarSec > 0)
        {
            timeJarSec -= 1 * Time.deltaTime;
        }

        else if(timeJarSpeed && timeJarSec <= 0)
        {
            timeJarSpeed = false;
            timeJarSec = 15;
            speed -= 2;
        }

        // таймер для атаки
        if (timeJarAttack && timeJarSec > 0)
        {
            timeJarSec -= 1 * Time.deltaTime;
        }

        else if(timeJarAttack && timeJarSec <= 0)
        {
            timeJarAttack = false;
            timeJarSec = 15;
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

        // pзадержка для использовния банок
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

                // вывод, сколько прибавилось 
                Debug.Log("35+");
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

                    timeJarSpeed = true;
                } 

                else { // вывод, что вторую использовать сразу нельзя
                    usingJarDelay = 1;

                    // вывод, сколько прибавилось 
                    Debug.Log("исп");
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

                    timeJarAttack = true;
                }

                else { // вывод, что вторую использовать сразу нельзя
                    usingJarDelay = 1;
                    
                    // вывод, сколько прибавилось 
                    Debug.Log("исп");
                }
            }   
        }

        // использовния банок
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

        // линия, кудакидается оружие
        Debug.DrawRay(pointForWeapon.transform.position, pointForWeapon.transform.right * distantionThrow, Color.red);

        // бросок оружия
        if (Input.GetKey(KeyCode.G) && is_ground){
            if (currentWeapon != 0) {
                
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
        if (other.CompareTag("weapon") && Input.GetKey(KeyCode.E)){
            // отоброжение характеристик

                
            if (!checkW) {
            // смена оружия
                if (currentWeapon != 0) {
                    weaponNow.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                    // кручение оружия
                    //weaponNow.transform.localScale = other.transform.localScale;
                    weaponNow.transform.parent = null;
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
                            break;
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
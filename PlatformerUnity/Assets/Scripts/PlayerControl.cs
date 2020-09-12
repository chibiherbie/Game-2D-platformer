using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{   
    public GameObject wall;
    public GameObject platform;
    public GameObject player;
    GameObject weaponNow;
    GameObject headNow;
    GameObject handNow;
    public Rigidbody rb;
    public Transform pointForWeapon;
    public Transform pountForHead;
    public Transform pountForHand;
    public int speed;
    public float health;
    float startHealth;
    public float maxHealth;
    public float damage;
    int damageJar = 0;
    int damageHand = 0;
    public int currentWeapon;
    public int currentHead;
    public int currentHand;
    public List<float> weaponList;
    public List<float> headList;
    public List<int> handList;
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
    public bool isRight;
    public float distantionThrow = 15f;
    public TextMesh textSpeed;
    public TextMesh textAttack;
    public TextMesh textHealth;
    public Slider speedbar;
    public Slider attackbar;
    public GameObject menuPanel;
    public GameObject dead;
    public GameObject finish;
    PhotonView photonView;
    Animator animator;
    SpriteRenderer sprite;
    Transform Direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 100f;
        startHealth = 100f;
        maxHealth = 100f;
        damage = 2f;
        currentJar.sprite = hpJar;
        currentJarsValue.text = countJar[0].ToString();
        checkW = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        photonView = GetComponent<PhotonView>();
    }   

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo indo){
        if (stream.IsWriting)
        {
            stream.SendNext(Direction.localScale);
        }
        else
        {   
            Direction = (Transform) stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        if (!photonView.IsMine) return;

        if (Input.GetKey(KeyCode.Escape)){
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }


        coinValue.text = countCoin.ToString();

        // передвижение персонажа
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            var v = new Vector3(x, 0.0f, z);
            transform.position += v * speed * Time.deltaTime;
            animator.Play("Player_Run");
        }

        else{
            animator.Play("Player_stay");
        }

        // сторона персонажа
        if (Input.GetKey(KeyCode.D) && !isRight && !Input.GetKey(KeyCode.A)) {
            
            isRight = true;
            Vector3 theScale = gameObject.transform.localScale;
            theScale.x *= -1;
            gameObject.transform.localScale = theScale;
            Direction = gameObject.transform;
        }
        
        else if (Input.GetKey(KeyCode.A) && isRight && !Input.GetKey(KeyCode.D)) {
            isRight = false;
            Vector3 theScale = gameObject.transform.localScale;
            theScale.x *= -1;
            gameObject.transform.localScale = theScale;
            Direction = gameObject.transform;
        }   

        //отображение жизней персонажа
        hpbar.value = health;

        if (health <= 0) {
            Time.timeScale = 0;
            dead.SetActive(true);
            AudioListener.volume = 0;
        }

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
            if(Input.GetKey(KeyCode.Z) && usingJarDelay <= 0 && countJar[0] >= 1 && health < maxHealth)
            {
                usingJarDelay = 1;
                if(health + 35 < maxHealth)
                {
                    health += 35;
                    countJar[0]--;
                    textHealth.text = "+35 хп";
                }    
                else if (health + 35 >= maxHealth)
                {
                    textHealth.text = (maxHealth - health).ToString();
                    health = maxHealth;
                    countJar[0]--;
                }
                
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
                            hit.transform.gameObject.GetComponent<AI>().health -= (damage + damageJar + damageHand) * 1.5f;
                        }
                        else if(hit.transform.CompareTag("wall"))
                        {
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 1f, hit.transform.position.z);
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
                        else if(hit.transform.CompareTag("wall"))
                        {
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 5f, hit.transform.position.z);
                        }
                        else {
                            weaponNow.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 0.5f, hit.transform.position.z);
                        }
                    }
                }
                
            damage = 2;
            currentWeapon = 0;
            } 
        }

        if (Input.GetKeyDown(KeyCode.Space) & is_ground) {        //если нажата кнопка "пробел" и игрок на земле
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);   //то придаем ему силу вверх импульсным пинком
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

                    other.GetComponent<AI>().health -= damage + damageJar + damageHand;
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
                        weaponNow.GetComponent<SpriteRenderer>().sortingOrder = 0;
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
                                weaponNow.GetComponent<SpriteRenderer>().sortingOrder = 1;
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
                                weaponNow.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                break;
                            }
                        }
                    }
                }
            }
        }

        // экипировка голова
        if (other.CompareTag("equipment_head")){
            // отоброжение характеристик
            


            if (Input.GetKey(KeyCode.E)) {
                if (!checkW) {
                    // смена головного убора
                    if (currentHead != 0) {
                        maxHealth = startHealth;
                        headNow.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                        headNow.transform.parent = null;
                        headNow.GetComponent<Collider>().enabled = true;

                        
                        // изменения слоя
                        headNow.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    }

                    checkW = true;
                    pickDelay = 0.5f;

                    currentHead = int.Parse(other.name);
                    for (int i=0; i < 10; i++) {
                        if (i == currentHead) {
                            if (isRight) {

                                Vector3 theScale = other.transform.localScale;
                                theScale.x *= -1;
                                other.transform.localScale = theScale;

                                other.transform.parent = pountForHead;
                                other.transform.position = pountForHead.position;
                                maxHealth += headList[i];

                                hpbar.maxValue = maxHealth;

                                headNow = other.gameObject; 
                                headNow.GetComponent<SpriteRenderer>().sortingOrder = 2;
                                headNow.GetComponent<Collider>().enabled = false;
                                break;
                            }
                            else {
                                
                                Vector3 theScale = other.transform.localScale;
                                theScale.x *= -1;
                                other.transform.localScale = theScale;

                                maxHealth += headList[i];
                                hpbar.maxValue = maxHealth;

                                other.transform.parent = pountForHead;
                                other.transform.position = pountForHead.position;
                                
                                headNow = other.gameObject; 
                                headNow.GetComponent<SpriteRenderer>().sortingOrder = 2;
                                headNow.GetComponent<Collider>().enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        // экипировка рука
        if (other.CompareTag("equipment_hand")){
            // отоброжение характеристик
            


            if (Input.GetKey(KeyCode.E)) {
                if (!checkW) {
                    // смена браслета
                    if (currentHand != 0) {
                        damageHand = 0;
                        handNow.GetComponent<Collider>().enabled = true;
                        handNow.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                        handNow.transform.parent = null;
                        
                        // изменения слоя
                        handNow.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }

                    checkW = true;
                    pickDelay = 0.5f;

                    currentHand = int.Parse(other.name);
                    for (int i=0; i < 10; i++) {
                        if (i == currentHand) {
                            if (isRight) {

                                Vector3 theScale = other.transform.localScale;
                                theScale.x *= -1;
                                other.transform.localScale = theScale;

                                other.transform.parent = pountForHand;
                                other.transform.position = pountForHand.position;
                                damageHand = handList[i];

                                handNow = other.gameObject; 
                                handNow.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                handNow.GetComponent<Collider>().enabled = false;
                                break;
                            }
                            else {
                                
                                Vector3 theScale = other.transform.localScale;
                                theScale.x *= -1;
                                other.transform.localScale = theScale;

                                other.transform.parent = pountForHand;
                                other.transform.position = pountForHand.position;
                                damageHand = handList[i];

                                handNow = other.gameObject; 
                                handNow.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                handNow.GetComponent<Collider>().enabled = false;
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

        //оканчание уровня
        if (other.CompareTag("finish")){
            finish.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
     void OnTriggerExit(Collider col){              //если из триггера что то вышло и у обьекта тег "ground"
        if (col.tag == "ground") {
            is_ground = false;
        }     //то выключаем переменную "на земле"
    }
}
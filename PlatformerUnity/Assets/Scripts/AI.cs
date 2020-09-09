using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    GameObject player;
    public float health;
    float lastHealth;
    float attackSped = 1;
    float atakDelay = 1;
    float timeDamage = 1;
    bool timeDamageForBot;
    public float damage;
    int randomJars;
    public GameObject coin;
    public GameObject jar;
    public GameObject damageForBot;
    public List<GameObject> jars; 
    public TextMesh damageBotText;
    float demageHealth;
    bool isLeft = true;
    public GameObject aiSprite;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        lastHealth = health;
    }


    // Update is called once per frame
    void Update()
    {   
        isLeft = player.GetComponent<PlayerControl>().isRight;
        // поворот в сторону игрока
        //transform.LookAt(player.transform);

        
        /*Vector3 direction = (goal.position - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, direction.y, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 500 * Time.deltaTime);*/

        if (!isRight && player.transform.position.x < aiSprite.transform.position.x){
            Vector3 theScale = aiSprite.transform.localScale;
            theScale.x *= -1;
            aiSprite.transform.localScale = theScale;
            isRight = true;
        }
        else if (isRight && player.transform.position.x > aiSprite.transform.position.x) {
            Vector3 theScale = aiSprite.transform.localScale;
            theScale.x *= -1;
            aiSprite.transform.localScale = theScale;
            isRight = false;
        }


        // ходьба за игроком
        goal = player.transform;
        agent.destination = goal.position;

        // уничтоение врага
        if (health <= 0) {

            // рандомное выпадение бафов
            if (Random.Range(0, 100) <= 20){
                randomJars = Random.Range(0, 3);
                jar = jars[randomJars];
                Instantiate(jar, new Vector3(gameObject.transform.position.x + Random.Range(0.4f, 1.9f), gameObject.transform.position.y+1, gameObject.transform.position.z + Random.Range(0.4f, 1.9f)), jar.transform.rotation);
            }

            // выпадение рандомное кол-во монет
            for (int i  = Random.Range(0, 10); i < 10; i++)
            {
                Instantiate(coin, new Vector3(gameObject.transform.position.x + Random.Range(0.4f, 1.9f), gameObject.transform.position.y+1, gameObject.transform.position.z + Random.Range(0.4f, 1.9f)), coin.transform.rotation);
            }

            Destroy(gameObject);
        }

        if (health < lastHealth) {
            demageHealth = lastHealth - health;
            damageBotText.text = demageHealth.ToString();
            lastHealth = health;
            
            Instantiate(damageBotText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z + 1), damageBotText.transform.rotation);
            
        }

        // задержка до исчезвнавения урона
        

        // задержка атаки
        if(atakDelay <= attackSped)
        {
            atakDelay += 1 * Time.deltaTime;
        }
    }
    
    private void OnTriggerStay(Collider other)
   {    
       // атака игрока
       if(other.tag == "Player")
        {
            if(atakDelay >= attackSped)
            {
                other.GetComponent<PlayerControl>().health -= damage;
                atakDelay = 0;
            }
        }
    }

}
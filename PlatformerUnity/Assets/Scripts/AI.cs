﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    GameObject player;
    public float health;
    float attackSped = 1;
    float atakDelay = 1;
    float damage = 10;
    int randomNumber;
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 100f;
    }


    // Update is called once per frame
    void Update()
    {   
        // ходьба за игроком
        goal = player.transform;
        agent.destination = goal.position;

        // уничтоение врага
        if (health <= 0) {

            randomNumber = Random.Range(0, 10);

            for (int i  = randomNumber; i < 10; i++)
            {
                Instantiate(coin, new Vector3(gameObject.transform.position.x + Random.Range(0.4f, 1.9f), gameObject.transform.position.y+1, gameObject.transform.position.z + Random.Range(0.4f, 1.9f)), coin.transform.rotation);
            }
            Destroy(gameObject);
        }

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
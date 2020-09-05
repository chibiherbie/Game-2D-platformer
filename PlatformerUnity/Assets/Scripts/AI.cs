using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    GameObject player;
    public float health;
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
        goal = player.transform;
        agent.destination = goal.position;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
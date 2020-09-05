using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    // Update is called once per frame
    void Update()
    {
        goal = player.transform;
        agent.destination = goal.position;
    }
}
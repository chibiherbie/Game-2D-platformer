using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    GameObject spawnedEnemy;
    int spawned;
    public int needSpawn;
    bool count = false;
    // Start is called before the first frame update
    void Start()
    {
        spawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned < needSpawn && count)
        {
            spawned++;
            Instantiate(enemy, new Vector3(gameObject.transform.position.x, 0.7f, gameObject.transform.position.z + 2f), enemy.transform.rotation);
            
        }
        if(spawned >= needSpawn) 
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !count)
        {
            count = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
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
            PhotonNetwork.Instantiate(enemy.name, new Vector3(gameObject.transform.position.x, 0.7f, gameObject.transform.position.z + 2f), Quaternion.identity);
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
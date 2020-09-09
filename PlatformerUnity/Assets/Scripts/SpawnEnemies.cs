using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    bool count = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && count)
        {
            Instantiate(enemy, new Vector3(gameObject.transform.position.x, 0.7f, gameObject.transform.position.z + 2f), enemy.transform.rotation);
            Destroy(gameObject);
            count = false;
        }
    }
}
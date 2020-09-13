using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectJarSingle : MonoBehaviour
{
    bool isPicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        
        // сбор предметов
        if (other.CompareTag("Player") && !isPicked) {
            isPicked = true;

            if (gameObject.name == "jarHealth" || gameObject.name == "jarHealth(Clone)") {
                other.GetComponent<PlayerControlSingle>().countJar[0]++;
            }
            if (gameObject.name == "jarSpeed" || gameObject.name == "jarSpeed(Clone)") {
                other.GetComponent<PlayerControlSingle>().countJar[1]++;
            }
            if (gameObject.name == "jarAttack" || gameObject.name == "jarAttack(Clone)") {
                other.GetComponent<PlayerControlSingle>().countJar[2]++;
            }
            Destroy(gameObject);
        }
    }
}

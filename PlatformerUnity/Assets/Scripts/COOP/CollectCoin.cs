using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{   
    public TextMesh countCoin;
    int newCoin;

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

        // сбор монет
        if (other.CompareTag("Player") && !isPicked) {
            isPicked = true;
            other.GetComponent<PlayerControl>().countCoin += 1;

            //countCoin.text = asdasdsa + "+";
            
            Instantiate(countCoin, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z + 3), countCoin.transform.rotation);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject chestMessage;
    bool showTextChest = false;
    
    // Start is called before the first frame update   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E)){
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        // открытия сундука
        if (other.CompareTag("Player") && !showTextChest) {

            // отоброжение информации
            chestMessage.SetActive(true);
            showTextChest = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.CompareTag("Player") && showTextChest) {  
            // отоброжение информации
            showTextChest = false;
            chestMessage.SetActive(false);
        }
    }
}

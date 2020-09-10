using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestMessage;
    bool showTextChest = false;
    public List<string> itemsInChest;
    int choiseItems;
    public List<GameObject> weapon;
    public List<GameObject> head;
    public List<GameObject> hand;
    GameObject player;
    
    // Start is called before the first frame update   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (showTextChest){
            if (Input.GetKey(KeyCode.E)){
                Destroy(gameObject);
                //  выбор варианта действия
                choiseItems = Random.Range(0, 3);;

                // оружие
                if (choiseItems == 0){
                    choiseItems = Random.Range(0, 4);

                    GameObject newWeapon = (GameObject)Instantiate(weapon[choiseItems], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3), weapon[choiseItems].transform.rotation);
                    newWeapon.name = (choiseItems + 1).ToString();
                }
                // экипировка
                else if (choiseItems == 1){
                    if (Random.Range(0, 2) == 0){ // head
                        choiseItems = Random.Range(0, 1);

                        GameObject newHead = (GameObject)Instantiate(head[choiseItems], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3), head[choiseItems].transform.rotation);
                        newHead.name = (choiseItems + 1).ToString();
                    }
                    else{ // hand

                    }
                }
                // проклятья
                else {
                    
                }
                
                //Instantiate(jar, new Vector3(gameObject.transform.position.x + Random.Range(0.4f, 1.9f), gameObject.transform.position.y+1, gameObject.transform.position.z + Random.Range(0.4f, 1.9f)), jar.transform.rotation);

            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layer : MonoBehaviour
{   

    GameObject player;
    public GameObject[] rock = new GameObject[10];
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        //rock1
        if (player.transform.position.z >= rock[0].transform.position.z - 3){
            rock[0].GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if (player.transform.position.z < rock[0].transform.position.z - 3) {
            rock[0].GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        //rock3
        if (player.transform.position.z >= rock[1].transform.position.z - 2){
            rock[1].GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if (player.transform.position.z < rock[1].transform.position.z - 2) {
            rock[1].GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        //stone
        if (player.transform.position.z >= rock[2].transform.position.z - 0.3){
            rock[2].GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if (player.transform.position.z < rock[2].transform.position.z - 0.3) {
            rock[2].GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}

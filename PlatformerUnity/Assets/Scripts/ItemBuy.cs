using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    GameObject player;
    public int cost;
    public TextMesh costText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        costText.text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                if(player.GetComponent<PlayerControl>().countCoin >= cost)
                {
                    player.GetComponent<PlayerControl>().countCoin -= cost;
                    if(gameObject.CompareTag("weapon_shop"))
                        gameObject.tag = "weapon";
                    else if (gameObject.CompareTag("head_shop"))
                        gameObject.tag = "equipment_head";
                    else if (gameObject.CompareTag("hand_shop"))
                        gameObject.tag = "equipment_hand";
                    Destroy(costText);
                    Destroy(this);
                }
            }
        }
    }
}

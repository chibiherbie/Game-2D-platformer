using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layer : MonoBehaviour
{   

    GameObject player;
    public GameObject rock;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z >= rock.transform.position.z - 3){
            rock.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if (player.transform.position.z < rock.transform.position.z - 3) {
            rock.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}

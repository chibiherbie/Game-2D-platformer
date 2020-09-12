using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sliderForXP : MonoBehaviour
{   
    public Slider xpBar;
    float xp;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        xp = player.GetComponent<skill>().XP;
    }

    // Update is called once per frame
    void Update()
    {   

        xp = player.GetComponent<skill>().XP;
        xpBar.value = xp;

        if (xp >= 0) {
            Vector3 theScale = xpBar.fillRect.localScale;
            theScale.x = 1f;
            xpBar.fillRect.localScale = theScale;
        }
        if (xp >= 19) {
            Vector3 theScale = xpBar.fillRect.localScale;
            theScale.x = 1.4f;
            xpBar.fillRect.localScale = theScale;
        }
        if (xp >= 33) {
            Vector3 theScale = xpBar.fillRect.localScale;
            theScale.x = 1.6f;
            xpBar.fillRect.localScale = theScale;
        }
        if (xp >= 50) {
            Vector3 theScale = xpBar.fillRect.localScale;
            theScale.x = 1.76f;
            xpBar.fillRect.localScale = theScale;
        }
    }
}

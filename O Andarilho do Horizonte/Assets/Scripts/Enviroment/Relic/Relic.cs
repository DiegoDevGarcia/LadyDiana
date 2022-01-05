using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{

    HudControl hControl;

    public static bool relicOn;
    
    void Start()
    {
        hControl = HudControl.hControl;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hControl.RelicOn();
            relicOn = true;

            Destroy(gameObject);
        }
    }
}

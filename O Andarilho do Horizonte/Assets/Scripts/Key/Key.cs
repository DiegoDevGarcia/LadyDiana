using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    HudControl hControl;

    private Animator anim;

    public static bool keyOn;

    void Start()
    {
        hControl = HudControl.hControl;

        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hControl.KeyOn();
            keyOn = true;

            Destroy(gameObject);
        }
    }
}

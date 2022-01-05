using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator anim;
    public bool enterInDoor;

    
    private void Start()
    {
        anim = GetComponent<Animator>();      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Key.keyOn == true && PlayerF.isGrounded == true)
            {
                anim.enabled = true;
                enterInDoor = true;
            }
        }
    }
}

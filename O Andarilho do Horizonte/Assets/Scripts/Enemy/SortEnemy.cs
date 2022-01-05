using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortEnemy : MonoBehaviour
{
    public Animator anim;
    public Inimigo enemy;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("attack", true);
            enemy.attaking = true;
            //GetComponent<BoxCollider2D>().enabled = false;

        }
    }
    
}

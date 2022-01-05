using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public bool flow = false;
    private bool dirRight = true;
    public float dist;
    public float speed = 1.0f;
    public Animator enemy;
    public Rigidbody2D rig;
    public Transform hero;


    private void Start()
    {
        enemy = GetComponent<Animator>();    
    }



    void Update()
    {
        Flow();

    }

    void Flip()
    {
        dirRight = !dirRight;

        Vector3 scale = this.transform.localScale;
        scale.x *= -1 ;
        this.transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.SetTrigger("attack");
            flow = true;
        }

        
    }
    
    
    private void OnCollisionEnter2D(Collision2D col) {

      if (col.gameObject.CompareTag("Player"))
          {
            
            col.gameObject.GetComponent<PlayerF>().TakeDamage(2);
          }
    }
        
        
   

   

    private void Flow()
    {
        dist = Vector2.Distance(this.transform.position, hero.transform.position);
        
        //Flip

        if ((hero.transform.position.x > this.transform.position.x) && !dirRight)
        {
            Flip();
        }else if((hero.transform.position.x < this.transform.position.x) && dirRight)
        {
            Flip();
        }

        //Flow

        if((flow == true) && dist > 0.8f)
        {
            if(hero.transform.position.x < this.transform.position.x)
            {
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                enemy.SetBool("walk", true);
                enemy.SetBool("idle", false);
            }
            else if(hero.transform.position.x > this.transform.position.x)
            {
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                enemy.SetBool("walk", true);
                enemy.SetBool("idle", false);
            }
            
        }
        else
        {
            enemy.SetBool("walk", false);
            enemy.SetBool("idle", true);

            
        }


    }

    
}

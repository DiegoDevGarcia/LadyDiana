using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public Transform playerTarget;
    public float velocity;
    public float distance;
    public Animator anim;
    public Rigidbody2D rig;

    private bool facingRigth = true;
    
    [Header("Atributos")]

    public float health;
    
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    

    void Update()
    {
        
        
            EnemyFlow();
            Attack();
        
        
    }


    public void EnemyFlow()
    {

        if (playerTarget != null)
        {


            distance = Vector2.Distance(transform.position, playerTarget.position);

            if (distance > 0.5 && distance < 3)
            {
                anim.SetBool("walk", true);
                transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, velocity * Time.deltaTime);
            
            }else if(distance > 3)
            
            {
                anim.SetBool("idle", true);
            }

            //Flip

            if ((playerTarget.transform.position.x > this.transform.position.x) && !facingRigth)
            {
                Flip();
            }
            else if ((playerTarget.transform.position.x < this.transform.position.x) && facingRigth)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRigth = !facingRigth;

        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        this.transform.localScale = scale;
    }

    public void Attack()
    {
        if(distance <= 0.5f && playerTarget != null)
        {
            rig.velocity = Vector2.zero;
            anim.SetTrigger("attack");
        }
    }

    public void Onhit(int damage)
    {
        anim.SetTrigger("hit");

        health -= damage;

        if (health <= 0)
        {
            EnemyDead();
        }

    }

    public void EnemyDead()
    {
      //rig.isKinematic = true;       
      velocity = 0;
      anim.SetTrigger("dead");
        OnDestroy();  
        
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }

    private void OnDestroy()
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Flow>());
        Destroy(gameObject, 120f);
    }
}

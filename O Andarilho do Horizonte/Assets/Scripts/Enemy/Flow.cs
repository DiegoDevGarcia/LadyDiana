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

    public bool recovering;     // está se recuperando de um ataque
    public float recoveryTime;     //coldown de recuperação
    public float recoveryCounter;     //calculo do tempo de recuperação
    
    private bool facingRigth = true;
    
    [Header("Atributos")]

    public float health;

    public Transform healthBar; // Barra verde
    public GameObject healthBarObject; // Objeto pai das barras

    private Vector3 healthBarScale;  // Tamanho da barra
    private float healthBarPercent;  // percentual de vida pára o calculo do tamanho da barra
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();

        healthBarScale = healthBar.localScale;
        healthBarPercent = healthBarScale.x / health;
        healthBarObject.SetActive(false);
    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthBarPercent * health;
        healthBar.localScale = healthBarScale;
    }

    void Update()
    {
                
        EnemyFlow();
        Attack();
        RecoveryTime();
        

    }

    public void RecoveryTime()
    {
        if (recovering) //Cooldown de recuperação
        {
            HealBarAtctive();

            recoveryCounter += Time.deltaTime;
            if (recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovering = false;
                HealBarDesatctive();
            }
        }

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

        //Rotacionar barra de vida 
        healthBarObject.transform.localScale = new Vector3(healthBarObject.transform.localScale.x * -1, healthBarObject.transform.localScale.y, healthBarObject.transform.localScale.y);
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
        recovering = true;

       

        anim.SetTrigger("hit");

        health -= damage;

        UpdateHealthBar();

        if (health <= 0)
        {
            healthBarObject.SetActive(false);
            
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

    public void HealBarAtctive()
    {
        healthBarObject.SetActive(true);
    }

    public void HealBarDesatctive()
    {
        healthBarObject.SetActive(false);
    }
}

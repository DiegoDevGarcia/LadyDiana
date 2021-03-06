using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerF : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Animator playerAnim;

    private float moveInput;
    public static bool isGrounded;
    public Transform foot;


    

    private bool facingRight = true;

    [Header("Parametros Player")]
    public float moveSpeed;
    public float jumpForce;
    public int health;                 //Hp
   

    [Header("Parametros Colisores")]
    public LayerMask whatIsGround;
    public LayerMask enemyLayers;  //Layer do inimigo


    [Header("System")]
    public bool attacking;
    private int facingDirection = 1; // 1 direita / -1 esquerda
    private bool recovering;     // está se recuperando de um ataque
    private bool canMove = true;  // permite bloquear a movimentação
    public float recoveryTime;     //coldown de recuperação
    public float recoveryCounter;     //calculo do tempo de recuperação
    public float attackTimer; //calculo do cooldown do ataque
    public GameObject vCam;    // Camera para executar efeitos

    [Header("Combat System")]
    public int attackDamage; //Dano do ataque
    public Transform attackHit; // ponto de origem do ataque
    public float attackRange; //distancia do ataque
    public float attackCooldown;

    
    [Header("Dash System")]

    private float dashAtual; //Duração do Dash atual
    private bool canDash = true; // variavel de controle para saber se já pode executar novo dash
    //public static bool isDashing; //controle de animação
    public float duracaoDash; //curação da utilização do dash
    public float dashSpeed; // velocidade do dash
    public float dashColldown; // recarga do dash, tempo minimo para executar o dash novamente

    [Header("Health System")]
    public Transform healthBar; //barra verde
    public GameObject healthBarObject; //objeto pai das barras
    private Vector3 healthBarScale; // tamanho da barra
    private float healthPercent; // percentual de vida para calculo do tamanho da barra

    //[Header("Energy System")]
    

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;
        canDash = true;
        dashAtual = duracaoDash;

    }

    
    private void FixedUpdate()
    {
        Dash();
    }
    void Update()
    {
        if (canMove)
        {
            Move();
        }
        
        Jump();
        CheckSurroundings();
        SetAnimations();
        Attack();

        if(recovering) //Cooldown de recuperação
        {
            recoveryCounter += Time.deltaTime;
            if(recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovering = false;
            }
        }

        
    }

    void CheckInput()
    {
        if(attackTimer <= 0) // se o ataque nao esta em cooldown
        {
            if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                Attack();
                attackTimer = attackCooldown;
            } 
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void SetAnimations()
    {
        playerAnim.SetBool("isGround", isGrounded);
        //playerAnim.SetBool("isDashing", isDashing);
        playerAnim.SetFloat("speedY", playerRb.velocity.y);
    }

    private void Move()
    {   
        // moveInput é multiplicado por +1 quando vai para direita e -1 para esquerda
        moveInput = Input.GetAxis("Horizontal"); // variavel recebendo o comando de apertar o botao para se mover

        playerRb.velocity = new Vector2(moveInput * moveSpeed, playerRb.velocity.y);
        playerAnim.SetFloat("velocity", Mathf.Abs(moveInput));

        Flip();
    }

    /*void Dash()
    {
        if(Input.GetButtonDown("Fire3") && isGrounded && canDash)
        {
            if (dashAtual <= 0)
            {
                StopDash();
            }
            else
            {
               // isDashing = true; 
                playerAnim.SetTrigger("Dash");

                dashAtual -= Time.deltaTime;

                //DashParticle(); criar um sistema de particulas para o dash

                if (facingRight)
                {
                    playerRb.velocity = Vector2.right * dashSpeed;
                }
                else
                {
                    playerRb.velocity = Vector2.left * dashSpeed;
                }
            }

        }

        if(Input.GetButtonUp("Fire3"))
        {
           // isDashing = false; //playerAnim.SetBool("isDashing", false);
            canDash = true;
            dashAtual = duracaoDash;
        }
    }
    */

    public void Dash()
    {
        if (canDash && isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashAtual <= 0)
            {
                StopDash();
                dashAtual += Time.deltaTime;
                canDash = true;
            }
            else
            {
                // isDashing = true; 
                playerAnim.SetTrigger("Dash");

                dashAtual -= Time.deltaTime;

                //DashParticle(); criar um sistema de particulas para o dash

                if (facingRight)
                {
                    playerRb.velocity = Vector2.right * dashSpeed;
                }
                else
                {
                    playerRb.velocity = Vector2.left * dashSpeed;
                }
            }


        }
    }
     
        
        
        
    private void StopDash()
    {
        playerRb.velocity = Vector2.zero;
        dashAtual = duracaoDash;
        //isDashing = false;  //playerAnim.SetBool("isDashing", false); 
        canDash = false;
    }

    private void Flip()
    {
        if((moveInput < 0 && facingRight)||(moveInput > 0 && !facingRight))
        {
            facingDirection *= -1;
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    IEnumerator Freeze()
    {
        //Retira o controle do personagem
        canMove = false;

        yield return new WaitForSeconds(.5f);

        //Devolve o controle ao personagem

        canMove = true;

    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthPercent * health;
        healthBar.localScale = healthBarScale;
    }

   
    public void TakeDamage (int damage)
    {
        

        playerAnim.SetTrigger("hurt");
        Knockback();
        
        health -= damage;
        UpdateHealthBar();

        recovering = true;
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        moveSpeed = 0;
        
        playerAnim.SetTrigger("die");
        Destroy(gameObject,1.5f);
        
    }

    void Knockback()
    {
        playerRb.AddForce(new Vector2(5 * -facingDirection, 5), ForceMode2D.Impulse);

        StartCoroutine("Freeze");
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //Altera a velocidade no eixo y por conta do vector2.up, multiplicado pela força do pulo
            playerRb.velocity = Vector2.up * jumpForce;
            //playerAnim.SetFloat("speedY", playerRb.velocity.y);
        }
    }

    void CheckSurroundings()
    {
        // vai criar um circulo, na posicao do pe, com o tamanho 0.3f  na layer ground

        isGrounded = Physics2D.OverlapCircle(foot.position, 0.3f, whatIsGround);
         
    }

    void Attack()
    {

        

        if (Input.GetButtonDown("Fire1"))
        {
            

            if (isGrounded)
                {
                
                attacking = true;

                playerAnim.SetTrigger("Attack");
                }
            
        }

        

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Potion1")
        {
            health = health + 2;
            UpdateHealthBar();

        }

    }


}

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

    public Transform healthBar; //barra verde
    public GameObject healthBarObject; //objeto pai das barras
    private Vector3 healthBarScale; // tamanho da barra
    private float healthPercent; // percentual de vida para calculo do tamanho da barra
    
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;
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
        //vCam.GetComponent<CameraController>().CameraShake(); Rever o conceito para acertar

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
        playerRb.velocity = Vector2.zero;
        playerAnim.SetTrigger("die");
        Destroy(gameObject,1.7f);
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

   

}

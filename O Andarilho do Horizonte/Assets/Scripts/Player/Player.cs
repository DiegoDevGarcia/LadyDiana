using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private RaycastHit2D ground;

    private float moveInput;

    private bool isGrounded;
    private bool facingRight = true;

    private int nroPulos;
    private int facingDirection = 1;

    [Header("Parametros Player")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Parametros Colisores")]
    public LayerMask whatIsGround;
    public Vector2 raycastOffset;




    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
    }

    void LoadComponents()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        SetAnimations();
        CheckSurroundings();
        CheckInput();
    }

    void SetAnimations()
    {
        playerAnim.SetBool("grounded", isGrounded);
        playerAnim.SetFloat("speedy", playerRb.velocity.y);
    }

    void CheckSurroundings()
    {
        // verifica se esta no chao
        ground = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, 1f, whatIsGround);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + raycastOffset.y), Vector2.down, Color.yellow);

        if (ground.collider != null)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;

    }

    public void CheckInput()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

       /* if (attackTimer <= 0) // Se o ataque nao esta em cooldown
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

        }*/
    }

    private void Move()
    {
        //Movimentacao do player
        moveInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(moveInput * moveSpeed, playerRb.velocity.y);
        playerAnim.SetFloat("Player_velocity", Mathf.Abs(moveInput));

        /*if (isWallSliding)
        {
            if (playerRb.velocity.y < -wallSlideSpeed)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -wallSlideSpeed);

            }
        }

        if ((moveInput < 0 && facingRight) || (moveInput > 0 && !facingRight))
        {
            Flip();
        }*/
    }

    void Jump()
    {
        if (nroPulos > 0)
        {
            //CreateDust();
            nroPulos--;
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce);

        }
    }

    void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        //CreateDust();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            //CreateDust();
            nroPulos = 1;
        }
    }

   /* void CreateDust()
    {
        dust.Play();
    }*/
}
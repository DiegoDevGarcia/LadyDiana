using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    bool isRight = true;

    [Header("Componemts")]

    public Rigidbody2D enemiyRb;
    public float moveSpeed;
    private int direction = 1;

    [Header("Check")]

    public float distance;
    public Transform groundCheck;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        enemiyRb.velocity = new Vector2(moveSpeed * direction, enemiyRb.velocity.y);

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);


        if (ground.collider == false)
        {


            if (isRight == true)
            {
                direction = 1;
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
                direction = -1;
            }
        }    
    }  
}

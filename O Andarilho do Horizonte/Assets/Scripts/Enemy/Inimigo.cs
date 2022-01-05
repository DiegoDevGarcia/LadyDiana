using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    public int routine;
    public float crono;
    public Animator anim;
    public int direction;
    public float speedWalk;
    public float speedrun;
    public GameObject target;

    public float sort_vision;
    public float sort_attack;
    public bool attaking;
    public GameObject sort;
    public GameObject Hit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        behaviour();
    }


    public void behaviour() //comportamentos
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > sort_vision && !attaking)
        {


            anim.SetBool("run", false);
            crono += 1 * Time.deltaTime;

            if (crono >= 4)
            {
                routine = Random.Range(0, 2);
                crono = 0;
            }


            switch (routine)
            {
                case 0:
                    anim.SetBool("walk", false);
                    break;

                case 1:
                    direction = Random.Range(0, 2);
                    routine++;
                    break;


                case 2:

                    switch (direction)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speedWalk * Time.deltaTime);
                            break;


                        case 1:

                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speedWalk * Time.deltaTime);
                            break;
                    }

                    anim.SetBool("walk", true);
                    break;

            }
        }

        else
        {
            if(Mathf.Abs(transform.position.x - target.transform.position.x) > sort_attack && !attaking)
            {
                if(transform.position.x < target.transform.position.x)
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("run", true);
                    transform.Translate(Vector3.right * speedrun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    anim.SetBool("attack", false);
                }
                else
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("run", true);
                    transform.Translate(Vector3.right * speedrun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    anim.SetBool("attack", false);
                }
            }

            else
            {
                if (!attaking)
                {
                    if(transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);
                }
            }
        }
    }

    public void Final_ani()
    {
        anim.SetBool("attack", false);
        attaking = false;
        sort.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public Animator anim;
    public GameObject key;
    public BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            anim.enabled = true;
            key.SetActive(true);

            Destroy(boxCollider);
        }
    }
}

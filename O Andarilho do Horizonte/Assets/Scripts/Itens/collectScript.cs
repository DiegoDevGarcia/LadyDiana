using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectScript : MonoBehaviour
{
    private Rigidbody2D itemRb;
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(new Vector2(Random.Range(-3f, 3f), 2f), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CollectItem();

        }
        
    }

    private void CollectItem()
    {

        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
	
    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<PlayerF>().TakeDamage(2);
		}
	}

    
}

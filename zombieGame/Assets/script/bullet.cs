using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int bulletDamage=20;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target")){
            print("hit " + collision.gameObject.name);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Zombie")){
            collision.gameObject.GetComponent<zombie>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}

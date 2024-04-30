using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("player die");
        }
        else
        {
            print("player damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("collisotino");
        if(other.CompareTag("zombieHand")){
            TakeDamage(25);
        }
    }
}

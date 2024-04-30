using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{

    [SerializeField] private int HP = 100;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount){
        HP -= damageAmount;

        if(HP <= 0){
            // animator.SetTrigger("DIE");
            Destroy(gameObject);
        }else{
            //animator.SetTrigger("DAMAGE");
        }
        print("HP : "+HP);
    }
    
}

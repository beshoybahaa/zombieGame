using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{

    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount){
        HP -= damageAmount;

        if(HP <= 0){
            animator.SetTrigger("DIE1");
            // Destroy(gameObject);
        }else{
            //animator.SetTrigger("DAMAGE");
        }
        print("HP : "+HP);
    }

    private void Update() {
        if (navAgent.velocity.magnitude > 0.1f){
            animator.SetBool("isWalking",true);
        }else{
            animator.SetBool("isWalking", false);
        }
    }
}

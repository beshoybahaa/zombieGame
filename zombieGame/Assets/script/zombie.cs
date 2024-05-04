using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{

    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent navAgent;
    public bool isDead = false;
    public AudioSource audioSource;
    public AudioClip attackSF;
    public AudioClip deadSF;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        audioSource.clip = attackSF;
        audioSource.loop = true;
        audioSource.Play();

    }

    public void TakeDamage(int damageAmount){
        HP -= damageAmount;

        if(HP <= 0){
            isDead=true;
            transform.gameObject.tag = "Wall";
            audioSource.PlayOneShot(deadSF);
            animator.SetTrigger("DIE1");
            Example();
            audioSource.Stop();
            
            // Destroy(gameObject);
        }
        else{
            
            animator.SetTrigger("DAMAGE");
        }
        print("HP : "+HP);
    }
    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(4);
        print(Time.time);
    }
}

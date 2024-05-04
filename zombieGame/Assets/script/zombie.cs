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
    private AudioSource audioSource;
    public AudioClip attackSF;
    public AudioClip deadSF;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSF;
        audioSource.Play();

    }

    public void TakeDamage(int damageAmount){
        HP -= damageAmount;

        if(HP <= 1){
            death();
        }
        else{
            
            animator.SetTrigger("DAMAGE");
        }
    }

    public void death(){
        audioSource.Stop();
        isDead = true;
        transform.gameObject.tag = "Wall";
        audioSource.clip = deadSF;
        audioSource.PlayOneShot(deadSF);
        int deathNum = Random.Range(1, 3);
        animator.SetTrigger("DIE" + deathNum.ToString());
        print("DIE" + deathNum.ToString());
        wait();
        print("disable");
        enabled = false;
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(10);
    }
}

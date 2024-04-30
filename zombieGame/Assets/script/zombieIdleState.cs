using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f;
    Transform player;
    public float detectionAreaRaduis=18f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer = 0;
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer>idleTime){
            animator.SetBool("isPatroling",true);
        }

        float distanceFromPlayer = Vector3.Distance(player.position,animator.transform.position);
        if(distanceFromPlayer<detectionAreaRaduis){
            animator.SetBool("isChasing",true);
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class zombiePatroliingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;
    Transform player;
    NavMeshAgent agent;
    public float detectionArea = 18f;
    public float patrolingSpeed = 2f;

    List<Transform> wayPointsList = new List<Transform>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = patrolingSpeed;
        timer=0;

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("wayPoints");
        foreach(Transform t in waypointCluster.transform){
            wayPointsList.Add(t);
        }

        Vector3 nextPosition = wayPointsList[Random.Range(0,wayPointsList.Count)].position;
        agent.SetDestination(nextPosition);
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance<=agent.stoppingDistance){
            agent.SetDestination(wayPointsList[Random.Range(0, wayPointsList.Count)].position);
        }

        timer += Time.deltaTime;
        if(timer>patrolingTime){
            animator.SetBool("isPatroling",false);
        }

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
    }

}

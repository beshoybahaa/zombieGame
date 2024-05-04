using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f *2;
    public float jumHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        //resetting the default velocity
        if(isGrounded && velocity.y<0){
            velocity.y = -2f;
        }
        //get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // creating the moving vector
        Vector3 move = transform.right * x + transform.forward * z;

        // acutal moving the player
        // controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButton("Fire3")){
            controller.Move(move * speed*2 * Time.deltaTime);
        }
        else{
            controller.Move(move * speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumHeight*-2f/gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity*Time.deltaTime);

        if(lastPosition != gameObject.transform.position && isGrounded == true){
            isMoving=true;
        }else{
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}

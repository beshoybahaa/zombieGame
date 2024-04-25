using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMovement : MonoBehaviour
{

    public float mouseSensitivity = 500f;
    float xRotation = 0f;
    float yRotation = 0f;
    float topClmap = -90f;
    float bottomClmap = 90f;

    // Start is called before the first frame update
    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible
        // Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation around the x axis (Look up and down)
        xRotation -= mouseY;

        //Clamp the rotation
        xRotation = Mathf.Clamp(xRotation,topClmap,bottomClmap);

        // Rotation around the x axis(Look left and right)
        yRotation += mouseX;

        //apply rotation to our transform
        // transform.localRotation = Quaternion.Euler(xRotation,yRotation,0f);
        transform.localRotation = Quaternion.Euler(xRotation,yRotation,0f);

    }
}

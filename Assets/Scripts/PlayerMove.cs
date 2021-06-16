using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    Camera cam;
    public float BaseSpeed = 20f;    
    public float SprintSpeed = 1.5f;
    private float CurrentSpeed = 20f;
    public float jumpHeight = 20f;
    public float MouseSense = 10f;

    private float xL = 0;
    private float yL = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }

    void Update()
    {
        //Look
        xL += (-Input.GetAxis("Mouse Y") * MouseSense) * Time.deltaTime;
        yL += (Input.GetAxis("Mouse X") * MouseSense) * Time.deltaTime;

        xL = Mathf.Clamp(xL, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(xL, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yL, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        //Move
        float xMouseAxis = (Input.GetAxisRaw("Horizontal") * CurrentSpeed) * Time.deltaTime;
        float yMouseAxis = (Input.GetAxisRaw("Vertical") * CurrentSpeed) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += jumpHeight * Vector3.up;            
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentSpeed = BaseSpeed * SprintSpeed;
        }
        else
        {
            CurrentSpeed = BaseSpeed;
        }

        Vector3 MovePos = transform.right * xMouseAxis + transform.forward * yMouseAxis;

        Vector3 newMovePos = new Vector3(MovePos.x, rb.velocity.y, MovePos.z);
        rb.velocity = newMovePos;


        //Keep Velocity When Jumping
        //if (xMouseAxis != 0 || yMouseAxis != 0)
        //{
        //    Vector3 newMovePos = new Vector3(MovePos.x, rb.velocity.y, MovePos.z);
        //    rb.velocity = newMovePos;
        //}
    }    
}

using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float jumpForce_Y = 30f;

   

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        

    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(0f, 0f);
        rb.freezeRotation = false;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.x = +1;
            // Debug.Log("Pressing");
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpFunction(jumpForce_Y);
        }

       


        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += moveDir * Time.deltaTime * walkSpeed;
        Debug.Log(moveDir);
    }

    private void jumpFunction(float jumpForce_Y)
    {

        Vector3 jumpForce = new Vector3(0f, jumpForce_Y, 0f);
        rb.AddForce(jumpForce, ForceMode.Force);
    }
}

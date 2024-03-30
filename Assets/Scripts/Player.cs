using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float jumpForce_Y = 30f;
    [SerializeField] private float sprintSpeed = 14f;



    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(0f, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.x = +1;
            // Debug.Log("Pressing");
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpFunction(jumpForce_Y);
        }






        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.y, 0f, inputVector.x);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += moveDir * Time.deltaTime * sprintSpeed;
        }
        else
        {
            transform.position += moveDir * Time.deltaTime * walkSpeed;
        }


        Debug.Log(moveDir);
    }

    private void jumpFunction(float jumpForce_Y)
    {

        Vector3 jumpForce = new Vector3(0f, jumpForce_Y, 0f);
        rb.AddForce(jumpForce, ForceMode.Force);
    }
}

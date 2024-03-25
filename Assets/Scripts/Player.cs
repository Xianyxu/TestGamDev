using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float jumpForce_Y = 30f;

    private Vector2 inputVector = new Vector3(0f, 0f);

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {


        if (Input.GetKey(KeyCode.W))
        {
            inputVector.x = +1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.S))
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
    }

    private void jumpFunction(float jumpForce_Y)
    {

        Vector3 jumpForce = new Vector3(0f, jumpForce_Y, 0f);
        rb.AddForce(jumpForce, ForceMode.Force);
    }
}

using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float jumpForce_Y = 30f;

    private Vector3 inputVector = new Vector3(0f, 0f, 0f);

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(transform.position.x, 0f, 0f) * Time.deltaTime * walkSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(transform.forward * Time.deltaTime * walkSpeed * -1f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpFunction(jumpForce_Y);
        }
    }

    private void jumpFunction(float jumpForce_Y)
    {

        Vector3 jumpForce = new Vector3(0f, jumpForce_Y, 0f) + inputVector;
        rb.AddForce(jumpForce, ForceMode.Force);
    }
}

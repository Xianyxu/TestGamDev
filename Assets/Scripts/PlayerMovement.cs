using System;
using System.Collections;
using System.Collections.Generic;
using GameDev1.Assets.Scripts.Code;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float forceMultiplier;
    private bool readyToJump;


    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;


    [Header("KeyBindings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;


    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // ground Check
        float raycastDeltaAdd = .2f;
        grounded = Physics.Raycast(transform.position, Vector3.down,
                             playerHeight * ConstantClass.floatHalf + raycastDeltaAdd, whatIsGround);
        Debug.Log("grounded status after raycast:  " + grounded + " MaxDistance: " + playerHeight * ConstantClass.floatHalf + raycastDeltaAdd);

        Console.WriteLine("new LIne");
        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }



        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw(ConstantClass.HORIZONTAL);
        verticalInput = Input.GetAxisRaw(ConstantClass.VERTICAL);

        
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;



        // on ground
        if (grounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * forceMultiplier, ForceMode.Force);
        }
        else if (!grounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);

        }


    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}

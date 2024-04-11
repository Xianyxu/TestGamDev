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


    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;


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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * ConstantClass.floatHalf + raycastDeltaAdd, whatIsGround);


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
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw(ConstantClass.HORIZONTAL);
        verticalInput = Input.GetAxisRaw(ConstantClass.VERTICAL);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float forceMultiplier = 10f;
        rb.AddForce(moveDirection.normalized * moveSpeed * forceMultiplier, ForceMode.Force);
    }
}

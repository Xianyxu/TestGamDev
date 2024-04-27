using System;
using System.Collections;
using System.Collections.Generic;
using GameDev1.Assets.Scripts.Code;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float forceMultiplier;
    private bool readyToJump;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchScale;
    private float startYScale;
    private float raycastMaxDistance = 0.5f;


    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded = true;

    [Header("Slope Handling")]
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float slopeForceMultiplier;
    private RaycastHit slopeHit;


    [Header("KeyBindings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;


    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    Rigidbody rb;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        onAir,
    }

    [Header("StateMachine")]
    [SerializeField] protected MovementState state;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground Check
        grounded = Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.1f), Vector3.down,
                             raycastMaxDistance, whatIsGround);


        // Debug.Log("grounded status after raycast:  " + grounded + " MaxDistance: " + raycastMaxDistance +
        //                   "   readu to jump status:" + readyToJump + "   state: " + state + "   movement speed: " + moveSpeed);

        Debug.Log("On Slope Status: " + OnSlope() + "    SlopeHit Info: " + slopeHit);

        // Debug.DrawRay(transform.position + new Vector3(0f, 0.2f, 0.1f), Vector3.down * raycastMaxDistance, Color.black);
        Debug.DrawRay(transform.position, slopeHit.normal);


        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

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

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.lossyScale.x, startYScale, transform.localScale.z);
        }

    }

    private void StateHandler()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.onAir;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * slopeForceMultiplier, ForceMode.Force);
        }

        // on ground
        if (grounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * forceMultiplier, ForceMode.Force);
        }
        else if (!grounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);

        }

        // turm off gravity
        rb.useGravity = !OnSlope();


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

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.1f), Vector3.down, out slopeHit, raycastMaxDistance + 2f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using GameDev1.Assets.Scripts.Code;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;


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

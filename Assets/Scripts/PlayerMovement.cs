using System.Collections;
using System.Collections.Generic;
using GameDev1.Assets.Scripts.Code;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header(ConstantClass.MOVEMENT)]
    [SerializeField]private float moveSpeed;
    [SerializeField]private Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private float forceMultiplier;

    private Vector3 moveDirection;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update() {
        MyInput();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw(ConstantClass.HORIZONTAL);
        verticalInput = Input.GetAxisRaw(ConstantClass.VERTICAL);
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * forceMultiplier, ForceMode.Force);
    }

    
}

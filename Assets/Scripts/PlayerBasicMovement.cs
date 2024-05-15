using System;
using UnityEngine;
using GameDev1.Assets.Scripts.Code;
using Unity.Burst.CompilerServices;

public class PlayerBasicMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private Transform orientation;


    private float horizontalInput;
    private float verticalInput;

    private float moveSpeed;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw(ConstantClass.HORIZONTAL);
        verticalInput = Input.GetAxisRaw(ConstantClass.VERTICAL);
    }
}

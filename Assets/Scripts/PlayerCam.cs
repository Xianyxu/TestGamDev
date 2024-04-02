using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev1.Assets.Scripts.Code;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float senX;
    [SerializeField] private float senY;

    [SerializeField] private Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw(ConstantClass.MOUSE_X) * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw(ConstantClass.MOUSE_Y) * Time.deltaTime * senY;

        yRotation += mouseX;   // <-----| The fuck!!?
        xRotation -= mouseY;  // <-----| Same Thing!!

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}

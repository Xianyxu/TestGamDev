using System;
using System.Collections;
using UnityEngine;
using GameDev1.Assets.Scripts.Code;

public class Player : MonoBehaviour
{

    private void Start()
    {

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
            
        }


        Debug.Log(ConstantClass.HORIZONTAL);


    }
}

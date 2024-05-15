using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform toAnimate;

    private const string IS_WALKING = "IsWalking";
    private const string IS_IDLE = "IsIdle";

    private void Start()
    {
        animator = toAnimate.GetComponent<Animator>();


        animator.SetBool(IS_IDLE, true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool(IS_WALKING, true);

        }
        else
        {
            animator.SetBool(IS_IDLE, true);
            animator.SetBool(IS_WALKING, false);

        }
    }
}

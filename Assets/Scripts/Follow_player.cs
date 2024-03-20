using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraDistanceFromGround = 3f;
    [SerializeField] private float cameraDistanceFromTarget = 3f;                                   

    private void Update()
    {
        Vector3 cameraDistanceVector = new Vector3(0f, cameraDistanceFromGround, cameraDistanceFromTarget); 
        transform.position = player.transform.position - cameraDistanceVector;
        transform.LookAt(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraDistanceFromGround = System.Math.Abs(4f);
    [SerializeField] private float cameraDistanceFromTarget = System.Math.Abs(3f);

    private void Update()
    {
        Vector3 cameraDistanceVector = new Vector3(0f, -cameraDistanceFromGround, cameraDistanceFromTarget);
        


        Vector3 cameraPosition = player.transform.position - cameraDistanceVector;
        // cameraPosition = Vector3.ClampMagnitude(Vector3.zero, float.PositiveInfinity);
        Debug.Log($"Camera Position: {cameraPosition}, Camera Distance From Ground: {cameraDistanceFromGround}");
        transform.position = cameraPosition;
        transform.LookAt(player);
    }
}

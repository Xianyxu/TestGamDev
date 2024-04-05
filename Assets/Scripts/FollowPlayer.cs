using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPlayer : _BaseFollowScript
{
    [SerializeField] private float cameraDistanceFromGround = 4f;
    [SerializeField] private float cameraDistanceFromTarget = 3f;


    private void Update()
    {
        Vector3 cameraDistanceVector = new Vector3(0f, -cameraDistanceFromGround, cameraDistanceFromTarget);

        Vector3 cameraPosition = positionToFollowFrom.transform.position - cameraDistanceVector;
        // cameraPosition = Vector3.ClampMagnitude(Vector3.zero, float.PositiveInfinity);
        Debug.Log($"Camera Position: {cameraPosition}, Camera Distance From Ground: {cameraDistanceFromGround}");
        transform.position = cameraPosition;
        transform.LookAt(positionToFollowFrom);
    }
}

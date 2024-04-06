using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraScript : MonoBehaviour
{
    [SerializeField] private Transform positionToFollowFrom;

    private void Update()
    {
        transform.position = positionToFollowFrom.position;
    }
}

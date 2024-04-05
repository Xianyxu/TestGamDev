using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraScript : _BaseFollowScript
{

    private void Update()
    {
        transform.position = positionToFollowFrom.position;
    }
}

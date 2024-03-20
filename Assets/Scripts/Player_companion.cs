using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_companion : MonoBehaviour
{
    [SerializeField] private GameObject revolveAroundPlayer;
    [SerializeField] private float floatDistance = .5f;

    private void Start() {

        
    }

    private void Update() {
        Vector3 floatDistanceVector = new Vector3(0f, 0f, floatDistance);

        transform.position = revolveAroundPlayer.transform.position - floatDistanceVector;
    }

}

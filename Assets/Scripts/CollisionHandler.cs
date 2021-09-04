using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Hit");
                break;
            case "Fuel":
                Debug.Log("Fuel Hit");
                break;
            case "Finish":
                Debug.Log("Finish Hit");
                break;
            default:
                Debug.Log("Obstacle Hit");
                break;
        }
    }
}

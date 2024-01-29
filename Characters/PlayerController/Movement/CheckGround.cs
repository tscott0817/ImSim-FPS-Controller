using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private bool isOnGround = false;

    private void Update()
    {
        // You can use the 'isOnGround' variable as needed in your other scripts
        // to determine whether the player is currently colliding with the ground.
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with an object of a specific layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isOnGround = true;
            gameObject.GetComponent<AutoStep>().enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player is no longer colliding with the ground layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isOnGround = false;
            gameObject.GetComponent<AutoStep>().enabled = true;
        }
    }
}

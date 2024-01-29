using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RopeSwing
{
    private bool isAttached = false; 
    private GameObject currentRope = null; 

    public void HookRope(RaycastHit hit, GameObject player)
    {
        if (Input.GetKeyDown(ManageInputs.interactionKey))
        {
            if (!isAttached)
            {
                AttachToRope(hit.collider.gameObject, player);
            }
        }

        if (isAttached)
        {
            if (Input.GetKeyDown(ManageInputs.jumpKey))
            {
                DetachFromRope(player);
            }
            else
            {
                Vector3 rightOffset = player.transform.right * 0.25f;
                Vector3 backwardOffset = -player.transform.forward * 0.25f; // Adjust the value as needed
                Vector3 downwardOffset = -Vector3.up * 1.3f; // Adjust the value as needed
                player.transform.position = currentRope.transform.position + rightOffset + backwardOffset + downwardOffset;
            }
        }
    }

    public GameObject GetRope()
    {
        return currentRope;
    }

    public void Swing(GameObject player)
    {
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        float forceMagnitude = 2.0f; // Adjust this value as needed



        Vector3 force = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            force += player.transform.forward * forceMagnitude;
        }
        if (Input.GetKey(KeyCode.S))
        {
            force -= player.transform.forward * forceMagnitude;
        }
        if (Input.GetKey(KeyCode.A))
        {
            force -= player.transform.right * forceMagnitude;
        }
        if (Input.GetKey(KeyCode.D))
        {
            force += player.transform.right * forceMagnitude;
        }

        // Use the player movement to apply to rope
        currentRope.GetComponent<Rigidbody>().AddForce(force);
    }

    private void AttachToRope(GameObject rope, GameObject player)
    {
        currentRope = rope;
        isAttached = true;

        player.transform.position = currentRope.transform.position;
        player.transform.parent = currentRope.transform;

    }

    private void DetachFromRope(GameObject player)
    {
        isAttached = false;
        player.transform.parent = null;

        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            float upwardForce = 20.0f; // Adjust the upward force as needed
            float forwardForce = 10.0f; // Adjust the forward force as needed

            // Apply upward and forward force to the player
            playerRigidbody.AddForce(Vector3.up * upwardForce + player.transform.forward * forwardForce, ForceMode.Impulse);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Swim : MonoBehaviour
{
    [SerializeField] private float swimUpForce = 10f;
    [SerializeField] private float swimDownForce = 10f;
    private bool isInWater = false;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is in water (inside a trigger collider with the "Water" tag)
        if (isInWater)
        {
            playerRigidbody.useGravity = false;
            // Add upward force while the space key is held
            if (Input.GetKey(KeyCode.Space))
            {
                playerRigidbody.AddForce(Vector3.up * swimUpForce);
            }

            // Add downward force while the left control key is held
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerRigidbody.AddForce(Vector3.down * swimDownForce);
            }
        }
        else
        {
            playerRigidbody.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the water area
        if (other.CompareTag("Water"))
        {
            Debug.Log("In Water");
            /*playerRigidbody.useGravity = false;*/
            isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the water area
        if (other.CompareTag("Water"))
        {
            /*playerRigidbody.useGravity = true;*/
            isInWater = false;
        }
    }
}

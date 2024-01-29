using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTowardsObject : MonoBehaviour
{
    public GameObject targetObject; // The object to pull towards, set this in the Inspector
    public float pullForce = 10.0f; // Adjust the strength of the pull

    private Rigidbody rb;
    private float highestYPosition;
    private bool isPulling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        highestYPosition = transform.position.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isPulling = true;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            isPulling = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPulling && targetObject != null)
        {
            Vector3 directionToTarget = targetObject.transform.position - transform.position;
            directionToTarget.y = Mathf.Max(directionToTarget.y, 0f); // Ensure upward pull only
            rb.AddForce(directionToTarget.normalized * pullForce);

            // Keep track of the highest 'y' position
            highestYPosition = Mathf.Max(highestYPosition, transform.position.y);
        }
    }

    private void LateUpdate()
    {
        // Ensure the object stays at the highest 'y' position
        Vector3 newPosition = transform.position;
        newPosition.y = highestYPosition;
        transform.position = newPosition;
    }
}

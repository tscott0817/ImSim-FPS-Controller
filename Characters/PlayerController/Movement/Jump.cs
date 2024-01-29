using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forwardJumpForce = 2f;
    [SerializeField] private float maxAboveGroundHeight = 0.5f;
    public PhysicMaterial zeroFrictionMaterial;
    private PhysicMaterial originalMaterial;
    private Collider playerCollider;

    private float capsuleHeight;
    private Rigidbody playerRigidbody;
    private bool jumping = false;
    private bool canJump = true; // Added a flag to control the jump delay.

    private void Start()
    {
        capsuleHeight = GetComponent<CapsuleCollider>().height;
        playerRigidbody = GetComponent<Rigidbody>();

        // Get the Collider component of the player
        playerCollider = GetComponent<Collider>();

        // Store the original material of the player's Collider
        originalMaterial = playerCollider.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(ManageInputs.jumpKey) && canJump)
        {
            jumping = true;
            StartCoroutine(JumpDelay());
        }
    }

    private IEnumerator JumpDelay()
    {
        canJump = false;
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed.
        canJump = true;
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            JumpAction();
            jumping = false;
        }

        UpdatePhysicsMaterial();
    }

    private void UpdatePhysicsMaterial()
    {
        float halfHeight = capsuleHeight / 2f;
        Vector3 raycastOrigin = transform.position + Vector3.up * halfHeight;
        float raycastLength = halfHeight + maxAboveGroundHeight;

        RaycastHit hit;
        bool isGrounded = Physics.Raycast(raycastOrigin, -Vector3.up, out hit, raycastLength);

        if (isGrounded && hit.distance <= 1.0f)
        {
            // Player is in direct contact with the ground, use the original material
            playerCollider.material = originalMaterial;
        }
        else
        {
            // Player is not in direct contact with the ground, use the zero friction material
            playerCollider.material = zeroFrictionMaterial;
        }
    }

    private void JumpAction()
    {
        float halfHeight = capsuleHeight / 2f;
        Vector3 raycastOrigin = transform.position + Vector3.up * halfHeight;
        float raycastLength = halfHeight + maxAboveGroundHeight;

        Debug.DrawRay(raycastOrigin, -Vector3.up * raycastLength, Color.red, raycastLength);

        RaycastHit hit;
        bool isGrounded = Physics.Raycast(raycastOrigin, -Vector3.up, out hit, raycastLength);

        if (isGrounded)
        {
   
            float distanceToGround = Vector3.Distance(transform.position, hit.point);

            if (distanceToGround <= maxAboveGroundHeight)
            {
                playerRigidbody.velocity += Vector3.up * jumpForce;

                if (Input.GetKey(ManageInputs.moveForwardKey))
                {
                    Vector3 forwardForce = transform.forward * forwardJumpForce;
                    playerRigidbody.velocity += forwardForce;
                }
            }
        }
    }
}

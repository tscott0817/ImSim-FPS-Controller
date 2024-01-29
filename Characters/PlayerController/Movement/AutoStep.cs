using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoStep : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;


    private float lowerRaycastDistance = 0.3f;
    private float upperRaycastDistance = 0.4f;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        stepClimb();
    }

    void stepClimb()
    {
        Vector3 raycastDirection = Vector3.zero;

        if (Input.GetKey(ManageInputs.moveForwardKey))
        {
            raycastDirection += transform.forward;
        }
        if (Input.GetKey(ManageInputs.moveBackwardKey))
        {
            raycastDirection -= transform.forward;
        }
        if (Input.GetKey(ManageInputs.moveLeftKey))
        {
            raycastDirection -= transform.right;
        }
        if (Input.GetKey(ManageInputs.moveRightKey))
        {
            raycastDirection += transform.right;
        }

        if (raycastDirection != Vector3.zero)
        {
            RaycastHit hitLower;
            Debug.DrawRay(stepRayLower.transform.position, raycastDirection * lowerRaycastDistance, Color.red);
            if (Physics.Raycast(stepRayLower.transform.position, raycastDirection, out hitLower, lowerRaycastDistance))
            {

                RaycastHit hitUpper;
                Debug.DrawRay(stepRayUpper.transform.position, raycastDirection * upperRaycastDistance, Color.green);
                if (!Physics.Raycast(stepRayUpper.transform.position, raycastDirection, out hitUpper, upperRaycastDistance))
                {
                    rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
                }
            }
        }
    }
}

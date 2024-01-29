using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLook : MonoBehaviour
{

    public float snapTime;
    private bool isLooking;

    // For regular movement
    private float x_rotation = 0f;

    public float mouse_sensitivity = 100f;
    public Transform playerBody;
    public Transform playerHead;

    private Quaternion originalRotation;

    public float minRotation = -75;
    public float maxRotation = 75;
    Vector3 currentRotation;

    public GameObject items;
    public Transform temporaryParent;
    public Transform originalParent;

    void Update()
    {
        originalRotation = playerBody.transform.rotation;


        if (Input.GetKeyDown(ManageInputs.freeLookKey)) 
        {
            isLooking = true;
            CheckLook();
        }

        else if (Input.GetKeyUp(ManageInputs.freeLookKey))
        {
            isLooking = false;
            ResetHead();  // This needs to be here otherwise interferes with leaning
        }

        else
        {
            CheckLook();
        }
    }

    void CheckLook()
    {
        if (isLooking == true)
        {
            FreeMovement();

        }
        else if (isLooking == false)
        {
            RegularMovement();
        }

    }

    void RegularMovement()
    {
        float mouse_x = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

        x_rotation -= mouse_y; // 
        x_rotation = Mathf.Clamp(x_rotation, -75f, 75f);

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f); 

        playerBody.Rotate(Vector3.up * mouse_x);
    }

    void FreeMovement()
    {
        float mouse_x = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

        x_rotation -= mouse_y;
        x_rotation = Mathf.Clamp(x_rotation, -75f, 75f); // Adjust the pitch limits as needed

        // Limit the rotation left to right (yaw)
        float currentYaw = playerHead.localRotation.eulerAngles.y;
        float newYaw = currentYaw + mouse_x;
        float minYaw = -45f; // Adjust the minimum yaw angle as needed
        float maxYaw = 45f; // Adjust the maximum yaw angle as needed

        // Clamp the yaw rotation
        newYaw = Mathf.Clamp(newYaw, currentYaw - maxYaw, currentYaw + maxYaw);

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);
        playerHead.localRotation = Quaternion.Euler(x_rotation, newYaw, 0f);
    }

    void ResetHead()
    {
        StartCoroutine(LerpToOriginalRotation());
    }

    IEnumerator LerpToOriginalRotation()
    {
        float lerpSpeed = 7.5f; // Adjust the speed as needed
        float lerpProgress = 0f; // Track the interpolation progress
        Quaternion startRotation = playerHead.transform.rotation;

        while (lerpProgress < 1f)
        {
            lerpProgress += Time.deltaTime * lerpSpeed;
            playerHead.transform.rotation = Quaternion.Lerp(startRotation, originalRotation, lerpProgress);
            yield return null;
        }
    }
}

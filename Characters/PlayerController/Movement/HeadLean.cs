using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLean : MonoBehaviour
{
    public float leanAmount = 1.0f;
    public float leanSpeed = 5.0f; // Adjust the speed to control the smoothness
    public float maxTiltAngle = 30.0f; // Maximum tilt angle in degrees

    private Transform headTransform;
    private Vector3 initialHeadPosition;
    private Quaternion initialHeadRotation;
    private Vector3 targetHeadPosition;
    private Quaternion targetHeadRotation;
    private bool isLeaning = false;

    void Start()
    {
        // Store the initial head position and rotation
        headTransform = transform;
        initialHeadPosition = headTransform.localPosition;
        initialHeadRotation = headTransform.localRotation;
        targetHeadPosition = initialHeadPosition;
        targetHeadRotation = initialHeadRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(ManageInputs.leanRightKey))
        {
            LeanRight();
        }
        else if (Input.GetKeyDown(ManageInputs.leanLeftKey))
        {
            LeanLeft();
        }
        if (Input.GetKeyUp(ManageInputs.leanLeftKey) || Input.GetKeyUp(ManageInputs.leanRightKey))
        {
            ResetHeadPosition();
        }

        // Smoothly interpolate the head's position and rotation
        if (isLeaning)
        {
            headTransform.localPosition = Vector3.Lerp(headTransform.localPosition, targetHeadPosition, Time.deltaTime * leanSpeed);
            headTransform.localRotation = Quaternion.Lerp(headTransform.localRotation, targetHeadRotation, Time.deltaTime * leanSpeed);

            // Check if the head position is very close to the target position to stop the interpolation
            if (Vector3.Distance(headTransform.localPosition, targetHeadPosition) < 0.01f)
            {
                headTransform.localPosition = targetHeadPosition;
                headTransform.localRotation = targetHeadRotation;
                isLeaning = false;
            }
        }
    }

    void LeanRight()
    {
        Vector3 startHeadPosition = headTransform.localPosition;
        Quaternion startHeadRotation = headTransform.localRotation;

        targetHeadPosition = initialHeadPosition + Vector3.right * leanAmount;
        targetHeadRotation = Quaternion.Euler(0, 0, -maxTiltAngle); // Tilt right

        isLeaning = true;
    }

    void LeanLeft()
    {
        Vector3 startHeadPosition = headTransform.localPosition;
        Quaternion startHeadRotation = headTransform.localRotation;

        targetHeadPosition = initialHeadPosition + Vector3.left * leanAmount;
        targetHeadRotation = Quaternion.Euler(0, 0, maxTiltAngle); // Tilt left

        isLeaning = true;
    }

    void ResetHeadPosition()
    {
        Vector3 startHeadPosition = headTransform.localPosition;
        Quaternion startHeadRotation = headTransform.localRotation;

        targetHeadPosition = initialHeadPosition;
        targetHeadRotation = initialHeadRotation;

        isLeaning = true;
    }
}

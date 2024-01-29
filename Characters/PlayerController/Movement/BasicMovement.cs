using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [Header("Basic Movement Settings")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;

    private bool isSprinting = false;
    private float moveSpeed;
    private float horizontal;
    private float vertical;
    private Vector3 deltaPosition;
    private Rigidbody playerBody;
    public bool isMovementEnabled = true;

    [Header("Variable Speed Settings")]
    [SerializeField] private float variableSpeedMultiplier = 1f;

    [Header("Sprint Settings")]
    [SerializeField] private float sprintFOVMultiplier = 1.0f;
    [SerializeField] private float fovChangeSpeed = 5f;
    private float originalFOV;
    private float targetFOV;

    [Header("Headbob Settings")]
    [SerializeField] private float headbobFrequency = 1.5f;
    [SerializeField] private float headbobAmplitude = 0.2f;
    [SerializeField] private float sprintHeadbobMultiplier = 2f;
    private Vector3 originalCameraPosition;
    private float headbobTimer = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerBody = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;
        originalCameraPosition = Camera.main.transform.localPosition;
        originalFOV = Camera.main.fieldOfView;
        targetFOV = originalFOV;
    }

    private void Update()
    {
        GetInput();
        Sprint();
        VariableSpeed();

        // Headbob or Not
        if (Input.GetKey(ManageInputs.moveForwardKey) || Input.GetKey(ManageInputs.moveBackwardKey) || Input.GetKey(ManageInputs.moveLeftKey) || Input.GetKey(ManageInputs.moveRightKey))
        {
            CalculateHeadbob();
        }
        else
        {
            ResetCameraPosition();
        }
    }

    private void FixedUpdate()
    {
        if (isMovementEnabled)
        {
            CalculateMovement();
        }
    }

    private void GetInput()
    {
        horizontal = Input.GetKey(ManageInputs.moveLeftKey) ? -1f : Input.GetKey(ManageInputs.moveRightKey) ? 1f : 0f;
        vertical = Input.GetKey(ManageInputs.moveBackwardKey) ? -1f : Input.GetKey(ManageInputs.moveForwardKey) ? 1f : 0f;
    }

    private void CalculateMovement()
    {
        deltaPosition = ((transform.forward * vertical) + (transform.right * horizontal)) * moveSpeed * variableSpeedMultiplier * Time.fixedDeltaTime;
        playerBody.MovePosition(playerBody.position + deltaPosition);
    }

    private void Sprint()
    {
        if (Input.GetKey(ManageInputs.sprintKey))
        {
            isSprinting = true;
            moveSpeed = runSpeed;
            targetFOV = originalFOV * sprintFOVMultiplier;
        }
        else
        {
            isSprinting = false;
            moveSpeed = walkSpeed;
            targetFOV = originalFOV;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);  // TODO: Weird interplay with Zoom sometimes but seems alright now
    }

    private void VariableSpeed()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && Input.GetKey(ManageInputs.speedAdjustmentKey))
        {
            variableSpeedMultiplier++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && Input.GetKey(ManageInputs.speedAdjustmentKey))
        {
            variableSpeedMultiplier--;
        }
    }

    private void CalculateHeadbob()
    {
        float verticalBob = Mathf.Sin(headbobTimer) * headbobAmplitude;

        float currentHeadbobAmplitude = isSprinting ? headbobAmplitude * sprintHeadbobMultiplier : headbobAmplitude;
        Camera.main.transform.localPosition = originalCameraPosition + new Vector3(0f, verticalBob * currentHeadbobAmplitude, 0f);

        headbobTimer += Time.deltaTime * headbobFrequency;

        if (headbobTimer > Mathf.PI * 2)
        {
            headbobTimer -= Mathf.PI * 2;
        }
    }

    private void ResetCameraPosition()
    {
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, originalCameraPosition, Time.deltaTime * 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    [Header("Sway and Recoil Settings")]
    [SerializeField] private float swayAmount;
    [SerializeField] private float maxSwayAmount;
    [SerializeField] private float smoothAmount;
    [SerializeField] private float bounceAmount;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private float strafeAmount = 0.025f;

    [Header("Idle Bobbing Settings")]
    [SerializeField] private float idleBobSpeed = 0.1f;
    [SerializeField] private float idleBobAmount = 0.01f;

    private bool canSway;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float initialYPosition;
    private Vector3 idleBobPosition;

    void Start()
    {
        canSway = true;
        initialPosition = transform.localPosition;
        initialYPosition = initialPosition.y;
        idleBobPosition = initialPosition;
    }

    void Update()
    {
        Sway();
    }

    void Sway()
    {
        if (canSway)
        {
            float movementX = -Input.GetAxis("Mouse X") * swayAmount;
            float movementY = -Input.GetAxis("Mouse Y") * swayAmount;

            movementX = Mathf.Clamp(-movementX, -maxSwayAmount, maxSwayAmount);
            movementY = Mathf.Clamp(-movementY, -maxSwayAmount, maxSwayAmount);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0);

            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
        }
    }

}

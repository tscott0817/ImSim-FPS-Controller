using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
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
        ApplyIdleBob(); 
    }

    void Sway()
    {
        if (canSway)
        {
            float movementX = -Input.GetAxis("Mouse X") * swayAmount;
            float movementY = -Input.GetAxis("Mouse Y") * swayAmount;

            movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
            movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
                finalPosition.y += bounce;

                if (Input.GetKey(KeyCode.A))
                {
                    finalPosition.x += strafeAmount * 0.5f;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    finalPosition.x -= strafeAmount * 0.5f;
                }
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
        }
    }

    void ApplyIdleBob()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            // When the player is idle, apply bobbing effect
            float idleBob = Mathf.Sin(Time.time * idleBobSpeed) * idleBobAmount;
            idleBobPosition.y = initialYPosition + idleBob;
            transform.localPosition = Vector3.Lerp(transform.localPosition, idleBobPosition, Time.deltaTime * smoothAmount);
        }
    }
}

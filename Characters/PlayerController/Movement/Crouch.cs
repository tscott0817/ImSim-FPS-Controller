using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchSlideForce;
    [SerializeField] private float crouchSlideDuration;

    private CapsuleCollider controller;
    private bool canCrouch = false;
    private float originalHeight;
    private bool isSliding = false;

    private void Start()
    {
        controller = GetComponent<CapsuleCollider>();
        originalHeight = controller.height;
    }

    private void Update()
    {
        CrouchPlayer();
    }

    private void CheckCrouch()
    {
        if (canCrouch)
        {
            controller.height = crouchHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z);  // TODO: The 0.7f is relative to the c
        }
        else
        {
            controller.height = originalHeight;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
        }
    }

    private void CrouchPlayer()
    {
        bool shouldCrouchSlide = Input.GetKey(ManageInputs.moveForwardKey) && Input.GetKey(ManageInputs.sprintKey);  // TODO: Don't do this here

        if (Input.GetKeyDown(ManageInputs.crouchKey))
        {
            if (!isSliding)
            {
                /*PlayerStateController.SetCrouching(!PlayerStateController.IsCrouching);  // TODO: THIS*/
                canCrouch = !canCrouch;
                CheckCrouch();

/*                if (canCrouch && shouldCrouchSlide)
                {
                    StartCoroutine(CrouchSlide());
                }*/
            }
        }
    }

    private IEnumerator CrouchSlide()
    {
        isSliding = true;
        Vector3 initialVelocity = GetComponent<Rigidbody>().velocity;
        Vector3 slideVelocity = transform.forward * crouchSpeed;
        float timer = 0f;

        while (timer < crouchSlideDuration)
        {
            timer += Time.deltaTime;
            GetComponent<Rigidbody>().velocity = slideVelocity;
            yield return null;
        }

        GetComponent<Rigidbody>().velocity = initialVelocity;
        isSliding = false;
    }
}

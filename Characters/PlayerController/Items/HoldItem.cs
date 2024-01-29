using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{

    public bool freezeRotation;
    public float pickupRange = 2f;
    public float throwStrength = 30f;
    public float heldDistance = 1.5f;
    public float maxRange = 5f;
    public float maxPickupMass = 5.0f;

    public string holdTag = "PhysObject";
    private KeyCode holdKey = KeyCode.Mouse0;
    private KeyCode throwKey = KeyCode.Mouse1;

    private GameObject objectHeld;
    public GameObject playerCam;

    private bool isObjectHeld;
    private bool canPressHoldKey = true;
    private bool isCooldownActive = false;


    private IEnumerator StartCooldown()
    {
        isCooldownActive = true;
        canPressHoldKey = false;

        yield return new WaitForSeconds(1.0f); // 5 seconds cooldown

        canPressHoldKey = true;
        isCooldownActive = false;
    }

    private void Start()
    {
        isObjectHeld = false;
        objectHeld = null;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(holdKey) && !Input.GetKeyDown(throwKey))
        {
            if (!isCooldownActive)
            {
                if (!isObjectHeld)
                {
                    PickupObject();
                }
                else
                {
                    HoldObject();
                }
            }
        }
        else if (isObjectHeld)
        {
            DropObject();
        }

        if (Input.GetKey(throwKey) && isObjectHeld)
        {
            isObjectHeld = false;
            objectHeld.GetComponent<Rigidbody>().useGravity = true;
            ThrowObject();
            StartCoroutine(StartCooldown());
        }



    }

    private void PickupObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(playerAim, out hit, pickupRange))
        {
            objectHeld = hit.collider.gameObject;

            if ((hit.collider.tag == holdTag || hit.collider.tag == "Enemy") &&
                objectHeld.GetComponent<Rigidbody>() && objectHeld.GetComponent<Rigidbody>().mass <= maxPickupMass)
            {
                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity = false;

                if (freezeRotation)
                {
                    objectHeld.GetComponent<Rigidbody>().freezeRotation = true;
                }
                if (!freezeRotation)
                {
                    objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
                }
            }
        }
    }

    private void HoldObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        /*Vector3 nextPos = playerCam.transform.position + playerAim.direction * heldDistance;*/
        Vector3 nextPos = playerCam.transform.position + playerAim.direction * heldDistance;
        Vector3 currPos = objectHeld.transform.position;

        objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;

        if (Vector3.Distance(objectHeld.transform.position, playerCam.transform.position) > maxRange)
        {
            DropObject();
        }
    }

    private void DropObject()
    {
        isObjectHeld = false;

        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }

    private void ThrowObject()
    {
        objectHeld.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * throwStrength);
        //objectHeld.GetComponent<Rigidbody>().AddExplosionForce(throwStrength, playerCam.transform.position, 0.0f);  // @params: strength, starting posititon, blast radius

        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }
}

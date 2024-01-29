using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenDoor : MonoBehaviour
{
    private KeyCode doorKey = KeyCode.F;
    private bool doorOpen = false;
    public float doorRotationAngle = -90.0f; // Angle to rotate the door

    public void Door(RaycastHit hit)
    {
        if (Input.GetKeyDown(doorKey))
        {
            if (doorOpen)
            {
                RotateDoor(hit.transform, -doorRotationAngle);
                doorOpen = false;
            }
            else
            {
                RotateDoor(hit.transform, doorRotationAngle);
                doorOpen = true;
            }
        }
    }

    private void RotateDoor(Transform door, float angle)
    {
        // Rotate the door around its Y-axis pivot point 
        door.Rotate(Vector3.up, angle);
    }
}
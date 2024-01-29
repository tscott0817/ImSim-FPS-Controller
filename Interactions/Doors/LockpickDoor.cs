using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickDoor : DoorBase
{
    private KeyCode lockpickKey = KeyCode.K;
    private bool isLockpicking = false;

    private KeyCode doorKey = ManageInputs.interactionKey;
    private bool doorOpen = false;
    public float doorRotationAngle = -90.0f; // Angle to rotate the door

    public LockpickDoor()
    {
        IsLocked = true; // LockpickDoor is initially locked
    }

    public override void Open()
    {
        if (!IsLocked)
        {
            RotateDoor(transform, doorRotationAngle);
            Debug.Log("LockpickDoor opened.");
        }
        else
        {
            Debug.Log("LockpickDoor is locked.");
        }
    }

    public override void Close()
    {
        RotateDoor(transform, -doorRotationAngle);
        Debug.Log("LockpickDoor closed.");
    }

    public override void Interact()
    {
        if (!isLockpicking)
        {
            if (IsLocked)
            {
                if (Input.GetKeyDown(lockpickKey))
                {
                    StartLockpicking();
                }
            }
            else
            {
                Open();
            }
        }
        else
        {
            // Implement lockpicking progress here
/*            if (*//* Check lockpicking progress condition *//*)
            {
                Unlock();
            }*/
            if (isLockpicking)
            {
                Unlock();
            }
            else if (Input.GetKeyDown(lockpickKey))
            {
                // Cancel lockpicking
                CancelLockpicking();
            }
        }
    }

    private void StartLockpicking()
    {
        isLockpicking = true;
        // Implement lockpicking initialization here
        Debug.Log("Lockpicking started.");
    }

    private void Unlock()
    {
        IsLocked = false;
        isLockpicking = false;
        // Implement unlocking logic here
        Debug.Log("LockpickDoor unlocked.");
    }

    private void CancelLockpicking()
    {
        isLockpicking = false;
        // Implement lockpicking cancellation logic here
        Debug.Log("Lockpicking canceled.");
    }

    private void RotateDoor(Transform door, float angle)
    {
        // Rotate the door around its Y-axis pivot point 
        door.Rotate(Vector3.up, angle);
    }
}

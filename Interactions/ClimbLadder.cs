using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder
{
    private bool isClimb = true;
    Collider currentLadder;
    private float initialClimbPositionY = 0.0f;

    public void Climb(GameObject playerObject)
    {
        if (Input.GetKey(ManageInputs.moveForwardKey))
        {
            ClimbUp(playerObject);
        }
        else if (Input.GetKey(ManageInputs.moveBackwardKey))
        {
            ClimbDown(playerObject);
        }
    }

    public void SetLadder(RaycastHit ladder)
    {
        currentLadder = ladder.collider;
        Debug.Log("Height: " + currentLadder.bounds.size.y);
    }

    private void ClimbUp(GameObject playerObject)
    {
        float climbSpeed = 2.0f;

        // Store the initial climb position if not already stored
        if (initialClimbPositionY == 0.0f)
        {
            initialClimbPositionY = playerObject.transform.position.y;
        }

        Vector3 climbDirection = Vector3.up;
        Vector3 movement = climbDirection * climbSpeed * Time.deltaTime;
        Vector3 newPosition = playerObject.transform.position + movement;

        // Calculate the maximum climb height based on the initial climb position
        float maxClimbHeight = initialClimbPositionY + currentLadder.bounds.size.y; 

        if (newPosition.y < maxClimbHeight)
        {
            playerObject.transform.position = newPosition;
            isClimb = true;
        }
        else
        {
            isClimb = false;
        }
    }

    private void ClimbDown(GameObject playerObject)
    {
        float climbSpeed = 2.0f; 
        float minClimbHeight = 0.0f; 

        Vector3 climbDirection = Vector3.down; 
        Vector3 movement = climbDirection * climbSpeed * Time.deltaTime;
        Vector3 newPosition = playerObject.transform.position + movement;

        if (newPosition.y > minClimbHeight)
        {
            playerObject.transform.position = newPosition;
            isClimb = true;
        }
        else
        {
            isClimb = false;
        }
    }

    public bool getIsClimbing()
    {
        return isClimb;
    }
    public void setIsClimbing(bool isClimbing)
    {
        isClimb = isClimbing;
    }

}

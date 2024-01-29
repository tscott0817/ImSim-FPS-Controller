using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitcher : MonoBehaviour
{
    private Transform parentTransform;
    private List<Transform> childObjects;
    private int currentIndex = 0;

    private void Start()
    {
        // Assuming your parent object is the script's GameObject
        parentTransform = transform;

        // Get all the child objects
        childObjects = new List<Transform>();
        foreach (Transform child in parentTransform)
        {
            childObjects.Add(child);
            child.gameObject.SetActive(false); // Deactivate all child objects initially
        }

        // Activate the first child object
        if (childObjects.Count > 0)
            childObjects[currentIndex].gameObject.SetActive(true);
    }

    private void Update()
    {
        // Check for scroll wheel input
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheelInput != 0)
        {
            // Deactivate the currently active child object
            childObjects[currentIndex].gameObject.SetActive(false);

            // Calculate the new index based on scroll input
            currentIndex += (scrollWheelInput > 0) ? 1 : -1;

            // Wrap around the index to stay within the range of child objects
            if (currentIndex < 0)
                currentIndex = childObjects.Count - 1;
            else if (currentIndex >= childObjects.Count)
                currentIndex = 0;

            // Activate the newly selected child object
            childObjects[currentIndex].gameObject.SetActive(true);
        }
    }
}

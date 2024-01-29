using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light[] targetLights;
    [SerializeField] float minRadius = 5f; 
    [SerializeField] float maxRadius = 10f;

    void Update()
    {
        // Check if the "L" key is pressed
        if (Input.GetKeyDown(ManageInputs.flashlightKey))
        {
            ToggleLights();
        }
    }

    void ToggleLights()
    {
        foreach (Light light in targetLights)
        {
            light.enabled = !light.enabled;
        }
    }
}

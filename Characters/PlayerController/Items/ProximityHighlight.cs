using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityHighlight : MonoBehaviour
{
    private List<string> tagsToCheck = ManageTags.tagsList;  // TODO: Not sure if it will be for all tags or not yet 
    public float detectionDistance = 5.0f;
    public MonoBehaviour scriptToAdd; 
    private Color outlineColor = Color.yellow;
    private float outlineWidth = 10.0f;
    private Outline.Mode outlineMode = Outline.Mode.OutlineVisible;

    private void Update()
    {
        foreach (string tagToCheck in tagsToCheck)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagToCheck);

            foreach (GameObject obj in taggedObjects)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance <= detectionDistance)
                {
                    Debug.Log("In Proximity of: " + obj.name);

                    if (obj.GetComponent(scriptToAdd.GetType()) == null)
                    {
                        MonoBehaviour newScript = obj.AddComponent(scriptToAdd.GetType()) as MonoBehaviour;

                        Outline outline = newScript as Outline;

                        if (outline != null)
                        {
                            outline.OutlineMode = outlineMode;
                            outline.outlineColor = outlineColor;
                            outline.outlineWidth = outlineWidth;
                        
                        }

                    }
                }
                else
                {
                    MonoBehaviour existingScript = obj.GetComponent(scriptToAdd.GetType()) as MonoBehaviour;
                    if (existingScript != null)
                    {
                        Destroy(existingScript);
                        Debug.Log("Out of Proximity of: " + obj.name);
                    }
                }
            }
        }
    }
}

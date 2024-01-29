using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdentifyItem 
{
    private string[] tags = {"Door", "PhysObject", "LightSwitch", "Ladder", "Rope", "Enemy"};


    // Text only works as param, not as global var. Can i make constructor?
    public void IDItem(RaycastHit hit, TextMeshProUGUI itemText)
    {
        foreach (string interactTag in tags)
        {
            if (hit.collider.tag == interactTag)
            {
                itemText.enabled = true;
                itemText.SetText(hit.collider.name);
            }
        }
    }

    public void RemoveText(TextMeshProUGUI itemText)
    {
        itemText.enabled = false;
        itemText.SetText("");
    }
}
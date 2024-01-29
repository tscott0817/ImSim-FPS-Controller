using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightSwitch
{
    private KeyCode switchKey = KeyCode.F;

    private bool lightOn;

    public void SwitchLight(RaycastHit hit)
    {
        CheckOn(hit); // Check if light is currently on


        if (Input.GetKeyDown(switchKey) && lightOn)
        {

            for (int i = 0; i < hit.transform.childCount; i++)
            {
                Transform currentLight = hit.transform.GetChild(i).transform.GetChild(0);
                currentLight.gameObject.SetActive(false);
            }
            lightOn = false;
        }

        else if (Input.GetKeyDown(switchKey) && !lightOn)
        {
            for (int i = 0; i < hit.transform.childCount; i++)
            {
                Transform currentLight = hit.transform.GetChild(i).transform.GetChild(0);
                currentLight.gameObject.SetActive(true);
            }

            lightOn = true;
        }


    }

    // TODO: This bases it off the the last light, but if they all share state then it is fine
    public bool CheckOn(RaycastHit hit)
    {
        for (int i = 0; i < hit.transform.childCount; i++)
        {
            Transform currentLight = hit.transform.GetChild(i).transform.GetChild(0);

            if (currentLight.gameObject.activeInHierarchy == false)
            {
                lightOn = false;
            }
            else
            {
                lightOn = true;
            }
        }
        return lightOn;
    }

}

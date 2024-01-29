using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckSurfaceType : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ManageTags.electricityTag))
        {
            Debug.Log("Player entered the Electricity");

            // Start repeating the TakeDamage function every 1 seconds
            InvokeRepeating("ApplyElectricityDamage", 0.0f, 1.0f);
        }

        if (other.CompareTag(ManageTags.poisonTag))
        {
            Debug.Log("Player entered Poison");
            InvokeRepeating("ApplyPoisonDamage", 0.0f, 1.0f);
        }

        if (other.CompareTag(ManageTags.fireTag))
        {
            Debug.Log("Player entered Fire");
            InvokeRepeating("ApplyFireDamage", 0.0f, 1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ManageTags.electricityTag))
        {
            Debug.Log("Player exited the Electricity");
            CancelInvoke("ApplyElectricityDamage");
        }

        if (other.CompareTag(ManageTags.poisonTag))
        {
            Debug.Log("Player exited the Poison");
            CancelInvoke("ApplyPoisonDamage");
        }

        if (other.CompareTag(ManageTags.fireTag))
        {
            Debug.Log("Player exited the Fire");
            CancelInvoke("ApplyFireDamage");
        }
    }

    private void ApplyElectricityDamage()
    {
        gameObject.GetComponent<PlayerHealth>().TakeDamage(DamageType.Electricity, 25);  // TODO: Need to make damage type manager
    }

    private void ApplyPoisonDamage()
    {
        gameObject.GetComponent<PlayerHealth>().TakeDamage(DamageType.Poison, 25);  // TODO: Need to make damage type manager
    }

    private void ApplyFireDamage()
    {
        gameObject.GetComponent<PlayerHealth>().TakeDamage(DamageType.Poison, 25);  // TODO: Need to make damage type manager
    }
}


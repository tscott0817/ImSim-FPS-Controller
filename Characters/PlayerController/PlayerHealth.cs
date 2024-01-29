using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamage
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageType type, int amount)
    {
        Debug.Log("Damage Taken -> Amount: " + currentHealth);
        switch (type)
        {
            case DamageType.Blunt:
                // Apply damage for blunt type
                currentHealth -= amount;
                break;
            case DamageType.Electricity:
                // Apply damage for electricity type
                // You can add specific effects or logic here
                currentHealth -= amount;
                break;
            case DamageType.Fire:

                currentHealth -= amount;
                break;

            case DamageType.Poison:

                currentHealth -= amount;
                break;

        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    private void Die()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;  // TODO: This will be going into dead state
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour, IDamage
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageType type, int amount)
    {
        Debug.Log("Damage Taken");
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
        gameObject.GetComponent<Animator>().enabled = false;

/*        float upwardForce = 1000.0f;
        Vector3 force = new Vector3(upwardForce, upwardForce, 0);

        gameObject.GetComponentInChildren<Rigidbody>().AddForce(force);*/
    }
}

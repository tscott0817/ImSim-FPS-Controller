using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int damageAmount = 10;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*gameObject.GetComponentInParent<Animator>().enabled = false;*/

        // If not colliding with itself
        if (collision.gameObject.tag != "Enemy")
        {
            gameObject.GetComponentInParent<NPCHealth>().TakeDamage(DamageType.Blunt, damageAmount);

            // float upwardForce = 100.0f;
            // gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }
}

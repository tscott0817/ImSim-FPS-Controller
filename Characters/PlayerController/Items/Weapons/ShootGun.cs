using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ShootGun : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int damageAmount = 10;  // TODO: Make this relative to gun type / perks
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private float hitForce = 100f;
    [SerializeField] private float recoilUpDistance = 0.1f; 
    [SerializeField] private float recoilBackDistance = 0.05f;
    [SerializeField] private float recoilDuration = 0.1f; 
    [SerializeField] private float resetDuration = 0.2f; 
    [SerializeField] private float rotationDistance = 10.0f;
    [SerializeField] private GameObject bulletImpactPrefab; 
    [SerializeField] private AudioSource gunshotAudioSource; 

    private bool isFiring = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Coroutine recoilCoroutine;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        // Just doing single fire for now
        if (Input.GetMouseButtonDown(0))
        {
            PlayGunshotSound();
            Shoot();
            ApplyRecoil();
        }
    }

    private void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        Ray ray = mainCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Rigidbody hitRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                Vector3 forceDirection = (hit.point - transform.position).normalized;
                hitRigidbody.AddForce(forceDirection * hitForce);
            }

            CreateBulletImpact(hit.collider.tag, hit.point, hit.normal);

            if (hit.collider.tag == "Enemy")
            {
                hit.transform.GetComponentInParent<NPCHealth>().TakeDamage(DamageType.Blunt, damageAmount);
            }
        }
    }

    private void ApplyRecoil()
    {
        if (recoilCoroutine != null)
        {
            StopCoroutine(recoilCoroutine);
        }

        recoilCoroutine = StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
        Vector3 recoilPosition = originalPosition + Vector3.up * recoilUpDistance - Vector3.forward * recoilBackDistance;
        /*Quaternion recoilRotation = Quaternion.Euler(Vector3.up * 25f); // Adjust the rotation amount as needed*/
        /*Quaternion recoilRotation = Quaternion.Euler(-rotationDistance, 0f, 0f);*/

        // Apply recoil
        float elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime / recoilDuration);
            /*transform.localRotation = Quaternion.Slerp(originalRotation, recoilRotation, elapsedTime / recoilDuration);*/
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Smoothly reset the gun position and rotation
        elapsedTime = 0f;
        while (elapsedTime < resetDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime / resetDuration);
            /*transform.localRotation = Quaternion.Slerp(recoilRotation, originalRotation, elapsedTime / resetDuration);*/
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;

        recoilCoroutine = null;
    }

    private void CreateBulletImpact(string tag, Vector3 position, Vector3 normal)
    {

        // TODO: Load bulletImpactPrefab here based on tag
        if (bulletImpactPrefab != null)
        {
            GameObject bulletImpactInstance = Instantiate(bulletImpactPrefab, position, Quaternion.LookRotation(normal));

            Destroy(bulletImpactInstance, bulletImpactInstance.GetComponent<ParticleSystem>().main.duration);
        }
    }

    private void PlayGunshotSound()
    {
        if (gunshotAudioSource != null)
        {
            gunshotAudioSource.Play();
        }
    }
}
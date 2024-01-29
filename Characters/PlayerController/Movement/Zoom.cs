using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zoom : MonoBehaviour
{
    private float targetFov;
    private float originalFov;
    private bool isZoomed = false;

    [SerializeField] Camera mainCam;
    /*[SerializeField] Camera weaponCam;*/
    [SerializeField] float zoomSpeed = 5f;

    void Start()
    {
        originalFov = mainCam.fieldOfView;
        targetFov = originalFov;
    }

    void Update()
    {
        if (Input.GetKey(ManageInputs.zoomKey))
        {
            isZoomed = true;
            targetFov = 25f;
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);
            /*weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, targetFov * 2.0f, Time.deltaTime * zoomSpeed);*/
        }
        else
        {
            isZoomed = false;
            targetFov = originalFov;
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);
            /*weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);*/
        }
    }
}
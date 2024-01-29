using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour
{
    // Picking up Item
    public GameObject shootPoint;
    private float grappleRange = 20f;
    private GameObject objectPickedUp;

    // Testing
    private string grappleTag = "GrappleObject";

    public List<GameObject> objectList;
    public KeyCode selectKey = KeyCode.L;
    private RaycastHit hit;

    // Item Name
    public TextMeshProUGUI itemText;

    private void Awake()
    {
        objectPickedUp = new GameObject();
        objectList = new List<GameObject>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        itemText.enabled = false;

    }

    private void Update()
    {

        ShootGrapple();

    }

    private void ShootGrapple()
    {

        if (Physics.Raycast(shootPoint.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit, grappleRange))
        {

            if (hit.collider.tag == grappleTag)
            {
                itemText.SetText(hit.collider.name);
                itemText.enabled = true;
            }


            Debug.Log(hit.collider.name);

/*            if ((hit.collider.tag == pickupTagStatic || hit.collider.tag == pickupTagPhys) && Input.GetKeyDown(selectKey))
            {

                objectList.Add(hit.transform.gameObject); // Adds gameObject to list
                Destroy(hit.collider.gameObject.GetComponent<MeshRenderer>());
                Destroy(hit.collider.gameObject.GetComponent<Collider>());

            }*/

        }

    }

}

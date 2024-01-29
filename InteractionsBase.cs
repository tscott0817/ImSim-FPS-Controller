using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionsBase : MonoBehaviour
{

    // Raycast params
    public GameObject playerCam;
    private float range = 2.0f;  // Same as pickup range from hold item script
    private RaycastHit hit;
    private GameObject currentPlayer;
    

    // Interactive Objects
    IdentifyItem identifyItem;
    OpenDoor doors;
    LightSwitch lights;
    ClimbLadder ladder;
    RopeSwing rope;
    [SerializeField] GameObject flashlight;

    // Tags
    private string doorTag = "Door";
    private string lightTag = "LightSwitch";
    private string ladderTag = "Ladder";
    private string ropeTag = "Rope";

    // GUI
    public TextMeshProUGUI itemName;

    // TODO: Extra Stuff To Work Out
    bool onLadder = false;
    bool onRope = false;

    private void Awake()
    {
        itemName.SetText("");
        itemName.enabled = false;
        currentPlayer = this.gameObject;
    }
    private void Start()
    {
        // Initialize Interactive Objects
        identifyItem = new IdentifyItem();
        doors = new OpenDoor();
        lights = new LightSwitch();
        ladder = new ClimbLadder();
        rope = new RopeSwing();
    }


    // TODO: Some of the code uses Physics, so want to separate in to Update and FixedUpdate
    private void Update()
    {
        CheckRaycast();
        ExecuteInteractions();
    }


    private void ExecuteInteractions()
    {
        // Stuff to happen outside of main raycast
        if (onLadder)
        {
            currentPlayer.GetComponent<BasicMovement>().enabled = false;
            currentPlayer.GetComponent<Rigidbody>().useGravity = false;
            ladder.Climb(currentPlayer);

            if (Input.GetKeyDown(ManageInputs.jumpKey))
            {
                onLadder = false;
            }
            if (ladder.getIsClimbing() == false)
            {
                onLadder = false;
                ladder.setIsClimbing(true);  // TODO: This works, but I don't like using setters/getters
            }
        }

        // Rope Stuff
        else if (onRope)
        {
            /*Physics.IgnoreCollision(currentPlayer.GetComponent<Collider>(), rope.GetRope().GetComponent<Collider>());  // TODO: MESSSSYYYY*/
            currentPlayer.GetComponent<BasicMovement>().enabled = false;
            currentPlayer.GetComponent<Rigidbody>().useGravity = false;
            rope.HookRope(hit, currentPlayer);
            rope.Swing(currentPlayer);

            if (Input.GetKeyDown(ManageInputs.jumpKey))
            {
                onRope = false;
            }

            // This needs to come last, a bit hacky
            Physics.IgnoreCollision(currentPlayer.GetComponent<Collider>(), rope.GetRope().GetComponent<Collider>());
        }
        else
        {
            currentPlayer.GetComponent<BasicMovement>().enabled = true;
            currentPlayer.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void CheckRaycast()
    {

        Ray ray = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out hit, range))
        {
            identifyItem.IDItem(hit, itemName);

            if (Input.GetKeyDown(ManageInputs.interactionKey))
            {
                if (hit.collider.tag == doorTag)
                {
                    doors.Door(hit);
                }
                else if (hit.collider.tag == lightTag)
                {
                    lights.SwitchLight(hit);
                }
                else if (hit.collider.tag == ladderTag)
                {
                    onLadder = true;
                    ladder.SetLadder(hit);

                }
                else if (hit.collider.tag == ropeTag)
                {
                    onRope = true;
                    
                }
            }         
        }
        else
        {
            identifyItem.RemoveText(itemName);  // Remove item name when not looking at object
        }
    }
}

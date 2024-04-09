using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    public bool doorClosed = true;
    private bool canTrigger;
    private bool isTouchingDoor;
    private bool isTouchingLockedDoor;
    private bool isTouchingSafe;
    private bool isTouchingOwnerDoor;
    public float doorSpeed = 60f;
    public float doorTime = 1.6f;

    [SerializeField] private TMP_Text pressEText;
    [SerializeField] private TMP_Text lockedDoorText;

    public Interactable Interactable;
    [SerializeField] SafePuzzle safePuzzle;
    [SerializeField] Keypad keypad;

    public Map Map;

    private void Start()
    {
        pressEText.enabled = false;
        lockedDoorText.enabled = false;
        if (GameManager.meetingScene || GameManager.chaseScene)
        {
            StartCoroutine(OnInteract());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        //pressEText.GetComponent<TextMeshProUGUI>().text = "Press E to Interact";
        if (other.CompareTag("Player"))
        {
            canTrigger = true;
            if (gameObject.name != "LockedDoor" && gameObject.name != "SafeDoor" && gameObject.name != "OwnerDoor")
            {
                pressEText.enabled = true;
                isTouchingDoor = true;
            }
            if (gameObject.name == "LockedDoor")
            {
                pressEText.enabled = false;
                isTouchingLockedDoor = true;
            }
            if (gameObject.name == "SafeDoor")
            {
                isTouchingDoor = false;
                isTouchingSafe = true;
            }
            if (gameObject.name == "OwnerDoor")
            {
                isTouchingDoor = false;
                isTouchingOwnerDoor = true;
            }
        }

        if (other.CompareTag("Tom"))
        {
            StartCoroutine(OnInteract());
        }


    }
    private void OnTriggerExit(Collider other)
    {
        pressEText.enabled = false;
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            isTouchingDoor = false;
            isTouchingLockedDoor = false;
            isTouchingSafe = false;
            isTouchingOwnerDoor = false;
            Interactable.tryingToUseLuggageKey = false;
            Interactable.tryingToUseOwnerKey = false;
        }
    }

    public void Interaction()
    {
        pressEText.enabled = false;
        Debug.Log("Door Interaction!");
        StartCoroutine(OnInteract());
    }

    public IEnumerator OnInteract()
    {
        float rotfactor;

        if (gameObject.name == "DoorR" || gameObject.name == "SafeDoor")
        {
            rotfactor = doorClosed ? -1f : 1f;
        }
        else 
        { 
            rotfactor = doorClosed ? 1f : -1f; 
        }

        canTrigger = false;
        Transform doorHing = transform.parent.Find("Hinge");

        //rotate the door
        for (float t = 0f; t < doorTime; t += Time.deltaTime)
        {
            transform.RotateAround(doorHing.position, doorHing.up, doorSpeed * rotfactor * Time.deltaTime);

            yield return null;
        }

        //rotate done:
        doorClosed = !doorClosed;
        if (doorClosed)
        {
            transform.localRotation = Quaternion.identity; //identity is base start
        }
        canTrigger = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTouchingDoor && canTrigger)
        {
            Interaction();
        }
        if (Input.GetKeyDown(KeyCode.E) && isTouchingLockedDoor)
        {
            lockedDoorText.enabled = true;
            StartCoroutine(WaitForText());
        }
        if (Input.GetKeyDown(KeyCode.E) && isTouchingSafe)
        {
            if (keypad.codeSolved && safePuzzle.totalTime > 0)
            {
                safePuzzle.reachedSafe = true;
                Interaction();
            }
            else
            {
                lockedDoorText.enabled = true;
                StartCoroutine(WaitForText());
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && isTouchingOwnerDoor)
        {
            lockedDoorText.enabled = true;
            StartCoroutine(WaitForText());
        }
        if (canTrigger && isTouchingOwnerDoor && Interactable.tryingToUseOwnerKey)
        {
            Interaction();
            gameObject.name = "Door";
            isTouchingOwnerDoor = false;
            Interactable.tryingToUseOwnerKey = false;
           
        }

        if (canTrigger && isTouchingLockedDoor && Interactable.tryingToUseLuggageKey) 
        {
            
            Interaction();
            gameObject.name = "Door";
            isTouchingLockedDoor = false;
            Interactable.tryingToUseLuggageKey = false;
            Map.hintText.text = "There may be important information within the unlocked guest room.";
        }
    }

    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(.4f);
        lockedDoorText.enabled = false;
    }
}

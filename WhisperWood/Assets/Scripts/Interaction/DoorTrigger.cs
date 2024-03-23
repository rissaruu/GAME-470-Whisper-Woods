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
    public float doorSpeed = 60f;
    public float doorTime = 1.6f;

    [SerializeField] private TMP_Text pressEText;
    [SerializeField] private TMP_Text lockedDoorText;

    public Interactable Interactable;

    private void Start()
    {
        pressEText.enabled = false;
        lockedDoorText.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        //pressEText.GetComponent<TextMeshProUGUI>().text = "Press E to Interact";
        if (other.CompareTag("Player"))
        {
            canTrigger = true;
            if (gameObject.name != "LockedDoor")
            {
                pressEText.enabled = true;
                isTouchingDoor = true;
            }
            if (gameObject.name == "LockedDoor")
            {
                pressEText.enabled = false;
                isTouchingLockedDoor = true;
            }
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
        float rotfactor = doorClosed ? 1f : -1f;
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

        if (canTrigger && isTouchingLockedDoor && Interactable.tryingToUseTomKey) //needs another condition (the use button)
        {
            Interaction();
            gameObject.name = "Door";
            isTouchingLockedDoor = false;
            Interactable.tryingToUseTomKey = false;
        }
        if (!canTrigger && Interactable.tryingToUseTomKey)
        {
            Interactable.tryingToUsePaintingPiece = false;
        }
    }

    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(.4f);
        lockedDoorText.enabled = false;
    }
}

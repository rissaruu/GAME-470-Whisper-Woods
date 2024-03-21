using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool doorClosed = true;
    private bool canTrigger;
    private bool isTouchingDoor;
    public float doorSpeed = 60f;
    public float doorTime = 1.6f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            isTouchingDoor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            isTouchingDoor = false;
        }
    }

    public void Interaction()
    {
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
    }
}

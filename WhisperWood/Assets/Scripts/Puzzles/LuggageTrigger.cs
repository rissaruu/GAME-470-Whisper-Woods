using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageTrigger : MonoBehaviour
{

    private bool canTrigger;
    private bool isTouchingLuggage;

    public CombinationLock CombinationLock;

    [SerializeField] private GameObject LockUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            isTouchingLuggage = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            isTouchingLuggage = false;

        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && LockUI.activeInHierarchy && isTouchingLuggage)
        {
            CombinationLock.CloseLuggageUI();
            canTrigger = false;
            //GameManager.canCamera = true;
        }

        if (canTrigger && Input.GetKeyDown(KeyCode.E))
        {
            CombinationLock.OpenLuggageUI();
            canTrigger = false;
            //GameManager.canCamera = false;

        }


    }
}

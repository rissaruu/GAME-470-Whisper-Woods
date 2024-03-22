using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LuggageTrigger : MonoBehaviour
{

    private bool canTrigger;
    private bool isTouchingLuggage;

    public CombinationLock CombinationLock;

    [SerializeField] private GameObject LockUI;
    [SerializeField] private TMP_Text pressEText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            isTouchingLuggage = true;
            if (!CombinationLock.solvedLuggageCombination)
            {
                pressEText.enabled = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            isTouchingLuggage = false;
            pressEText.enabled = false;

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
            pressEText.enabled = false;
            //GameManager.canCamera = false;

        }


    }
}

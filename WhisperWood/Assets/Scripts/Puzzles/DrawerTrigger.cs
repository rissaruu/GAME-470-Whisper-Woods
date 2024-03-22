using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrawerTrigger : MonoBehaviour
{
    private bool canTrigger;
    private bool isTouchingDrawer;

    public CombinationLock CombinationLock;

    [SerializeField] private GameObject LockUI;
    [SerializeField] private TMP_Text pressEText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            isTouchingDrawer = true;

            if (!CombinationLock.solvedDrawerCombination)
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
            isTouchingDrawer = false;
            pressEText.enabled = false;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && LockUI.activeInHierarchy && isTouchingDrawer)
        {
            CombinationLock.CloseDrawerUI();
            canTrigger = false;
            //GameManager.canCamera = true;
        }

        if (canTrigger && Input.GetKeyDown(KeyCode.E))
        {
            CombinationLock.OpenDrawerUI();
            canTrigger = false;
            pressEText.enabled = false;
            //GameManager.canCamera = false;

        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerTrigger : MonoBehaviour
{
    private bool canTrigger;
    private bool isTouchingDrawer;

    public CombinationLock CombinationLock;

    [SerializeField] private GameObject LockUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            isTouchingDrawer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            isTouchingDrawer = false;
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
            //GameManager.canCamera = false;

        }


    }
}

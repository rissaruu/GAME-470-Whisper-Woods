using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OwnerDrawerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject movingDrawer;
    [SerializeField] private GameObject receptionList;
    private bool canTrigger;
    private bool isTouchingDrawer;
   

    [SerializeField] private TMP_Text pressEText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            isTouchingDrawer = true;
            pressEText.enabled = true;
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



        if (canTrigger && Input.GetKeyDown(KeyCode.E))
        {
            canTrigger = false;
            pressEText.enabled = false;
            Vector3 movement = Vector3.right * 1f;

            movingDrawer.transform.Translate(movement);
           
            receptionList.SetActive(true);

        }


    }
}
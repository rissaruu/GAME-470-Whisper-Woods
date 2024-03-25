using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StairsTrigger : MonoBehaviour
{
    private bool canTrigger;
    [SerializeField] GameObject player;
    private Vector3 playerPosition;
    [SerializeField] private TMP_Text pressEText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = true;
            pressEText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            pressEText.enabled = false;
        }
    }

    private void Update()
    {
        if (canTrigger && Input.GetKeyDown(KeyCode.E))
        {
            //Logic to move player
            if (gameObject.name == "DownStairsTrigger") 
            {
                float newYValue = player.transform.position.y + 4;
                float newZValue = player.transform.position.z - 1;
                playerPosition = new Vector3(player.transform.position.x, newYValue, newZValue);
                player.transform.position = playerPosition;
                //playerTransform = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
            }
            else if (gameObject.name == "UpStairsTrigger") 
            {
                float newYValue = player.transform.position.y - 4;
                float newZValue = player.transform.position.z - 1;
                playerPosition = new Vector3(player.transform.position.x, newYValue, newZValue);
                player.transform.position = playerPosition;
                //playerTransform = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
            }
        }
    }
}

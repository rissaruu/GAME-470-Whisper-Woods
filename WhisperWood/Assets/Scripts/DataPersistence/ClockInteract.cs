using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockInteract : MonoBehaviour
{
    public DataPersistenceManager dataManager;
    private bool canTrigger;
    [SerializeField] private TMP_Text pressEText;

    // Start is called before the first frame update
    void Start()
    {
        pressEText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTrigger && Input.GetKeyDown(KeyCode.E))
        {
            dataManager.SaveGame();
            Debug.Log("Game saved succesfully!"); //replace this with a ui prompt or something to let player know realistically
        }

    }

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
            pressEText.enabled = false;
            canTrigger = false;
        }
    }

}

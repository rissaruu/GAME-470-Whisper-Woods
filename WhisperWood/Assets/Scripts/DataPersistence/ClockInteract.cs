using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInteract : MonoBehaviour
{
    public DataPersistenceManager dataManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dataManager.SaveGame();
            Debug.Log("Game saved succesfully!"); //replace this with a ui prompt or something to let player know
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPosition; 
    public Quaternion playerRotation;
    public InventoryData inventoryData;
    public bool mapActive;
    public bool guestRoomHighlightActive;
    public bool lobbyHighlightActive;
    public bool hallwayHighlightActive;
    public bool employeeLoungeHighlightActive;
    public bool diningRoomHighlightActive;
    public bool unnamedRoomHighlightActive;
    public bool ownerOfficeHighlightActive;
    public string hintText;
    public float progressSliderValue;
    public bool[] dialogueTriggers;
    public int foundEvidence;
    public Vector3 tomLocation;
    public bool meetingScene;
    public bool chaseScene;


    public GameData()
    {
        playerPosition = Vector3.zero;
        playerRotation = Quaternion.identity;
        inventoryData = new InventoryData();
        mapActive = false;
        guestRoomHighlightActive = false;
        lobbyHighlightActive = true;
        hallwayHighlightActive = false;
        employeeLoungeHighlightActive = false;
        diningRoomHighlightActive = false;
        unnamedRoomHighlightActive = false;
        ownerOfficeHighlightActive = false;
        hintText = "Find out what's inside the luggage.";
        progressSliderValue = 0f;

    }


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPosition; 
    public Quaternion playerRotation;
    public Vector3 tomPosition;
    public Quaternion tomRotation;

    // Order anything below here as not working until confirmed functional
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
  
    public bool meetingScene;
    public bool chaseScene;
    public bool addedCombinationImage;
    public bool addedTomKey;
    public bool addedPaintingPiece;
    public bool addedPlayingCard;
    public bool addedScroll;
    public bool addedDroranAd;
    public bool addedOwnerKey;
    //public bool addedWallet;
    //public bool addedList;

    public bool solvedLuggageCombination;
    public bool solvedDrawerCombination;

    public GameData()
    {
        playerPosition = Vector3.zero;
        playerRotation = Quaternion.identity;
        tomPosition = Vector3.zero;
        tomRotation = Quaternion.identity;

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



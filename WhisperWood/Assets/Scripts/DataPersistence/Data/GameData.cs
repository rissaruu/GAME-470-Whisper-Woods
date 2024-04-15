using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //PlayerMovement
    public Vector3 playerPosition; 
    public Quaternion playerRotation;

    // Order anything below here as not working until confirmed functional

    //TomMovement
    public Vector3 tomPosition;
    public Quaternion tomRotation;

    //ItemIndex
    public InventoryData inventoryData;


    //Map
    public bool mapActive;
    public bool guestRoomHighlightActive;
    public bool lobbyHighlightActive;
    public bool hallwayHighlightActive;
    public bool employeeLoungeHighlightActive;
    public bool diningRoomHighlightActive;
    public bool unnamedRoomHighlightActive;
    public bool ownerOfficeHighlightActive;
    public string hintText;

    //ProgessBar
    public float progressSliderValue;

    //DialogueManager
    public bool[] dialogueTriggers;

    //GameManager
    public int foundEvidence;
    public bool meetingScene;
    public bool chaseScene;

    //
    public bool addedCombinationImage;
    public bool addedTomKey;
    public bool addedPaintingPiece;
    public bool addedPlayingCard;
    public bool addedScroll;
    public bool addedDroranAd;
    public bool addedOwnerKey;
    public bool addedWallet;
    public bool addedList;

    //CombinationLock
    public bool solvedLuggageCombination;
    public bool solvedDrawerCombination;

    public GameData()
    {
        playerPosition = new Vector3(1.835f, 1.13f, 2.6f);
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



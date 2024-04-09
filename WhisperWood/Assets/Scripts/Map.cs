using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject map;
    public GameObject guestRoomHighlight;
    public GameObject lobbyHighlight;
    public GameObject hallwayHighlight;
    public GameObject employeeLoungeHighlight;
    public GameObject diningRoomHighlight;
    public GameObject unnamedRoomHighlight;
    public GameObject ownerOfficeHighlight;
    public TMP_Text hintText;

    // Start is called before the first frame update
    void Start()
    {
        map.SetActive(false);
        guestRoomHighlight.SetActive(false);
        lobbyHighlight.SetActive(true);
        hallwayHighlight.SetActive(false);
        employeeLoungeHighlight.SetActive(false);
        diningRoomHighlight.SetActive(false);
        unnamedRoomHighlight.SetActive(false);
        ownerOfficeHighlight.SetActive(false);

        hintText.text = "Find out what's inside the luggage.";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!map.activeInHierarchy)
            {
                map.SetActive(true);
                GameManager.DisablePlayer();
            }
            else
            {
                map.SetActive(false);
                GameManager.EnablePlayer();
            }
        }

    }

    public void DeletePreviousHighlights()
    {
        guestRoomHighlight.SetActive(false);
        lobbyHighlight.SetActive(false);
        hallwayHighlight.SetActive(false);
        employeeLoungeHighlight.SetActive(false);
        diningRoomHighlight.SetActive(false);
        unnamedRoomHighlight.SetActive(false);
        ownerOfficeHighlight.SetActive(false);
    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
            gameData.mapActive = map.activeSelf;
            gameData.guestRoomHighlightActive = guestRoomHighlight.activeSelf;
            gameData.lobbyHighlightActive = lobbyHighlight.activeSelf;
            gameData.hallwayHighlightActive = hallwayHighlight.activeSelf;
            gameData.employeeLoungeHighlightActive = employeeLoungeHighlight.activeSelf;
            gameData.diningRoomHighlightActive = diningRoomHighlight.activeSelf;
            gameData.unnamedRoomHighlightActive = unnamedRoomHighlight.activeSelf;
            gameData.ownerOfficeHighlightActive = ownerOfficeHighlight.activeSelf;
            gameData.hintText = hintText.text;
        }
    }

    // Load the state of the map from game data
    public void LoadData(GameData gameData)
    {
        if (gameData != null)
        {
            map.SetActive(gameData.mapActive);
            guestRoomHighlight.SetActive(gameData.guestRoomHighlightActive);
            lobbyHighlight.SetActive(gameData.lobbyHighlightActive);
            hallwayHighlight.SetActive(gameData.hallwayHighlightActive);
            employeeLoungeHighlight.SetActive(gameData.employeeLoungeHighlightActive);
            diningRoomHighlight.SetActive(gameData.diningRoomHighlightActive);
            unnamedRoomHighlight.SetActive(gameData.unnamedRoomHighlightActive);
            ownerOfficeHighlight.SetActive(gameData.ownerOfficeHighlightActive);
            hintText.text = gameData.hintText;
        }
    }


}

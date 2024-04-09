using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
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
}

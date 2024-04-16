using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemIndex : MonoBehaviour, IDataPersistence
{
    public static bool[] keyItems; // Array to store whether each key item is found
    public Dictionary<string, int> codenameToIndex; // Dictionary to map codenames to indices


    public Dictionary<string, int> inventoryItems;
    public int itemIndex;

    public Map Map;


    // Singleton pattern to ensure only one instance of the InventoryManager exists
    public static ItemIndex instance;

    private void Start()
    {
        inventoryItems = new Dictionary<string, int>();
        itemIndex = 0;
    }

    public void AddItemToInventory(string itemName)
    {
        inventoryItems.Add(itemName, itemIndex);
        itemIndex++;
        GameManager.foundEvidence += 1;


        if (itemName == "LuggageKey")
        {
            Map.DeletePreviousHighlights();
            Map.guestRoomHighlight.SetActive(true);
            Map.hintText.text = "Use key to open the room.";
        }
        if (itemName == "DroranAd")
        {
            Map.DeletePreviousHighlights();
            Map.hallwayHighlight.SetActive(true);
            Map.hintText.text = "Talk to someone who might be helpful.";
        }
        if (itemName == "Scroll")
        {
            Map.DeletePreviousHighlights();
            Map.diningRoomHighlight.SetActive(true);
        }
        if (itemName == "OwnerKey")
        {
            Map.DeletePreviousHighlights();
            Map.ownerOfficeHighlight.SetActive(true);
            Map.hintText.text = "Unlock the owner's office.";
        }

        if (itemName == "Wallet")
        {
            Map.hintText.text = "There has to be something else in here.";
        }

        if (itemName == "List")
        {
            Map.DeletePreviousHighlights();
            Map.diningRoomHighlight.SetActive(true);
            Map.hintText.text = "Close this case.";
            //GameManager.meetingScene = true;
            SceneManager.LoadScene("TransitionScene");
            //SceneManager.LoadScene("GatheredScene");
            // consider save and maybe load game here if ever gets working properly
        }

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            keyItems = new bool[GameManager.totalEvidence]; // Initialize the array size based on totalEvidence

        }
        else
        {
            Destroy(gameObject); // If another instance already exists, destroy this one
        }
    }

    // Method to add a key item to the inventory
    public void AddKeyItem(string codename)
    {
        if (codenameToIndex.ContainsKey(codename))
        {
            int index = codenameToIndex[codename];
            keyItems[index] = true;
            GameManager.foundEvidence++; // Increment foundEvidence directly from GameManager
        }
        else
        {
            Debug.LogError("Invalid codename!");
        }
    }

    // Method to check if a key item is in the inventory
    public bool HasKeyItem(string codename)
    {
        if (codenameToIndex.ContainsKey(codename))
        {
            int index = codenameToIndex[codename];
            return keyItems[index];
        }
        else
        {
            Debug.LogError("Invalid codename!");
            return false;
        }
    }

    // Method to reset the key items array
    public static void ResetKeyItems()
    {
        for (int i = 0; i < keyItems.Length; i++)
        {
            keyItems[i] = false;
        }
    }

    //To call upon KeyItems from other scripts, use this to add:

    //InventoryManager.instance.AddKeyItem("scroll");

    //And this to check:

    //if (InventoryManager.instance.HasKeyItem("Scroll"))
    //{

    //}
    //Have to be carefull with capital letters and spelling -Christian

    public void LoadData(GameData gameData)
    {
        if (gameData != null && gameData.inventoryData != null)
        {
            // Load inventory items from gameData
            foreach (var item in gameData.inventoryData.inventoryItems)
            {
                inventoryItems.Add(item.Key, item.Value);
            }

            // Load key items from gameData
            keyItems = gameData.inventoryData.keyItems.ToArray();
        }
    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
            // Save inventory items to gameData
            gameData.inventoryData.inventoryItems = new Dictionary<string, int>(inventoryItems);

            // Save key items to gameData
            gameData.inventoryData.keyItems = new List<bool>(keyItems);
        }
    }

}

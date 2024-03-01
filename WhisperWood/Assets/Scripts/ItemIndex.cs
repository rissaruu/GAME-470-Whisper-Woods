using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    public static bool[] keyItems; // Array to store whether each key item is found
    public Dictionary<string, int> codenameToIndex; // Dictionary to map codenames to indices

    // Singleton pattern to ensure only one instance of the InventoryManager exists
    public static ItemIndex instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            keyItems = new bool[GameManager.totalEvidence]; // Initialize the array size based on totalEvidence

            // Initialize the dictionary
            codenameToIndex = new Dictionary<string, int>();
            codenameToIndex.Add("TomKey", 0);
            codenameToIndex.Add("PaintingPiece", 1);
            codenameToIndex.Add("DroranAd", 2);
            codenameToIndex.Add("Scroll", 3);
            codenameToIndex.Add("PlayingCard", 4);
            codenameToIndex.Add("OwnerKey", 5);
            codenameToIndex.Add("Wallet", 6);
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
}

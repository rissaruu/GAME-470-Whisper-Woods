using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class InventoryData
{
    public Dictionary<string, int> inventoryItems;
    public List<bool> keyItems;

    public InventoryData()
    {
        inventoryItems = new Dictionary<string, int>();
        keyItems = new List<bool>();
    }
}

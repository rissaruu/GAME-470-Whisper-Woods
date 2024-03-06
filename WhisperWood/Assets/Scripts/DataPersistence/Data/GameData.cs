using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPosition; 
    public Quaternion playerRotation;
    public Dictionary<string, bool> keyItemCollected; // may change based on how item scripts are written


    public GameData()
    {
        playerPosition = Vector3.zero;
        playerRotation = Quaternion.identity;
        keyItemCollected = new Dictionary<string, bool>(); // may change based on how item scripts are written

    }


}



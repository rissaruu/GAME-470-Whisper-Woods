using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct IsPlayer 
{
    public bool walking;
    public bool running;
    public bool jumping;
    public bool vaulting;
}


public struct CanPlayer 
{
    public bool walk;
    public bool run;
    public bool jump;
    public bool rotate;
    public bool vault;
}



public class GameManager : MonoBehaviour
{
    public static bool isGamePaused = false;

    //PLAYER VARIABLES
    public const float playerWalkSpeed = 5f;
    public const float playerVaultSpeed = 10f;
    public const float rotationSpeed = 5f;
    public const float playerRunSpeed = 8f;
    public const float playerJumpForce = 5f;

    public static CanPlayer canPlayer = new CanPlayer();
    public static IsPlayer isPlayer = new IsPlayer();

    //GAME VARIABLES
    public ProgressBar progressBar;
    public GameObject chasePrompt;
    public const int totalEvidence = 9; // for each puzzle/item?
    public static int foundEvidence; // increase this value after every puzzle/item
    public static bool meetingScene = false;
    public static bool chaseScene = false;

    public PlayerMovement PlayerMovement;
    public static bool canPause = true;
    public static bool canCamera = true;

    //ITEM VARIABLES
    public static bool canUseTomKey;
    public static bool canUseScroll;

    public static void ResetVariables()
    {
        canPlayer.walk = true;
        canPlayer.run = true;
        canPlayer.jump = true;
        canPlayer.rotate = true;

        isPlayer.walking = false;
        isPlayer.running = false;
        isPlayer.jumping = false;

        
        // Keep this commented for now until applicable
        // ItemIndex.ResetKeyItems();  

        Debug.Log("Variables Reset");
    }

    public static void DisablePlayer()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        canPlayer.walk = false;
        canCamera = false;
        canPlayer.jump = false;
        canPause = false;


    }

    public static void EnablePlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canPlayer.walk = true;
        canCamera = true;
        canPlayer.jump = true;
        canPause = true;
    }


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (meetingScene || chaseScene)
        {
            foundEvidence = totalEvidence;
        }
    }

    public static void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
            gameData.foundEvidence = foundEvidence;
            gameData.meetingScene = meetingScene;
            gameData.chaseScene = chaseScene;
        }
    }

    public static void LoadData(GameData gameData)
    {
        if (gameData != null)
        {
            foundEvidence = gameData.foundEvidence;
            gameData.meetingScene = meetingScene;
            gameData.chaseScene = chaseScene;
        }
    }

}
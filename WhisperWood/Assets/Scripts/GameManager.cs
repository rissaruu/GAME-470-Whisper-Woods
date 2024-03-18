using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IsPlayer 
{
    public bool walking;
    public bool running;
    public bool jumping;
}


public struct CanPlayer 
{
    public bool walk;
    public bool run;
    public bool jump;
    public bool rotate;
}



public class GameManager : MonoBehaviour
{
    public static bool isGamePaused = false;

    //PLAYER VARIABLES
    public const float playerWalkSpeed = 5f;
    public const float rotationSpeed = 5f;
    public const float runSpeedMod = 4f;
    public const float playerJumpForce = 4f;

    public static CanPlayer canPlayer = new CanPlayer();
    public static IsPlayer isPlayer = new IsPlayer();

    //GAME VARIABLES
    public ProgressBar progressBar;
    public const int totalEvidence = 7; // for each puzzle
    public static int foundEvidence; // increase this value after every puzzle

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

        foundEvidence = 0;

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

    }

}

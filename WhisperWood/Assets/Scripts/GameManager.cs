using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IsPlayer 
{
    public bool walking;
    public bool running;
    public bool jumping;
    public bool crouching;
}


public struct CanPlayer 
{
    public bool walk;
    public bool run;
    public bool jump;
    public bool crouch;
}

public class GameManager : MonoBehaviour
{  
    public static bool isGamePaused = false;

    //PLAYER VARIABLES
    public const float playerWalkSpeed = 3f;
    public const float crouchSpeedMod = -0.5f;
    public const float runSpeedMod = 4f;
    public const float playerJumpForce = 4f;

    public static CanPlayer canPlayer = new CanPlayer();
    public static IsPlayer isPlayer = new IsPlayer();

    public static void ResetVariables()
    {
        canPlayer.walk = true;
        canPlayer.run = true;
        canPlayer.jump = true;
        canPlayer.crouch = true;

        isPlayer.walking = false;
        isPlayer.running = false;
        isPlayer.jumping = false;
        isPlayer.crouching = false;

        Debug.Log("Variables Reset");
    }

}

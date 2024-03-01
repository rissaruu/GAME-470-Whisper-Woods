using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Roll : MonoBehaviour
{
    public GameObject d20Model; // Reference to the d20 dice model
    public float rollDuration = 3f; // Duration of the roll animation
    public int result; // Result of the d20 roll

    private bool isRolling = false; // Flag to track if the d20 is currently rolling

    //void Update() 
    //{
    //    if (Input.GetKeyDown(KeyCode.R)) // Check for input to roll the d20
    //    {
    //        RollD20();
    //    }

    //} was used for testing

    public void RollD20()
    {
        if (!isRolling)
        {
            StartCoroutine(RollAnimation());
        }
    }

    public IEnumerator RollAnimation()
    {
        isRolling = true;

        // Generate a random number between 1 and 20
        result = Random.Range(1, 21);

        // Rotate the d20 dice model to simulate rolling
        Quaternion startRotation = d20Model.transform.rotation;
        Quaternion targetRotation = Quaternion.identity;

        // Set the target rotation based on the result, will have to find angle, also, have to find a way to make the dice appear infront of the screen
        switch (result)
        {
            case 1:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 4:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 5:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 6:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 7:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 8:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 9:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 10:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 11:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 12:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 13:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 14:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 15:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 16:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 17:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 18:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 19:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 20:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            default:
                Debug.LogWarning("Unexpected d20 roll result: " + result);
                break;
        }

        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            d20Model.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rollDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        d20Model.transform.rotation = targetRotation;
        isRolling = false;
    }
}
//When a dialouge script is made, use this to call upon the dice roll

//public class DialogueScript : MonoBehaviour
//{
//    public D20Roll d20RollScript;

//    public void OnOptionSelected()
//    {
// Logic to handle when the player selects an option in the dialogue
// If the option requires a d20 roll, call RollD20 method
//        d20RollScript.RollD20();
//    }


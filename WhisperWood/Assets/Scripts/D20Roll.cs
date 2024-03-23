using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Roll : MonoBehaviour
{
    public GameObject d20Model; // Reference to the d20 dice model
    private float rollDuration = 1f; // Duration of the roll animation
    public static int result; // Result of the d20 roll

    private bool isRolling = false; // Flag to track if the d20 is currently rolling

    private Renderer d20Renderer; // Reference to the Renderer component

    void Start()
    {
        // Get the Renderer component of the d20Model GameObject
        d20Renderer = d20Model.GetComponent<Renderer>();

        // Ensure the Renderer component is not null
        if (d20Renderer == null)
        {
            Debug.LogError("Renderer component not found on d20Model GameObject!");
        }

        // Disable the Renderer component initially
        d20Renderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Check for input to roll the d20
        {
            RollD20(false);
        }
        //above key pressed used for testing, in final version (or when a dialouge script is made) it should be activated by the dialouge option needing a dice roll
        if (!isRolling)
        {
            // Hide the D20 object
            d20Renderer.enabled = false;
        }

    }

    public void RollD20(bool isRigged)
    {
        if (!isRolling)
        {
            StartCoroutine(RollAnimation(isRigged));
        }
    }

    public IEnumerator RollAnimation(bool isRigged)
    {
        d20Renderer.enabled = true;

        //Make D20 start at 0, as all other angles are based off it
        d20Model.transform.rotation = Quaternion.Euler(0,0,0);

        isRolling = true;

        // Generate a random number between 1 and 20
        result = Random.Range(1, 21);

        if (isRigged)
        {
            result = Random.Range(1, 6);
        }
        if (!isRigged)
        {
            result = Random.Range(20, 21);
        }

        Debug.Log("Result: " + result);

        // Rotate the d20 dice model to simulate rolling
        Quaternion startRotation = d20Model.transform.rotation;
        Quaternion targetRotation = Quaternion.identity;

        // Set the target rotation based on the result
        switch (result)
        {
            case 1:
                targetRotation = Quaternion.Euler(0, -91, -150);
                break;
            case 2:
                targetRotation = Quaternion.Euler(-56, 247, -278);
                break;
            case 3:
                targetRotation = Quaternion.Euler(-54, 478, -562);
                break;
            case 4:
                targetRotation = Quaternion.Euler(-3, -182, -237);
                break;
            case 5:
                targetRotation = Quaternion.Euler(20, -61, 151);
                break;
            case 6:
                targetRotation = Quaternion.Euler(-18, -122, 151);
                break;
            case 7:
                targetRotation = Quaternion.Euler(-55,-121, 267);
                break;
            case 8:
                targetRotation = Quaternion.Euler(1, -89, 345);
                break;
            case 9:
                targetRotation = Quaternion.Euler(16, 123, -24);
                break;
            case 10:
                targetRotation = Quaternion.Euler(2, -1, -59);
                break;
            case 11:
                targetRotation = Quaternion.Euler(-17, 59, -24);
                break;
            case 12:
                targetRotation = Quaternion.Euler(56, 60, 157);
                break;
            case 13:
                targetRotation = Quaternion.Euler(0, 271, 168);
                break;
            case 14:
                targetRotation = Quaternion.Euler(58, 654, 440);
                break;
            case 15:
                targetRotation = Quaternion.Euler(264, 534, -321);
                break;
            case 16:
                targetRotation = Quaternion.Euler(176, 540, -418);
                break;
            case 17:
                targetRotation = Quaternion.Euler(183, 359, 123);
                break;
            case 18:
                targetRotation = Quaternion.Euler(92, 727, 39);
                break;
            case 19:
                targetRotation = Quaternion.Euler(125, 477, -273);
                break;
            case 20:
                targetRotation = Quaternion.Euler(180, 450, -154);
                break;
            default:
                Debug.LogWarning("Unexpected d20 roll result: " + result);
                break;
        }

        // Define the number of iterations for random rotations
        int numIterations = 10;

        float elapsedTime = 0f;

        for (int i = 0; i < numIterations; i++)
        {
            // Randomize the roll by adding random rotations along multiple axes
            float randomX = Random.Range(-720f, 720f);
            float randomY = Random.Range(-720f, 720f);
            float randomZ = Random.Range(-720f, 720f);
            Quaternion randomRotation = Quaternion.Euler(randomX, randomY, randomZ);

            elapsedTime = 0f;

            while (elapsedTime < rollDuration / numIterations)
            {
                // Interpolate between the start rotation and the randomized rotation
                d20Model.transform.rotation = Quaternion.Slerp(startRotation, randomRotation, elapsedTime / (rollDuration / numIterations));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Update the start rotation for the next iteration
            startRotation = randomRotation;
        }

        // Ensure the final rotation is set to the target rotation
        d20Model.transform.rotation = targetRotation;

        yield return new WaitForSeconds(1f);

        // Reset the rolling flag
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
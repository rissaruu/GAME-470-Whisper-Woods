using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; //new Unity Input System
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction rotateCameraAction; // New input action for camera rotation
    private InputAction zoomCameraAction;
    public Rigidbody rb;
    public Camera mainCamera;
    public Animator playerAnimation;


    void Start()
    {
        //playerAnimation = GameObject.FindWithTag("Player").GetComponent<Animator>();//This should find the detective player model for the animator. Enable when detective model and the capsule are combine for testing.
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        rotateCameraAction = playerInput.actions.FindAction("RotateCamera"); // Initialize rotateCameraAction
        zoomCameraAction = playerInput.actions.FindAction("Zoom");

        rb = GetComponent<Rigidbody>();
        GameManager.ResetVariables();
    }

    void Move()
    {
        GameManager.isPlayer.walking = true;

        Vector2 direction = moveAction.ReadValue<Vector2>();


            // Get the movement direction based on the input
            Vector3 movementDirection = new Vector3(direction.x, 0f, direction.y);

            // Normalize the movement direction to ensure consistent speed
            movementDirection.Normalize();

            // Move the player
            transform.position += transform.TransformDirection(movementDirection) * GameManager.playerWalkSpeed * Time.deltaTime;


    }

    void Update()
    {

        if (GameManager.canPlayer.walk)
        {
            Move();
        }

        // Jump
        if (playerInput.actions["Jump"].triggered && GameManager.canPlayer.jump && !GameManager.isPlayer.jumping) //I might need to figer out if something went wrong with this.
        {
            GameManager.isPlayer.jumping = true;
            Jump();
        }

        GameManager.isPlayer.walking = false;
        GameManager.isPlayer.running = false;
        //PlayerAnimationAndInteractions(); //This plays the animations, enable when testing animations.
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * GameManager.playerJumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((GameManager.isPlayer.jumping)
            && Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.9f) //makes sure can't jump on walls
          //&& Not colliding with tag vault 
        {
            StartCoroutine(CanJumpAgain());
        }
      //elseif(All the other stuff except player is colliding with tag vault)
      //{Timer += Time.Deltatime; if Timer >= 3f;{StartCoroutine(CanJumpAgain());}}
    }

    IEnumerator CanJumpAgain()
    {
        GameManager.canPlayer.jump = false;
        GameManager.isPlayer.jumping = false;
        yield return new WaitForSeconds(1f);
        GameManager.canPlayer.jump = true;
    }

    /* placeholder pseudocode for vaulting parkour
       public void Vault()
    {
        if ( colliding with tag "vault" && GameManager.IsPlayerJumping)
        {
         move the player across object and call animation
        }
    }

    */
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.transform.rotation = data.playerRotation;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerRotation = this.transform.rotation;
    }

    public void PlayerAnimationAndInteractions()
    {
        if (GameManager.isPlayer.walking) //I might need to double check if all else if works without the if.
        {
            playerAnimation.SetBool("walking", true);
            Debug.Log("Walk Animation works!");
        }
        else if (GameManager.isPlayer.jumping)
        {
            playerAnimation.SetBool("jumping", true); //This have a refernce issue.
            Debug.Log("Jumps Animation works!");
        }
        else if (GameManager.isPlayer.running)
        {
            playerAnimation.SetBool("running", true);
            Debug.Log("Run Animation works!");
        }
        else
        {
            playerAnimation.SetBool("walking", false);
            playerAnimation.SetBool("jumping", false);
            playerAnimation.SetBool("running", false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; //new Unity Input System
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
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
    private bool tomInRange;

    void Start()
    {
        playerAnimation = GameObject.FindWithTag("PlayerAnimation").GetComponent<Animator>();//This should find the detective player model for the animator.
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        rotateCameraAction = playerInput.actions.FindAction("RotateCamera"); // Initialize rotateCameraAction
        zoomCameraAction = playerInput.actions.FindAction("Zoom");

        rb = GetComponent<Rigidbody>();
        GameManager.ResetVariables();
    }

    void Move()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Get the movement direction based on the input
        Vector3 movementDirection = new Vector3(direction.x, 0f, direction.y);

        // Normalize the movement direction to ensure consistent speed
        movementDirection.Normalize();

        // Move the player
        if (GameManager.isPlayer.running)
        {
            transform.position += transform.TransformDirection(movementDirection) * GameManager.playerRunSpeed * Time.deltaTime;
            
        }
        else
        {
            transform.position += transform.TransformDirection(movementDirection) * GameManager.playerWalkSpeed * Time.deltaTime;
            
        }
    }       

    void Update()
    {

        if (GameManager.canPlayer.walk)
        {
            Move();
        }

        // Jump
        if (playerInput.actions["Jump"].triggered && GameManager.canPlayer.jump && !GameManager.isPlayer.jumping)
        {
            GameManager.isPlayer.jumping = true;
            Jump();
        }

        if (playerInput.actions["Run"].ReadValue<float>() > 0)
        {
            GameManager.isPlayer.running = true;
            GameManager.isPlayer.walking = false;
        }
        else
        {
            GameManager.isPlayer.running = false;
            GameManager.isPlayer.walking = true;
        }

        Vector2 direction = moveAction.ReadValue<Vector2>();
        float movementMagnitude = direction.magnitude;

        if (movementMagnitude != 0 && !GameManager.isPlayer.jumping)
        {
            if (!GameManager.isPlayer.running)
            {
                GameManager.isPlayer.walking = true;
            }

        }
        else 
        {
            GameManager.isPlayer.walking = false;
            GameManager.isPlayer.running = false;
        }

        PlayerAnimationAndInteractions(); //This plays the animations, enable when testing animations.

        if (tomInRange && GameManager.chaseScene)
        {
            // Check if the player presses the "E" key
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Catch called");
                SceneManager.LoadScene("VictoryScene");
            }
        }

    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * GameManager.playerJumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.isPlayer.jumping && !collision.collider.CompareTag("Vault")
            && Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.9f) //makes sure can't jump on walls
         
        {
            StartCoroutine(CanJumpAgain());
        }
     
       else if (GameManager.isPlayer.jumping && collision.collider.CompareTag("Vault"))
        {

            // Calculate slide direction using the collision normal
            Vector3 slideDirection = Vector3.Cross(collision.contacts[0].normal, Vector3.up).normalized;

            Vector3 oppositeDirection = -slideDirection;

            // Move the player
            transform.Translate(oppositeDirection * GameManager.playerRunSpeed * Time.fixedDeltaTime);
            rb.AddForce(Vector3.up * GameManager.playerJumpForce * 2, ForceMode.Impulse);
            GameManager.isPlayer.jumping = false;
            Debug.Log("Vault called");
       
        }

        else if (collision.collider.CompareTag("Tom"))
        {
            tomInRange = true;
            Debug.Log("Tom in range");
        }

        //{Timer += Time.Deltatime; if Timer >= 31f;{StartCoroutine(CanJumpAgain());}
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Tom"))
        {
            tomInRange = false;
        }
    }

    IEnumerator CanJumpAgain()
    {
        GameManager.canPlayer.jump = false;
        GameManager.isPlayer.jumping = false;
        yield return new WaitForSeconds(1f);
        GameManager.canPlayer.jump = true;
    }

  
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
     //   Debug.Log("isPlayer Walking: " + GameManager.isPlayer.walking);
        if (GameManager.isPlayer.walking == true) //I might need to double check if all else if works without the if.
        {
            playerAnimation.SetBool("walking", true);
            playerAnimation.SetBool("Jumping", false);
            playerAnimation.SetBool("Running", false);
           Debug.Log("Walk Animation works!");
        }
        else if (GameManager.isPlayer.jumping == true)
        {
            playerAnimation.SetBool("Jumping", true); //This have a refernce issue.
            playerAnimation.SetBool("walking", false);
            playerAnimation.SetBool("Running", false);
            Debug.Log("Jump Animation works!");
        }
        else if (GameManager.isPlayer.running)
        {
            playerAnimation.SetBool("Running", true);
            playerAnimation.SetBool("walking", false);
            playerAnimation.SetBool("Jumping", false);
            Debug.Log("Run Animation works!");
        }
        else
        {
            playerAnimation.SetBool("walking", false);
            playerAnimation.SetBool("Jumping", false);
            playerAnimation.SetBool("Running", false);
        }
    }
}

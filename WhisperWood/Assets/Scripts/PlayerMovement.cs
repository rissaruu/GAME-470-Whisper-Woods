using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //new Unity Input System
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    public Rigidbody rb;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        GameManager.ResetVariables();

    }

    void Move()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Get the movement direction based on the camera's forward direction
        Vector3 movementDirection = cameraForward * direction.y + cameraRight * direction.x;
        movementDirection.Normalize(); // Normalize the movement direction vector

        // Move the player
        transform.position += movementDirection * GameManager.playerWalkSpeed * Time.deltaTime;

        if (movementDirection != Vector3.zero && direction.y != -1f && GameManager.canPlayer.rotate)
        {
            // Rotate player towards the camera's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * GameManager.rotationSpeed);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.canPlayer.walk)
        {
            Move();
        }
        


        //Jump
        if (playerInput.actions["Jump"].triggered && GameManager.canPlayer.jump)
        {
            GameManager.isPlayer.jumping = true;
            Jump();
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * GameManager.playerJumpForce, ForceMode.Impulse);
        StartCoroutine(CanJumpAgain());
    }

    IEnumerator CanJumpAgain()
    {
        GameManager.canPlayer.jump = false;
        yield return new WaitForSeconds(1f);
        GameManager.isPlayer.jumping = false;
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


}

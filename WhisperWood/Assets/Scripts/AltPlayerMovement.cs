using UnityEngine;
using UnityEngine.InputSystem;

public class AltPlayerMovement : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction rotateCameraAction;
    [SerializeField] private InputAction resetCameraAction;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    private const float MaxVerticalRotation = 45f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions.FindAction("Move");
        rotateCameraAction = playerInput.actions.FindAction("RotateCamera");
        resetCameraAction = playerInput.actions.FindAction("ResetCamera");

        rb = GetComponent<Rigidbody>();
        GameManager.ResetVariables();

        initialCameraPosition = mainCamera.transform.position; // Store initial camera position
        initialCameraRotation = mainCamera.transform.rotation; // Store initial camera rotation
    }

    void Update()
    {
        if (GameManager.canPlayer.walk)
            Move();

        RotateCamera();

        if (resetCameraAction.triggered)
            ResetCamera();

        GameManager.isPlayer.walking = false;
        GameManager.isPlayer.running = false;
    }

    void Move()
    {
        GameManager.isPlayer.walking = true;

        Vector2 direction = moveAction.ReadValue<Vector2>();

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection = cameraForward * direction.y + cameraRight * direction.x;
        movementDirection.Normalize();

        transform.position += transform.TransformDirection(movementDirection) * GameManager.playerWalkSpeed * Time.deltaTime;

        if (direction != Vector2.zero && direction.y != -1f && GameManager.canPlayer.rotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * GameManager.rotationSpeed);
        }
    }

    void RotateCamera()
    {
        Vector2 rotateInput = rotateCameraAction.ReadValue<Vector2>();

        float horizontalRotation = rotateInput.x;
        float verticalRotation = -rotateInput.y;

        Vector3 currentRotation = mainCamera.transform.localEulerAngles;

        float newVerticalRotation = Mathf.Clamp(currentRotation.x + verticalRotation * GameManager.rotationSpeed, -MaxVerticalRotation, MaxVerticalRotation);

        mainCamera.transform.Rotate(Vector3.up, horizontalRotation * GameManager.rotationSpeed, Space.World);
        mainCamera.transform.localEulerAngles = new Vector3(newVerticalRotation, currentRotation.y, currentRotation.z);
    }

    void ResetCamera()
    {
        mainCamera.transform.position = initialCameraPosition;
        mainCamera.transform.rotation = initialCameraRotation;
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
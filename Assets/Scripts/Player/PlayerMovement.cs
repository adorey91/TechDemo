using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 playerInput;
    private CapsuleCollider player;
    public PlayerInteractions playerInteractions;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float crouchHeight;
    [SerializeField] private float standingHeight;
    private bool isCrouched;
    private bool isRunning;
    private bool isGrounded;

    [Header("View Settings")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float upDownLimit;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float crouchingCamera;
    [SerializeField] private float standingCamera;
    private float verticalRotation;

    public ResetBuildings resetBuildings;
    public FallControl fallControl;
    public GravityControl gravityControl;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<CapsuleCollider>();
        Cursor.visible = true;
    }

    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1.3f, 0), Vector3.up, Color.blue);
    }

    public void FixedUpdate()
    {
        MovePlayer();

        if (playerInput != null)
            moveSpeed = walkSpeed;
    }

    private void MovePlayer()
    {
        PlayerGrounded();

        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = playerCamera.transform.right;

        Vector3 movementVector = playerInput.y * cameraForward + playerInput.x * cameraRight;
        movementVector.Normalize();

        if (isRunning)
            moveSpeed = walkSpeed * 2;
        if (isCrouched)
            moveSpeed = walkSpeed / 2;
        if (!isRunning && !isCrouched)
            moveSpeed = walkSpeed;

        rb.MovePosition(rb.position + movementVector.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mouseDelta = context.ReadValue<Vector2>();

            float mouseRotationX = mouseDelta.x * mouseSensitivity;
            this.transform.Rotate(0, mouseRotationX, 0);

            verticalRotation -= mouseDelta.y * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownLimit, upDownLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
            isRunning = true;
        if (context.canceled)
            isRunning = false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(playerInteractions.reset)
                resetBuildings.resetRequested = true;
            if(playerInteractions.fall)
                fallControl.fallApartNow = true;
            if(playerInteractions.gravity)
                gravityControl.turnOffGravity = true;
        }
    }

    public void Crouch(InputAction.CallbackContext context)     // this isnt working right?
    {
        if (context.performed)
        {
            isCrouched = true;
            player.height = crouchHeight;
        }
        else if (context.canceled)
        {
            // Only uncrouch if there is no obstacle above
            RaycastHit hit;

            if (!Physics.Raycast(transform.position + new Vector3(0, 1.3f, 0), Vector3.up, out hit, 2f))
            {
                isCrouched = false;
                player.height = standingHeight;
            }
        }
    }

    void PlayerGrounded()
    {
        if (GetComponent<Rigidbody>().velocity.y == 0)
            isGrounded = true;
        else
            isGrounded = false;
    }
}

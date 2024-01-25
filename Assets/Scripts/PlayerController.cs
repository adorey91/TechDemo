using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 playerInput;
    private CapsuleCollider player;


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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 movementVector = playerInput.x * Vector3.right + playerInput.y * cameraForward;
        movementVector.Normalize();

        if (isRunning)
            moveSpeed = walkSpeed * 3;
        if (isCrouched)
            moveSpeed = walkSpeed / 2;
        if (!isRunning && !isCrouched)
            moveSpeed = walkSpeed;

        rb.MovePosition(rb.position + movementVector.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue movementValue)
    {
        playerInput = movementValue.Get<Vector2>();
    }

    void OnLook(InputValue cameraMovement)
    {
        Vector2 mouseDelta = cameraMovement.Get<Vector2>();

        float mouseRotationX = mouseDelta.x * mouseSensitivity;
        this.transform.Rotate(0, mouseRotationX, 0);

        verticalRotation -= mouseDelta.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownLimit, upDownLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void OnRun(InputValue input)
    {
        
    }

    void OnJump()
    {

    }

    bool IsGrounded()
    {
        return false;
    }

}

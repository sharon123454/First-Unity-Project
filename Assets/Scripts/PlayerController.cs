using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform cameraTransform;
    CharacterController charController;
    float verticalLookDirection;
    float groundCheckRadius = 0.2f;
    bool isGrounded;
    public Vector3 fallVelocity = Vector3.zero;
    public Transform groundTouchCheck;    
    public LayerMask groundMask;
    public LayerMask platformMask;
    public float gravity = -9.81f;
    public float mouseSensitivity = 150;
    public float moveSpeed = 10;
    public float jumpPower = 5;
    int jumpCount = 0;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {        
        PlayerLook();
        PlayerMove();
        PlayerGravity();
        MoveWithPlatform();
    }

    private void LateUpdate()
    {
        if (transform.position.y < 0)
        {
            transform.position = Vector3.up;
        }
    }

    void PlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        transform.Rotate(Vector3.up, mouseX);

        verticalLookDirection -= mouseY;

        verticalLookDirection = Mathf.Clamp(verticalLookDirection, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookDirection, 0f, 0f);
    }

    void PlayerMove()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.forward * movementZ) + (transform.right * movementX);
        charController.Move(moveDirection * moveSpeed * Time.deltaTime);

        //jumping:
        if ((isGrounded || jumpCount < 2) && Input.GetKeyDown(KeyCode.Space))
        {
            fallVelocity.y = jumpPower;
            jumpCount++;
            if (jumpCount == 1 && !isGrounded)
                jumpCount++;
        }
    }

    void PlayerGravity()
    {
        isGrounded = Physics.CheckSphere(groundTouchCheck.position, groundCheckRadius, groundMask);
        if (isGrounded && fallVelocity.y < 0)
        {
            fallVelocity = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            fallVelocity.y += gravity * Time.deltaTime;
            charController.Move(fallVelocity * Time.deltaTime);
        }
    }

    void MoveWithPlatform()
    {
        Collider[] platformColliders;
        platformColliders = Physics.OverlapSphere(groundTouchCheck.position, groundCheckRadius, platformMask);
        if (platformColliders.Length == 0)
            return;
        Vector3 platfomMovementSum = Vector3.zero;
        for (int i = 0; i < platformColliders.Length; i++)
        {
            MoveObject currentPlatform = platformColliders[i].GetComponent<MoveObject>();
            if (currentPlatform != null)
            {
                platfomMovementSum += currentPlatform.moveDirection;
            }
        }
        charController.Move(platfomMovementSum * Time.deltaTime);
    }
}

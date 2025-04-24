using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;
    public Animator animator;

    public float playerSpeed;
    private Vector3 movePlayer;

    public float gravity = 9.8f;
    private float fallVelocity;

    public float jumpForce;
    public Camera mainCamera;

    private Vector3 camForward;
    private Vector3 camRight;

    public bool isOnSlope = false;
    private Vector3 hitNormal;

    public float slideVelocity;
    public float slopeForceDown;

    // Control de cámara estilo Zelda
    public Vector3 cameraOffset = new Vector3(0, 3, -5);
    public float cameraRotationSpeed = 5f;
    private float currentYaw;

    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RotateCamera();

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        Vector3 horizontalMoveVector = playerInput.x * camRight + playerInput.z * camForward;
        horizontalMoveVector = horizontalMoveVector * playerSpeed;

        if (horizontalMoveVector != Vector3.zero)
            player.transform.LookAt(player.transform.position + horizontalMoveVector);

        SetGravity();
        PlayerSkills();

        animator.SetBool("IsGrounded", player.isGrounded || IsReallyGrounded());
        animator.SetFloat("Velocity", player.velocity.magnitude);

        movePlayer = new Vector3(horizontalMoveVector.x, fallVelocity, horizontalMoveVector.z);
        player.Move(movePlayer * Time.deltaTime);
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void PlayerSkills()
    {
        if ((player.isGrounded || IsReallyGrounded()) && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            animator.SetTrigger("Jump");
        }
    }

    void SetGravity()
    {
        if ((player.isGrounded || IsReallyGrounded()) && fallVelocity < 0)
        {
            fallVelocity = -2f;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }

        SlideDown();
    }

    void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlope && !player.isGrounded)
        {
            fallVelocity = slopeForceDown;

            movePlayer.x += ((1 - hitNormal.x) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1 - hitNormal.z) * hitNormal.z) * slideVelocity;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    bool IsReallyGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, player.height / 2 + 0.2f);
    }

    // Zelda-style camera rotation
    void RotateCamera()
    {
        currentYaw += Input.GetAxis("Mouse X") * cameraRotationSpeed;

        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        Vector3 desiredPosition = transform.position + rotation * cameraOffset;

        mainCamera.transform.position = desiredPosition;
        mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f); // mirar al pecho del jugador
    }
}

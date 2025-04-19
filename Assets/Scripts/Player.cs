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
    public float fallVelocity;
    public float jumpForce;
    public Camera mainCamera;
    private Vector3 camFoward;
    private Vector3 camRight;
    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;
    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //OBTIENE EL MOVIMIENTO HORIZONTAL Y VERTICAL
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        camDirection();
        movePlayer = playerInput.x * camRight + playerInput.z * camFoward;
        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);
        SetGravity();
        PlayerSkills();
        //INDICA SI TOCA EL SUELO
        animator.SetBool("IsGrounded", player.isGrounded);
        //ENVIA LA VELOCIDAD AL ARBOL DE ANIMACION
        animator.SetFloat("Velocity", player.velocity.magnitude);
        //MUEVE EL JUGADOR
        player.Move(movePlayer * Time.deltaTime);
    }
    void camDirection()
    {
        camFoward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;
        camFoward.y = 0;
        camRight.y = 0;
        camFoward = camFoward.normalized;
        camRight = camRight.normalized;
    }
    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            animator.SetTrigger("Jump");
        }
    }
    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        SlideDown();
    }
    void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if (isOnSlope)
        {
            //DESLIZA EN EJE X
            movePlayer.x += ((1 - hitNormal.x) * hitNormal.x) * slideVelocity;
            //DESLIZA EN EJE Z
            movePlayer.z += ((1 - hitNormal.z) * hitNormal.z) * slideVelocity;
            //DESLIZA EN EJE Y
            movePlayer.y = slopeForceDown;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }
}
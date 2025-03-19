using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float speedMultiplier;
    public float jumpHeight;
    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    private Player player;
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private float jumpDelay = 0.2f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetBool("idle", true);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;


        if (move.magnitude > 0.1f)
        {
            animator.SetBool("idle", false);
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("idle", true);
            animator.SetBool("isWalk", false);
        }

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (player.Stamina > 2)
            {
                animator.SetBool("isRunning", true);
                controller.Move(move * moveSpeed * speedMultiplier * Time.deltaTime);
                player.Stamina -= 2;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (player.Stamina > 2 && !isJumping)
            {
                animator.SetBool("idle", false);
                animator.SetBool("isWalk", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpTimer = 0f;
            }
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDelay)
            {
                velocity.y = Mathf.Sqrt(jumpHeight - 2f * Physics.gravity.y);
                isJumping = false;
                animator.SetBool("isJumping", false);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
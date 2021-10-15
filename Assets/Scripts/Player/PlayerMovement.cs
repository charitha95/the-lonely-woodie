using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    private Rigidbody2D player;
    private Animator anim;

    private enum MovementState
    {
        idle, running, jumping, falling
    };

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        float dirX = Input.GetAxisRaw("Horizontal");

        //playerMovement(dirX);
        UpdateAnimationState(dirX);
    }

    private void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void UpdateAnimationState(float dirX)
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (player.velocity.y > 1.5f)
        {
            state = MovementState.jumping;
        }
        else if (player.velocity.y < -1.5f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("movement",
                ((int)state));
    }
}
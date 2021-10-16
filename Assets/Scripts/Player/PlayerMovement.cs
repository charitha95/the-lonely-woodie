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

    public AudioSource playerAudioSource;
    public float footstepsSpeed;
    public AudioClip[] footsteps;
    public AudioClip[] jumpSounds;
    public AudioClip[] fallSounds;
    public float Timer;

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
            PlayRandomFootsteps();
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            PlayRandomFootsteps();
        }
        else
        {
            state = MovementState.idle;
        }

        if (player.velocity.y > 1.5f)
        {
            state = MovementState.jumping;
            PlayRandomJumpSounds();
        }
        else if (player.velocity.y < -1.5f)
        {
            state = MovementState.falling;
            PlayRandomFallSounds();
        }

        anim.SetInteger("movement",
                ((int)state));
    }

    private void PlayRandomFootsteps()
    {
        if (Time.time > Timer && controller.m_Grounded)
        {
            Timer = Time.time + 1 / footstepsSpeed;
            AudioClip clip = footsteps[Random.Range(0, footsteps.Length)];
            playerAudioSource.PlayOneShot(clip);
        }
    }

    private void PlayRandomJumpSounds()
    {
        if (Time.time > Timer && !controller.m_Grounded)
        {
            Timer = Time.time + 1 / 1.5f;
            AudioClip clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            playerAudioSource.PlayOneShot(clip);
        }
    }

    private void PlayRandomFallSounds()
    {
        if (Time.time > Timer && !controller.m_Grounded)
        {
            Timer = Time.time + 1 / 1;
            AudioClip clip = fallSounds[Random.Range(0, fallSounds.Length)];
            playerAudioSource.PlayOneShot(clip);
        }
    }
}
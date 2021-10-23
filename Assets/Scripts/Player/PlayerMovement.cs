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
    public AudioClip[] fightSounds;
    public float Timer;

    public bool isDance;
    private new CapsuleCollider2D collider;
    private bool airFight = false;

    public enum MovementState
    {
        idle, running, jumping, falling, dancing, attack
    };

    private void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("End") == true)
        {
            isDance = true;
        }
    }

    private void FixedUpdate()
    {
        // Move our character
        if (!isDance)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    private void UpdateAnimationState(float dirX)
    {
        MovementState state;

        if (controller.m_Grounded)
        {
            airFight = false;
        }
        if (dirX > 0f && controller.m_Grounded)
        {
            state = MovementState.running;
            PlayRandomFootsteps();
        }
        else if (dirX < 0f && controller.m_Grounded)
        {
            state = MovementState.running;
            PlayRandomFootsteps();
        }
        else
        {
            if (controller.m_Grounded)
                state = MovementState.idle;
            else
                state = MovementState.falling;
        }

        if (player.velocity.y > 1.5f && !airFight && !controller.m_Grounded)
        {
            state = MovementState.jumping;
            PlayRandomJumpSounds();
        }
        else if (player.velocity.y < -1.5f && !airFight && !controller.m_Grounded)
        {
            state = MovementState.falling;
            PlayRandomFallSounds();
        }

        if (Input.GetMouseButtonDown(0))
        {
            airFight = true;
            state = MovementState.attack;
            PlayRandomFightSounds();
        }
        if (isDance)
        {
            state = MovementState.dancing;
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

    private void PlayRandomFightSounds()
    {
        if (fightSounds.Length != 0)
        {
            AudioClip clip = fightSounds[Random.Range(0, fightSounds.Length)];
            playerAudioSource.PlayOneShot(clip);
        }
    }
}
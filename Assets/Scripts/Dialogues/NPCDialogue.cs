using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;

    private ParticleSystem particle;

    private SpriteRenderer spr;
    private new CapsuleCollider2D collider;
    private new AudioSource audio;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        spr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            trigger.StartDialogue();
            StartCoroutine(BreakObject());
        }
    }

    private IEnumerator BreakObject()
    {
        audio.Play();
        particle.Play();
        spr.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
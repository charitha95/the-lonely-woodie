using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    private ParticleSystem particle;
    public bool isStatic = true;
    public AudioClip[] hurtSounds;
    public AudioClip deathSound;
    private AudioSource audSource;
    private SpriteRenderer spr;
    private new CapsuleCollider2D collider;
    private int clipIndex;

    // Start is called before the first frame update
    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        audSource = GetComponentInChildren<AudioSource>();
        spr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(DistroyObject());
        }
        if (!isStatic)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        particle.Play();
        health -= damage;
        if (health > 0)
        {
            clipIndex = Random.Range(0, hurtSounds.Length);
            AudioClip clip = hurtSounds[clipIndex];
            audSource.PlayOneShot(clip);
        }
        else
        {
            audSource.PlayOneShot(deathSound);
        }
    }

    private IEnumerator DistroyObject()
    {
        spr.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(deathSound.length);
        Destroy(gameObject);
    }
}
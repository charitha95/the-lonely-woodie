using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem damageParticle;
    public ParticleSystem deadParticle;
    public int health;
    public HealthbarBehaviour healthbar;

    public AudioClip[] hurtSounds;
    public AudioClip deathSound;
    private AudioSource audSource;
    private SpriteRenderer spr;
    private new CapsuleCollider2D collider;
    private int clipIndex;
    public Animator myAnim;
    private int hitPoint;

    // Start is called before the first frame update
    private void Start()
    {
        //particle = GetComponentInChildren<ParticleSystem>();
        audSource = GetComponentInChildren<AudioSource>();
        spr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        myAnim = GetComponent<Animator>();
        hitPoint = health;
        healthbar.SetHealth(hitPoint, health);
    }

    // Update is called once per frame
    private void Update()
    {
        healthbar.SetHealth(health, hitPoint);

        if (health <= 0)
        {
            healthbar.gameObject.SetActive(false);
            StartCoroutine(DistroyObject());
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoint++;
        health -= damage;
        if (health > 0)
        {
            clipIndex = Random.Range(0, hurtSounds.Length);
            if (hurtSounds.Length > 0)
            {
                AudioClip clip = hurtSounds[clipIndex];
                audSource.PlayOneShot(clip);
            }
            myAnim.Play("GetDamage");
            damageParticle.Play();
        }
        else
        {
            damageParticle.Play();
            audSource.PlayOneShot(deathSound);
            deadParticle.Play();
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
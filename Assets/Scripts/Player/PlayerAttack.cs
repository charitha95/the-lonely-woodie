using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeToAttack;
    public float startTimeToAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    public Animator myAnim;
    public bool isAttacking = false;
    public bool isThrowing = false;
    public static PlayerAttack instance;
    public AudioClip[] fightSounds;
    public AudioSource playerAudioSource;

    private void Awake()
    {
        instance = this;
    }

    //public Animator camAnim;

    // Start is called before the first frame update
    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            PlayRandomFightSounds();
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                //if (enemiesToDamage[i].GetComponent<Enemy>().health == 1)
                //    FreezeFrame.Freeze();
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        if (Input.GetMouseButtonDown(1) && !isThrowing)
        {
            isThrowing = true;
            PlayRandomFightSounds();
            myAnim.Play("throw");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    private void PlayRandomFightSounds()
    {
        if (fightSounds.Length != 0)
        {
            AudioClip clip = fightSounds[UnityEngine.Random.Range(0, fightSounds.Length)];
            playerAudioSource.PlayOneShot(clip);
        }
    }
}
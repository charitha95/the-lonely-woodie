using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    public AudioClip deathSound;
    private AudioSource audioSource;

    public static Health life;

    // Start is called before the first frame update
    private void Start()
    {
        life = GetComponent<Health>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CommonDamageBehaviour();
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            CommonDamageBehaviour();
        }
        if (life.health == 0)
        {
            Die();
        }
    }

    private void CommonDamageBehaviour()
    {
        CameraEffects.ShakeOnce();
        life.health = life.health - 1;
        audioSource.PlayOneShot(deathSound);
        anim.Play("damage");
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
        CameraEffects.ShakeOnce();
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
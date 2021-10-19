using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    private ParticleSystem particle;
    public bool isStatic = true;

    // Start is called before the first frame update
    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        //anim.GetComponent<Animator>();
        //anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (!isStatic)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        particle.Play();
        health -= damage;
    }
}
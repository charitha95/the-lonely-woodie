using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBombEnemy : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;

    public GameObject bullet;
    public GameObject bulletParent;
    public bool facingRight = false;
    public bool canShoot = false;

    private Transform player;
    private Transform initialState;
    private float nextFireTime;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        initialState = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            if (PlayerLife.life.health != 0)
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time && canShoot)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Flip()
    {
        //here your flip funktion, as example
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float delay = 0;
    public float pastTime = 0;
    public float when = 1.0f;
    private Vector3 off;

    public Rigidbody2D rig;
    public GameObject player;
    public bool magnitize = false;

    // Start is called before the first frame update
    private void Awake()
    {
        off = new Vector3(Random.Range(-3, 3), off.y, off.z);
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if (when >= delay)
        {
            pastTime = Time.deltaTime;
            delay += pastTime;
        }
    }
}
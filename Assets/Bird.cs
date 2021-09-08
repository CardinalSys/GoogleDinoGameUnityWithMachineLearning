using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 8;
    public GroundManager gm;
    private void Start()
    {
        int num = Random.Range(1, 3);
        if(num == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.76f, -1);
        }
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundManager>();
    }
    private void Update()
    {
        speed = gm.speed + 3;
        rb.velocity = (Vector2.left * speed);
    }
}

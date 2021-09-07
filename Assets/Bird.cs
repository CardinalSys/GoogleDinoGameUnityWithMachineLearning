using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 8;
    public GroundManager gm;
    private void Start()
    {
        int num = Random.Range(1, 3);
        Debug.Log(num);
        if(num == 1)
        {
            transform.position = new Vector2(transform.position.x, 0);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, -1.76f);
        }
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundManager>();
    }
    private void Update()
    {
        speed = gm.speed;
        rb.velocity = (Vector2.left * speed);
    }
}

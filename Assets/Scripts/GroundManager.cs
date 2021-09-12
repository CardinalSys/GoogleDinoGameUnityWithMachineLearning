using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

    Vector3 g3Pos;
    public Score s;
    public Transform Ground3;
    public float speed = 10f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        g3Pos = Ground3.position;
    }
    private void Update()
    {
        rb.velocity = (Vector2.left * speed);
        if(s.started)
        {
            AddVelocity();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            transform.position = g3Pos;
        }
    }
    private void AddVelocity()
    {
        if(s.currentScore >= 100 && s.currentScore < 200)
        {
            speed = 10f;
        }
        else if (s.currentScore >= 200 && s.currentScore < 300)
        {
            speed = 11f;
        }
        else if (s.currentScore >= 300 && s.currentScore < 400)
        {
            speed = 12f;
        }
        else if (s.currentScore >= 400 && s.currentScore < 500)
        {
            speed = 13f;
        }
        else if (s.currentScore >= 500 && s.currentScore < 600)
        {
            speed = 14f;
        }
        else if (s.currentScore >= 600 && s.currentScore < 800)
        {
            speed = 15f;
        }
        else if (s.currentScore >= 800 && s.currentScore < 1000)
        {
            speed = 16f;
        }
        else if (s.currentScore >= 1000 && s.currentScore < 1300)
        {
            speed = 17f;
        }
        else if (s.currentScore >= 1300 && s.currentScore < 1600)
        {
            speed = 18f;
        }
    }
}

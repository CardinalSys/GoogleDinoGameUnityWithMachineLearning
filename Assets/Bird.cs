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
        RandomPos();
        rb = GetComponent<Rigidbody2D>();
        if(name == "PlayerBird (Clone)")
        {
            gm = GameObject.Find("Ground 1").GetComponent<GroundManager>();
        }
        else if(name == "NpcBird")
        {
            gm = GameObject.Find("NPCGround 1").GetComponent<GroundManager>();
        }
        else if(name == "B1" || name == "B2")
        {
            gm = GameObject.Find("MLGround 1").GetComponent<GroundManager>();
        }
    }
    public void Update()
    {
        speed = gm.speed;
        rb.velocity = (Vector2.left * speed);
    }
    public void RandomPos()
    {
        int num = Random.Range(0, 2);
        if(num == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.76f, -1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public Collider2D jumpBase;

    SpriteRenderer Sprite;
    public Sprite[] sprites = new Sprite[6];

    Rigidbody2D rb;
    public float speed = 8;
    public GroundManager gm;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //gm = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundManager>();

        jumpBase = GameObject.FindGameObjectWithTag("Jump").GetComponent<Collider2D>();
        float x = Random.Range(1.8f, 2.3f);
        float y = Random.Range(1.8f, 2.3f);

        transform.localScale = new Vector3(x, y, 1);

        Sprite = GetComponent<SpriteRenderer>();
        int num = Random.Range(0, 5);
        switch (num)
        {
            case 0:
                Sprite.sprite = sprites[0];
                break;
            case 1:
                Sprite.sprite = sprites[1];
                break;
            case 2:
                Sprite.sprite = sprites[2];
                break;
            case 3:
                Sprite.sprite = sprites[3];
                break;
            case 4:
                Sprite.sprite = sprites[4];
                break;
            case 5:
                Sprite.sprite = sprites[5];
                break;
        }
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), jumpBase);
    }
    private void Update()
    {
        speed = gm.speed;
        rb.velocity = (Vector2.left * speed);
    }
}

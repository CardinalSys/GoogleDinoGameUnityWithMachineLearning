using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10f;

    public Animator anim;

    public Score s;
    public ScenarioMananger sm;

    Rigidbody2D rb;

    bool isGrounded = false;
    public Transform GroundCheck1;
    public LayerMask groundLayer;

    public BoxCollider2D NormalColl, CrouchColl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck1.position, 0.1f, groundLayer);
        PlayerJump();
        Crouch();
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * speed);
        }
        if (isGrounded)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            NormalColl.enabled = false;
            CrouchColl.enabled = true;
            anim.SetBool("Crouch", true);
        }
        else
        {
            anim.SetBool("Crouch", false);
            NormalColl.enabled = true;
            CrouchColl.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Restart Game
            anim.SetBool("Dead", true);
            sm.GameOver();
        }
    }
}

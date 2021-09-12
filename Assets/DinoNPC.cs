using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoNPC : MonoBehaviour
{

    public GameObject[] Cactus, Birds;
    Rigidbody2D rb;
    public float speed = 10f;
    public Transform GroundCheck1;
    public LayerMask groundLayer;
    public bool isGrounded;
    public float jumpTO = 0.5f;
    public ScenarioMananger sm;
    public Animator anim;
    public BoxCollider2D normal, crounch;
    public LayerMask enemy;
    bool jumped = false;
    bool birdObs = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sm.npcDead)
        {
            anim.SetBool("Dead", false);
        }
        if (jumpTO < 0.5f)
        {
            jumpTO += Time.deltaTime * 3;
        }
        else
        {
            jumpTO = 0.5f;
        }
        isGrounded = Physics2D.OverlapCircle(GroundCheck1.position, 0.03f, groundLayer);

        if (!sm.npcDead)
        {
            //Cactus
            foreach (GameObject cactus in Cactus)
            {
                if (cactus != null)
                {
                    float distance = Vector2.Distance(transform.position, cactus.transform.position);

                    foreach (GameObject bird in Birds)
                    {
                        if (bird != null)
                        {
                            float birdDistance = Vector2.Distance(transform.position, bird.transform.position);
                            if(birdDistance < 4 && bird.transform.position.y == -0.5f && jumpTO == 0.5 && isGrounded && distance > 2 && distance < 2.5)
                            {
                                jumpTO = 0;
                                rb.AddForce(Vector2.up * speed);
                                isGrounded = false;
                                birdObs = false;
                                break;
                            }
                            if (birdDistance < 7 && bird.transform.position.y == -0.5f)
                            {
                                birdObs = true;
                                break;
                            }
                            else
                            {
                                birdObs = false;
                            }
                        }
                        else
                        {
                            birdObs = false;
                        }
                    }

                    if (distance < 3f && isGrounded && jumpTO == 0.5 && !jumped)
                    {
                        //Birds
                        if(!birdObs)
                        {
                            anim.SetBool("Crouch", true);
                            normal.enabled = false;
                            crounch.enabled = true;
                        }
                        jumpTO = 0;
                        rb.AddForce(Vector2.up * speed);
                        isGrounded = false;
                        jumped = true;
                        birdObs = false;
                    }
                    else if (isGrounded && jumped && jumpTO == 0.5)
                    {
                        jumped = false;
                        anim.SetBool("Crouch", false);
                        normal.enabled = true;
                        crounch.enabled = false;
                    }
                }
            }
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            sm.NPCGameOver();
            anim.SetBool("Dead", true);
        }
    }
}

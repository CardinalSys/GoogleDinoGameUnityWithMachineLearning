using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DinoAgent : Agent
{
    public ScenarioMananger sm;
    public GameObject[] Cactus, Birds;
    public Animator anim;
    public Transform GroundCheck1;
    public LayerMask groundLayer;
    public BoxCollider2D normal, crounch;
    public GameObject Collision;
    public bool isGrounded, isCrouched;
    public float jumpTO = 0.5f;
    public float jump = 0, crouch = 0;
    public float speed = 850;
    Rigidbody2D rb;
    public Score s;
    public float requiredDifference = 50, currentDifference;
    public GroundManager gm;
    public float currentTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck1.position, 0.03f, groundLayer);

        currentTime += Time.deltaTime;
        if (jumpTO < 0.5f)
        {
            jumpTO += Time.deltaTime * 3;
        }
        else
        {
            jumpTO = 0.5f;
        }
    }
    public override void OnEpisodeBegin()
    {
        //Detect if agent is dead
        if(sm.mlAgentDead == true)
        {
            currentTime = 0;
            s.mlStarted = false;
            sm.MlRestart();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.Cactus[0].transform.position);
        sensor.AddObservation(this.Cactus[1].transform.position);
        sensor.AddObservation(this.Cactus[2].transform.position);
        sensor.AddObservation(this.Birds[0].transform.position);
        sensor.AddObservation(this.Birds[1].transform.position);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        jump = actionBuffers.ContinuousActions[0];
        crouch = actionBuffers.ContinuousActions[1];
        if (crouch == 1f)
        {
            isCrouched = true;
            anim.SetBool("Crouch", true);
            normal.enabled = false;
            crounch.enabled = true;
        }
        else
        {
            isCrouched = false;
            anim.SetBool("Crouch", false);
            normal.enabled = true;
            crounch.enabled = false;
        }
        if (jump == 1 && isGrounded && jumpTO == 0.5f)
        {
            jumpTO = 0;
            jump = 0;
            rb.AddForce(Vector2.up * speed);
            isGrounded = false;
        }

        else if (sm.mlAgentDead == true)
        {
            if((Collision.name == "B1" || Collision.name == "B2") && isCrouched == false && Collision.transform.position.y == 18.04f)
            {
                Collision = null;
                Debug.Log("B collision");
                AddReward(-1f);
            }
            if (currentTime > s.BestTime)
            {
                s.BestTime = currentTime;
                PlayerPrefs.SetFloat("Best", s.BestTime);
                Debug.Log("Reward Positive");
                AddReward(1.0f);
            }
            else if(currentTime == s.BestTime)
            {
                Debug.Log("Reward");
                AddReward(0.5f);
            }
            else if (currentTime < s.BestTime)
            {
                Debug.Log("Reward Negative");
                AddReward(-1f);
            }
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            continuousActionsOut[0] = 1;
        }
        else
        {
            continuousActionsOut[0] = 0;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            continuousActionsOut[1] = 1;
        }
        else
        {
            continuousActionsOut[1] = 0;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            Collision = collision.gameObject;
            sm.mlAgentDead = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Collision = collision.gameObject;
            sm.mlAgentDead = true;
        }
    }
}

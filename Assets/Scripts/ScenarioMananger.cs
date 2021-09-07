using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMananger : MonoBehaviour
{
    public GameObject CactusPref, BirdPref;

    public float cactusTime = 5f;
    public float cactusCurrentTime = 5f;
    public float birdTime = 5f;
    public float birdCurrentTime = 5f;

    public Score s;
    public Rigidbody2D[] Grounds;
    public GameObject DeathScreen;
    public Animator anim;

    private void Update()
    {
        SpawnCactus();
    }

    private void SpawnCactus()
    {
        cactusCurrentTime -= Time.deltaTime;
        if(cactusCurrentTime <= 0 && s.started)
        {
            cactusTime = Random.Range(1, 4);
            cactusCurrentTime = cactusTime;
            Instantiate(CactusPref);
        }
        if(s.currentScore > 300)
        {
            birdCurrentTime -= Time.deltaTime;
        }
        if(birdCurrentTime <= 0 && s.started)
        {
            birdTime = Random.Range(2.0f, 7.1f);
            birdCurrentTime = birdTime;
            Instantiate(BirdPref);
        }
    }

    public void GameOver()
    {
        if (s.currentScore > s.maxScore)
        {
            s.maxScore = s.currentScore;
        }
        s.started = false;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        Grounds[0].gameObject.GetComponent<GroundManager>().speed = 0f;
        Grounds[1].gameObject.GetComponent<GroundManager>().speed = 0f;
        Grounds[2].gameObject.GetComponent<GroundManager>().speed = 0f;


        DeathScreen.SetActive(true);
    }

    public void Restart()
    {
        DeathScreen.SetActive(false);
        s.currentScore = 0;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in Enemies)
        {
            Destroy(enemy);
        }
        anim.SetBool("Dead", false);
        s.started = true;

        Grounds[0].gameObject.GetComponent<GroundManager>().speed = 8f;
        Grounds[1].gameObject.GetComponent<GroundManager>().speed = 8f;
        Grounds[2].gameObject.GetComponent<GroundManager>().speed = 8f;
    }
}

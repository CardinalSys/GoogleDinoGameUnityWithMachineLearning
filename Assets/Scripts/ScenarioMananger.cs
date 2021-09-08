using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMananger : MonoBehaviour
{
    public GameObject CactusPref, BirdPref, Cactus, Bird, NPCCactus, NPCBird;

    public float cactusTime = 5f;
    public float cactusCurrentTime = 5f;
    public float birdTime = 5f;
    public float birdCurrentTime = 5f;

    public Score s;
    public Rigidbody2D[] Grounds;
    public Rigidbody2D[] NPCGrounds;
    public GameObject DeathScreen;
    public Animator anim;
    public DinoNPC npc;

    public bool playerDead = false, npcDead = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        SpawnCactus();
    }

    private void SpawnCactus()
    {
        cactusCurrentTime -= Time.deltaTime;
        if(cactusCurrentTime <= 0)
        {
            cactusTime = Random.Range(1, 4);
            cactusCurrentTime = cactusTime;
            if(!playerDead)
            {
                Cactus = Instantiate(CactusPref);
                Cactus.GetComponent<Cactus>().gm = Grounds[0].gameObject.GetComponent<GroundManager>();
                Cactus.name = "PlayerCactus";
            }
            if (!npcDead)
            {
                NPCCactus = Instantiate(CactusPref, new Vector3(CactusPref.transform.position.x, -2.12f, -1), CactusPref.transform.rotation);
                NPCCactus.GetComponent<Cactus>().gm = NPCGrounds[0].gameObject.GetComponent<GroundManager>();
                for (int num = 0; num < 4; num++)
                {
                    if (npc.Cactus[num] == null)
                    {
                        npc.Cactus[num] = NPCCactus;
                        break;
                    }
                }
                NPCCactus.name = "NPCCactus";
            }
        }
        if (s.currentScore > 100 || s.npcCurrentScore > 100)
        {
            birdCurrentTime -= Time.deltaTime;
        }
        if(birdCurrentTime <= 0)
        {
            birdTime = Random.Range(2.0f, 7.1f);
            birdCurrentTime = birdTime;
            if (!playerDead && s.currentScore > 100)
            {
                Instantiate(BirdPref);
                BirdPref.GetComponent<Bird>().gm = Grounds[0].gameObject.GetComponent<GroundManager>();
                BirdPref.name = "PlayerBird";
            }
            if(!npcDead && s.npcCurrentScore > 100)
            {
                NPCBird = Instantiate(BirdPref, new Vector3(BirdPref.transform.position.x, -0.5f, -1), BirdPref.transform.rotation);
                NPCBird.GetComponent<Bird>().gm = NPCGrounds[0].gameObject.GetComponent<GroundManager>();
                NPCBird.name = "NpcBird";
                for(int num = 0; num < 2; num++)
                {
                    if(npc.Birds[num] == null)
                    {
                        if(NPCBird.transform.position.y == -0.5f)
                        {
                            npc.Birds[num] = NPCBird;
                            break;
                        }
                    }
                }
            }

        }
    }

    public void GameOver()
    {
        if (s.currentScore > s.maxScore)
        {
            s.maxScore = s.currentScore;
        }
        s.started = false;
        playerDead = true;

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

        foreach(GameObject enemy in Enemies)
        {
            if(enemy.name == "PlayerCactus" || enemy.name == "PlayerBird")
            {
                Destroy(enemy);
            }
        }

        anim.SetBool("Dead", false);
        s.started = true;
        playerDead = false;

        Grounds[0].gameObject.GetComponent<GroundManager>().speed = 8f;
        Grounds[1].gameObject.GetComponent<GroundManager>().speed = 8f;
        Grounds[2].gameObject.GetComponent<GroundManager>().speed = 8f;
    }

    public void NPCGameOver()
    {
        NPCGrounds[0].gameObject.GetComponent<GroundManager>().speed = 0f;
        NPCGrounds[1].gameObject.GetComponent<GroundManager>().speed = 0f;
        NPCGrounds[2].gameObject.GetComponent<GroundManager>().speed = 0f;
        if (s.npcCurrentScore > s.npcMaxScore)
        {
            s.npcMaxScore = s.npcCurrentScore;
        }
        s.npcStarted = false;
        npcDead = true;

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in Enemies)
        {
            if(enemy.name == "NPCCactus" || enemy.name == "NpcBird")
            {
                Destroy(enemy);

            }
        }

        StartCoroutine(NpcRespawn());
    }

    IEnumerator NpcRespawn()
    {
        yield return new WaitForSeconds(5);
        NPCRestart();
    }

    public void NPCRestart()
    {
        s.npcCurrentScore = 0;

        NPCGrounds[0].gameObject.GetComponent<GroundManager>().speed = 8f;
        NPCGrounds[1].gameObject.GetComponent<GroundManager>().speed = 8f;
        NPCGrounds[2].gameObject.GetComponent<GroundManager>().speed = 8f;

        s.npcStarted = true;
        npcDead = false;
    }

}

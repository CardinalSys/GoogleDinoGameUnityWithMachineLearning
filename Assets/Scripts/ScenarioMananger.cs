using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMananger : MonoBehaviour
{
    public GameObject CactusPref, BirdPref, Cactus, Bird, NPCCactus, NPCBird, MlCactus, MlBird;

    public float cactusTime = 5f;
    public float cactusCurrentTime = 5f;
    public float birdTime = 5f;
    public float birdCurrentTime = 5f;

    public Score s;
    public Rigidbody2D[] Grounds, NPCGrounds, MLGrounds;
    public GameObject DeathScreen;
    public Animator anim;
    public DinoNPC npc;
    public DinoAgent agent;
    public int cactusNum = 0, birdsNum = 0;

    public bool playerDead = false, npcDead = false, mlAgentDead = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        SpawnCactus();
        if (cactusNum > 2)
        {
            cactusNum = 0;
        }
        if(birdsNum > 1)
        {
            birdsNum = 0;
        }
    }

    private void SpawnCactus()
    {
        cactusCurrentTime -= Time.deltaTime;
        if(cactusCurrentTime <= 0)
        {
            cactusTime = Random.Range(1, 4);
            cactusCurrentTime = cactusTime;
            //Player
            if(!playerDead)
            {
                Cactus = Instantiate(CactusPref);
                Cactus.GetComponent<Cactus>().gm = Grounds[0].gameObject.GetComponent<GroundManager>();
                Cactus.name = "PlayerCactus";
            }
            //Npc
            if (!npcDead)
            {
                NPCCactus = Instantiate(CactusPref, new Vector3(CactusPref.transform.position.x, -2.12f, -1), CactusPref.transform.rotation);
                NPCCactus.GetComponent<Cactus>().gm = NPCGrounds[0].gameObject.GetComponent<GroundManager>();
                for (int num = 0; num < 6; num++)
                {
                    if (npc.Cactus[num] == null)
                    {
                        npc.Cactus[num] = NPCCactus;
                        break;
                    }
                }
                
                NPCCactus.name = "NPCCactus";
            }
            if (!mlAgentDead && agent.Cactus[cactusNum].transform.position.y > 25)
            {
                agent.Cactus[cactusNum].transform.position = new Vector3(CactusPref.transform.position.x, 16.44f, -1);
                cactusNum++;
            }
        }
        if (s.currentScore > 100 || s.npcCurrentScore > 100 || s.mlCurrentScore > 0)
        {
            birdCurrentTime -= Time.deltaTime;
        }
        if(birdCurrentTime <= 0)
        {
            int x = Random.Range(0, 2);
            if(x == 0)
            {
                birdCurrentTime = cactusCurrentTime - 3;
            }
            else
            {
                birdCurrentTime = cactusCurrentTime + 4;
            }
            birdCurrentTime = birdTime;
            //Player
            if (!playerDead && s.currentScore > 100)
            {
                Instantiate(BirdPref);
                BirdPref.name = "PlayerBird";
            }
            //Npc
            if(!npcDead && s.npcCurrentScore > 100)
            {
                NPCBird = Instantiate(BirdPref, new Vector3(BirdPref.transform.position.x, -0.5f, -1), BirdPref.transform.rotation);
                NPCBird.name = "NpcBird";
                for(int num = 0; num < 6; num++)
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
            //MlAgent
            if (!mlAgentDead && agent.Birds[birdsNum].transform.position.y > 25)
            {
                agent.Birds[birdsNum].transform.position = new Vector3(CactusPref.transform.position.x, 18.04f, -1);
                agent.Birds[birdsNum].GetComponent<Bird>().RandomPos();
                birdsNum++;
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

    public void MlRestart()
    {

        if (s.mlCurrentScore > s.mlMaxScore)
        {
            s.mlMaxScore = s.mlCurrentScore;
        }
        s.mlCurrentScore = 0;

        agent.Cactus[0].transform.position = new Vector3(3f, 16.43f, -1);
        agent.Cactus[1].transform.position = new Vector3(13.3f, 16.43f,  -1);
        agent.Cactus[2].transform.position = new Vector3(21.24f, 16.43f, -1);

        agent.Birds[0].transform.position = new Vector3(0, 30, -1);
        agent.Birds[1].transform.position = new Vector3(0, 30, -1);

        MLGrounds[0].gameObject.GetComponent<GroundManager>().speed = 8f;
        MLGrounds[1].gameObject.GetComponent<GroundManager>().speed = 8f;
        MLGrounds[2].gameObject.GetComponent<GroundManager>().speed = 8f;
        s.mlStarted = true;
        mlAgentDead = false;
    }

}

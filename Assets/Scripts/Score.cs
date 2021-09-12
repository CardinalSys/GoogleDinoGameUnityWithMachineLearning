using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text CurrentScore, MaxScore, NpcCurrentScore, NcpMaxScore, MlCurrentScore, MlMaxScore;


    public bool started = true, npcStarted = true, mlStarted = true;
    public float currentScore, maxScore, npcCurrentScore, npcMaxScore, mlCurrentScore, mlMaxScore;

    public float BestTime = 0;


    string zeros = "000000", npcZeros = "000000", mlZeros = "000000";
    int zerosRemoved = 0, npcZerosRemoved = 0, mlZerosRemoved = 0;
    private void Start()
    {
        BestTime = PlayerPrefs.GetFloat("Best");
        MlMaxScore.text = BestTime.ToString();
    }
    private void Update()
    {
        //Player
        if(started)
        {
            currentScore += Time.deltaTime * 8f;
            if (currentScore > 9 && zerosRemoved == 0)
            {
                zerosRemoved++;
                zeros = zeros.Remove(zeros.Length - 1, 1);
            }
            else if (currentScore > 99 && zerosRemoved == 1)
            {
                zerosRemoved++;
                zeros = zeros.Remove(zeros.Length - 1, 1);
            }
            else if (currentScore > 999 && zerosRemoved == 2)
            {
                zerosRemoved++;
                zeros = zeros.Remove(zeros.Length - 1, 1);
            }
            else if (currentScore > 9999 && zerosRemoved == 3)
            {
                zerosRemoved++;
                zeros = zeros.Remove(zeros.Length - 1, 1);
            }
            CurrentScore.text = zeros.ToString() + (int)currentScore;
        }
        MaxScore.text = "Hi " + (int)maxScore;   
        //Npc
        if(npcStarted)
        {
            npcCurrentScore += Time.deltaTime * 8f;
            if (npcCurrentScore > 9 && npcZerosRemoved == 0)
            {
                npcZerosRemoved++;
                npcZeros = npcZeros.Remove(zeros.Length - 1, 1);
            }
            else if (npcCurrentScore > 99 && npcZerosRemoved == 1)
            {
                npcZerosRemoved++;
                npcZeros = npcZeros.Remove(zeros.Length - 1, 1);
            }
            else if (npcCurrentScore > 999 && npcZerosRemoved == 2)
            {
                npcZerosRemoved++;
                npcZeros = npcZeros.Remove(zeros.Length - 1, 1);
            }
            else if (npcCurrentScore > 9999 && npcZerosRemoved == 3)
            {
                npcZerosRemoved++;
                npcZeros = npcZeros.Remove(zeros.Length - 1, 1);
            }
            NpcCurrentScore.text = npcZeros.ToString() + (int)npcCurrentScore;
        }
        NcpMaxScore.text = "Hi " + (int)npcMaxScore;

        //MlAgent

        if (mlStarted)
        {
            mlCurrentScore += Time.deltaTime * 8f;
            if (mlCurrentScore > 9 && mlZerosRemoved == 0)
            {
                mlZerosRemoved++;
                mlZeros = mlZeros.Remove(zeros.Length - 1, 1);
            }
            else if (mlCurrentScore > 99 && mlZerosRemoved == 1)
            {
                mlZerosRemoved++;
                mlZeros = mlZeros.Remove(zeros.Length - 1, 1);
            }
            else if (mlCurrentScore > 999 && mlZerosRemoved == 2)
            {
                mlZerosRemoved++;
                mlZeros = mlZeros.Remove(zeros.Length - 1, 1);
            }
            else if (mlCurrentScore > 9999 && mlZerosRemoved == 3)
            {
                mlZerosRemoved++;
                mlZeros = mlZeros.Remove(zeros.Length - 1, 1);
            }
            MlCurrentScore.text = mlZeros.ToString() + (int)mlCurrentScore;
        }
        MlMaxScore.text = "Hi " + (int)mlMaxScore;
    }
}

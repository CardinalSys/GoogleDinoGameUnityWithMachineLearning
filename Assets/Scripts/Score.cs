using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text CurrentScore, MaxScore, NpcCurrentScore, NcpMaxScore;


    public bool started = true, npcStarted = true;
    public float currentScore, maxScore, npcCurrentScore, npcMaxScore;


    string zeros = "0000", npcZeros = "0000";
    int zerosRemoved = 0, npcZerosRemoved = 0;

    private void Update()
    {
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
    }
}

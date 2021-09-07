using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text CurrentScore;
    public Text MaxScore;

    public bool started = true;
    public float currentScore;
    public float maxScore;

    string zeros = "0000";
    int zerosRemoved = 0;

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
    }
}
